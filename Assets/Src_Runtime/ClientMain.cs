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
                gameSys.Enter();
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
                Debug.Log("收到的信息: " + message);

                int typeID = MessageHelper.ReadHeader(message.Array);
                Debug.Log("消息类型ID: " + typeID);

                // 广播
                if (typeID == MessageConst.SpawnRole_Bro) {

                    SpawnRoleBroMessage bro = MessageHelper.ReadDate<SpawnRoleBroMessage>(message.Array);
                    RoleDomain.OnSpanw(gameSys.Ctx);

                } else if (typeID == 1) {

                }

                // 发的单条信息
                if (typeID == MessageConst.SpawnRole_Res) {
                    Debug.Log("发给自己");
                    RoleEntity owner = RoleDomain.OnSpanw(gameSys.Ctx);
                    gameSys.Ctx.gameEntity.OwnerIDsig = owner.idSig;
                }

            };

            client.OnDisconnected += () => {
                Debug.Log("断开链接");
            };

            // TODO:这里信息为什么没有发出去
            // // 临时发一条消息
            // string str = "Hello, Server!";
            // byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            // client.Send(data);

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


            if (client != null) {
                client.Disconnect();
                client = null;
            }
        }
    }
}
