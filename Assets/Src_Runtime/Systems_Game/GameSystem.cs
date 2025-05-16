using System;
using System.Collections.Generic;
using Protocoles;
using UnityEditor.VersionControl;
using UnityEngine;
using Telepathy;
using PlasticPipe.PlasticProtocol.Messages;

namespace Game_Client {

    public class GameSystem : MonoBehaviour {

        GameSystemContext ctx;
        public GameSystemContext Ctx => ctx;

        public void Ctor() {
            ctx = new GameSystemContext();
        }

        public void Inject(AssetsModule assetsModule, InputModule inputModule, Client client) {
            ctx.Inject(assetsModule, inputModule, client);
        }

        public void Enter() {
            ctx.isRunning = true;
            // 1.发送信息由Server生成角色
            SpawnRoleReqMessage req = new SpawnRoleReqMessage();
            // 生成 
            // RoleEntity role = RoleDomain.Spawn(ctx);
            // // 给Serverf发发送一条信息
            // SpawnRoleReqMessage req = new SpawnRoleReqMessage();
            // req.idSig = role.idSig;
            // req.pos = role.transform.position;
            // Debug.Log(req.pos);
            byte[] data = MessageHelper.ToData(req);
            ctx.client.Send(data);

        }


        // 临时写这里

        public void Tick(float dt) {

            if (!ctx.isRunning) {
                return;
            }

            var game = ctx.gameEntity;

            PreTick(dt);

            ref float restFixTime = ref ctx.gameEntity.restFixTime;
            restFixTime += dt;
            const float FIX_INTERVAL = 0.020f;
            if (restFixTime <= FIX_INTERVAL) {
                LogicTick(restFixTime);
                restFixTime = 0;
            } else {
                while (restFixTime >= FIX_INTERVAL) {
                    LogicTick(FIX_INTERVAL);
                    restFixTime -= FIX_INTERVAL;
                }
            }

            LastTick(dt);
        }

        public void PreTick(float dt) {
            // PreTick 
            // int len = ctx.RoleRepository.TakeAll(out RoleEntity[] roles);
            // for (int i = 0; i < len; i++) {
            //     var role = roles[i];
            //     RoleDomain.Input_Record(ctx, role);
            // }

            RoleEntity owner = ctx.GetOwner();
            RoleDomain.Input_Record(ctx, owner);
        }
        public void LogicTick(float dt) {
            var client = ctx.client;

            // LogicTick

            RoleEntity owner = ctx.GetOwner();
            RoleDomain.Move(owner);

            // 
            if (Input.GetKeyDown(KeyCode.Space)) {
                // 发送一条信息
                TestReqMessage req = new TestReqMessage();
                req.pos = owner.transform.position;
                Debug.Log(req.pos);
                byte[] data = MessageHelper.ToData(req);
                client.Send(data);
            }
        }

        public void LastTick(float dt) {
            // LastTick
        }

        public void Exit() {
            Debug.Log("GameSystem Exit");
        }
    }

}