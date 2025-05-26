using System;
using UnityEngine;
using MyTelepathy;


namespace Game_Client {

    public static class BulletDomain {

        public static BulletEntity OnSpawnByBro(GameSystemContext ctx, SpawnBulletBroMessage bro, Transform rootPos) {
            var entity = GameFactory.Bullet_CreateResBro(ctx.assetsModule, ctx.iDServer, bro.rootPos);
            entity.idSig = bro.idSig;
            entity.SetPos(rootPos.position);
            entity.Ctor();
            ctx.bulletRepository.Add(entity);

            return entity;
        }

        public static void UnSpawnByBro(GameSystemContext ctx, int iDSignature) {

            ctx.bulletRepository.TryGet(iDSignature, out var entity);
            entity.TearDown();
            ctx.bulletRepository.Remove(entity);
        }

        public static void OnMove(GameSystemContext ctx, BulletMoveBroMessage bro) {
            // 1. 查找对应的子弹实体
            bool has = ctx.bulletRepository.TryGet(bro.idSig, out BulletEntity bullet);

            if (!has) {
                Debug.LogWarning($"找不到子弹实体: {bro}");
                return;
            }
            // 2. 更新子弹位置
            bullet.SetPos(bro.position);
        }

        
    }
}