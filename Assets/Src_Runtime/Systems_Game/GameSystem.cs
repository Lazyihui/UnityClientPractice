using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Game_Client {

    public class GameSystem : MonoBehaviour {

        GameSystemContext ctx;

        public void Ctor() {
            ctx = new GameSystemContext();
        }

        public void Inject(AssetsModule assetsModule) {
            ctx.Inject(assetsModule);
        }

        public void Enter() {
            ctx.isRunning = true;

            RoleDomain.Spawn(ctx);
            Debug.Log("GameSystem Enter");
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
            // PreTick
        }
        public void LogicTick(float dt) {
            // LogicTick
        }

        public void LastTick(float dt) {
            // LastTick
        }

        public void Exit() {
            Debug.Log("GameSystem Exit");
        }
    }

}