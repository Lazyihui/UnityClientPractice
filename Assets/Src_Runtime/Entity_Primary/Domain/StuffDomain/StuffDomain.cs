using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static class StuffDomain {

        public static StuffEntity SpawnStuffByBro(GameSystemContext ctx, StuffSpawnBroMessage bro) {
            var entity = GameFactory.Stuff_CreateResBro(ctx.assetsModule, ctx.iDServer);
            entity.idSig = bro.idSig;
            entity.SetPos(bro.pos);

            ctx.StuffRepository.Add(entity);
            return entity;
        }
    }
}