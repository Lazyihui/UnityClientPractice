using System;
using UnityEngine;
using Telepathy;
using MyTelepathy;

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

        public void Enter(string roleName) {

            ctx.roleName = roleName;
            ctx.isRunning = true;
            // 1.发送信息由Server生成角色
            SpawnRoleReqMessage req = new SpawnRoleReqMessage();
            req.roleName = ctx.roleName;
            byte[] data = MessageHelper.ToData(req);
            ctx.client.Send(data);

        }

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
            RoleEntity owner = ctx.GetOwner();
            if (owner == null) {
                return;
            }
            RoleDomain.Input_Record(ctx, owner);

            RoleDomain.Input_Apply(ctx, owner, dt);
        }

        public void LogicTick(float dt) {
            var client = ctx.client;

            RoleEntity owner = ctx.GetOwner();
            if (owner == null) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                // 发送一条信息
                SpawnBulletReqMessage req = new SpawnBulletReqMessage();
                req.rootPos = owner.BulletRoot;
                req.belongName = owner.roleName;

                req.pos = owner.BulletRoot.position;
                req.dir = Vector3.up;
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