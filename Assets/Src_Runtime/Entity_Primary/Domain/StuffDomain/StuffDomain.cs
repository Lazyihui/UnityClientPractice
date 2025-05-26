using System;
using UnityEngine;
using MyTelepathy;
using PlasticPipe.PlasticProtocol.Messages;

namespace Game_Client {

    public static class StuffDomain {

        public static StuffEntity SpawnStuffByBro(GameSystemContext ctx, StuffSpawnBroMessage bro) {
            var entity = GameFactory.Stuff_CreateResBro(ctx.assetsModule, ctx.iDServer);
            entity.idSig = bro.idSig;
            entity.SetPos(bro.pos);

            ctx.stuffRepository.Add(entity);
            return entity;
        }

        public static void UnSpawnStuffByBro(GameSystemContext ctx, int iDSignature) {
            bool has = ctx.stuffRepository.TryGet(iDSignature, out StuffEntity entity);
            if (!has) {
                Debug.LogWarning($"找不到物体实体: {iDSignature}");
                return;
            }

            // 1.销毁物体
            entity.TearDown();
            ctx.stuffRepository.Remove(entity);
        }

        public static void OnMove(GameSystemContext ctx, StuffMoveBroMessage msg) {
            bool has = ctx.stuffRepository.TryGet(msg.iDSignature, out StuffEntity foundEntity);
            if (!has) {
                Debug.LogWarning($"找不到物体实体: {msg.iDSignature}");
                return;
            }

            // 1.设置物体位置
            foundEntity.SetPos(msg.pos);
        }
    }
}