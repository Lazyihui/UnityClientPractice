using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telepathy;


namespace Game_Client {

    public class ClientMain : MonoBehaviour {
        Client client;

        public int port = 7777;
        public int messageSize = 1024;
        string ip = "127.0.0.1"; // 服务器IP地址

        bool isTearDown = false;

        // === System ===
        public static GameSystemContext gameContext;

        // === Module ===

        public static AssetsModule assetsModule;

        void Start() {
            Application.runInBackground = true; // 允许后台运行

            client = new Client(messageSize);
            client.Connect(ip, port);


            client.OnConnected += () => {
                Debug.Log("成功链接");
            };
            client.OnData += (message) => {
                // 处理接收到的数据
                Debug.Log("收到的信息: " + message);
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
            if (client != null) {
                client.Tick(10); // 每帧处理一次
            }

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

            if (client != null) {
                client.Disconnect();
                client = null;
            }
        }
    }
}
