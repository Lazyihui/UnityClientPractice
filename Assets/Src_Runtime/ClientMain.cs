using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telepathy;
using UnityEditor.IMGUI.Controls;
using Protocoles;
using PlasticPipe.PlasticProtocol.Messages;


namespace Game_Client {

    public class ClientMain : MonoBehaviour {

        [SerializeField] string roleName;

        Client client;

        public int port = 7777;
        public int messageSize = 1024;
        string ip = "127.0.0.1"; // 服务器IP地址

        bool isTearDown = false;
        bool isInit = false;

        // === System ===
        public static GameSystem gameSys;

        // === Module ===

        public static AssetsModule assetsModule;
        public static InputModule inputModule;

        void Start() {
            // === System ===
            gameSys = GetComponentInChildren<GameSystem>();
            gameSys.Ctor();

            // === Module ===
            assetsModule = GetComponentInChildren<AssetsModule>();
            assetsModule.Ctor();

            inputModule = GetComponentInChildren<InputModule>();
            inputModule.Ctor();


            Action action = async () => {
                await assetsModule.LoadAll();

                isInit = true;

                // ---  Enter  ---
                gameSys.Enter(roleName);
            };
            action.Invoke();

            // 

            client = new Client(messageSize);
            client.Connect(ip, port);
            // === Inject ===
            gameSys.Inject(assetsModule, inputModule, client);
            Application.runInBackground = true; // 允许后台运行

            client.OnConnected += () => {
                Debug.Log("成功链接");
                // 1. 发送连接请求

            };

            client.OnData += (message) => {
                // 处理接收到的数据

                int typeID = MessageHelper.ReadHeader(message.Array);

                // Bro
                if (typeID == MessageConst.SpawnRole_Bro) {

                    SpawnRoleBroMessage bro = MessageHelper.ReadDate<SpawnRoleBroMessage>(message.Array);
                    Debug.Log("生成配角");
                    RoleDomain.OnSpawnByBro(gameSys.Ctx, bro);

                } else if (typeID == MessageConst.Move_Bro) {

                    MoveBroMessage bro = MessageHelper.ReadDate<MoveBroMessage>(message.Array);
                    if (bro.roleName != roleName) {  // 其他玩家移动
                        RoleDomain.OnMove(gameSys.Ctx, bro);
                    } else {  // 本地玩家的服务端校验
                        if (bro.timestamp > gameSys.Ctx.lastMoveTimestamp) {
                            RoleDomain.OnMove(gameSys.Ctx, bro);
                        }
                    }

                }

                // Res
                if (typeID == MessageConst.SpawnRole_Res) {

                    SpawnRoleResMessage res = MessageHelper.ReadDate<SpawnRoleResMessage>(message.Array);
                    RoleEntity owner = RoleDomain.OnSpawnByRes(gameSys.Ctx, res);
                    gameSys.Ctx.gameEntity.OwnerIDsig = owner.idSig;
                }

            };

            client.OnDisconnected += () => {
                Debug.Log("断开链接");
            };

        }

        void Update() {
            if (!isInit) {
                return;
            }

            float dt = Time.deltaTime;

            if (client != null) {
                client.Tick(10); // 每帧处理一次
            }
            inputModule.Process(dt);

            gameSys.Tick(dt);
        }

        void OnDestory() {
            TearDown();
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;
            assetsModule.UnLoadAll();

            client?.Disconnect();
            client = null;
        }
    }
}
