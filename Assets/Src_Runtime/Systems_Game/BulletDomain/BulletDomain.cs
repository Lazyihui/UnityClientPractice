using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static class BulletDomain {

        public static BulletEntity OnSpawnByBro(GameSystemContext ctx, SpawnBulletBroMessage bro) {
            var entity = GameFactory.Bullet_CreateResBro(ctx.assetsModule, ctx.iDServer, bro.rootPos);
            // entity.idSig = bro.idSig;
            entity.Ctor();
            ctx.BulletRepository.Add(entity);

            return entity;
        }
    }
}