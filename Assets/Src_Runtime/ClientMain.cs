using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telepathy;
using UnityEditor.IMGUI.Controls;


namespace Game_Client {

    public class ClientMain : MonoBehaviour {
        Client client;

        public int port = 7777;
        public int messageSize = 1024;
        string ip = "127.0.0.1"; // 服务器IP地址

        bool isTearDown = false;
        bool isInit = false;

        // === System ===
        public static GameSystem gameContext;

        // === Module ===

        public static AssetsModule assetsModule;

        void Start() {
            // === System ===
            gameContext = GetComponentInChildren<GameSystem>();
            gameContext.Ctor();

            // === Module ===
            assetsModule = GetComponentInChildren<AssetsModule>();
            assetsModule.Ctor();

            Action action = async () => {

                await assetsModule.LoadAll();

                isInit = true;

                // ---  Enter  ---
                gameContext.Enter();

            };
            action.Invoke();

            // 
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
            if(!isInit) {
                return;
            }

            float dt = Time.deltaTime;

            if (client != null) {
                client.Tick(10); // 每帧处理一次
            }

            gameContext.Tick(dt);
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
