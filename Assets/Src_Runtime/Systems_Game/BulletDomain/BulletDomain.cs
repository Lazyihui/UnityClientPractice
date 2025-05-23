using System;
using UnityEngine;

namespace Game_Client {

    public static class BulletDomain {

        public static BulletEntity SpawnByBro(GameSystemContext ctx, SpawnBulletBroMessage bro, Vector3 rootPos) {
            var entity = GameFactory.Bullet_CreateResBro(ctx.assetsModule, ctx.iDServer);
            // entity.idSig = bro.idSig;
            entity.Ctor();
            entity.SetPos(rootPos);

            // 设置位置 还没想好要怎么样设置

            return null;
        }
    }
}