using System;
using UnityEngine;
using Telepathy;
using MyTelepathy;
using JetBrains.Annotations;
using System.Linq.Expressions;

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

                    if (bro.roleType == RoleType.Player) {
                        RoleDomain.OnSpawnByBro(gameSys.Ctx, bro);
                    } else if (bro.roleType == RoleType.Monster) {
                        RoleDomain.OnSpawnByBro(gameSys.Ctx, bro);
                    }

                } else if (typeID == MessageConst.Move_Bro) {

                    MoveBroMessage bro = MessageHelper.ReadDate<MoveBroMessage>(message.Array);

                    if (bro.roleName != roleName) {  // 其他玩家移动
                        RoleDomain.OnMove(gameSys.Ctx, bro);
                    } else {  // 本地玩家的服务端校验
                        if (bro.timestamp > gameSys.Ctx.lastMoveTimestamp) {
                            RoleDomain.OnMove(gameSys.Ctx, bro);
                        }
                    }

                } else if (typeID == MessageConst.BulletSpawn_Bro) {
                    var owner = gameSys.Ctx.GetOwner();
                    var rootPos = owner.BulletRoot;
                    SpawnBulletBroMessage bro = MessageHelper.ReadDate<SpawnBulletBroMessage>(message.Array);

                    BulletDomain.OnSpawnByBro(gameSys.Ctx, bro, rootPos);
                } else if (typeID == MessageConst.BulletMove_Bro) {
                    BulletMoveBroMessage bro = MessageHelper.ReadDate<BulletMoveBroMessage>(message.Array);

                    BulletDomain.OnMove(gameSys.Ctx, bro);
                } else if (typeID == MessageConst.BulletDestory_Bro) {

                    var bro = MessageHelper.ReadDate<BulletDestoryBroMessage>(message.Array);
                    BulletDomain.UnSpawnByBro(gameSys.Ctx, bro.idSig);

                } else if (typeID == MessageConst.StuffSpawn_Bro) {
                    StuffSpawnBroMessage bro = MessageHelper.ReadDate<StuffSpawnBroMessage>(message.Array);
                    StuffDomain.SpawnStuffByBro(gameSys.Ctx, bro);
                } else if (typeID == MessageConst.StuffMove_Bro) {
                    var bro = MessageHelper.ReadDate<StuffMoveBroMessage>(message.Array);
                    StuffDomain.OnMove(gameSys.Ctx, bro);
                } else if (typeID == MessageConst.StuffDestory_Bro) {
                    var bro = MessageHelper.ReadDate<StuffDestoryBroMessage>(message.Array);
                    StuffDomain.UnSpawnStuffByBro(gameSys.Ctx, bro.idSig);
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
