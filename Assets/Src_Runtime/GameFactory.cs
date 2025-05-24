using System;
using System.Collections.Generic;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static class GameFactory {

        public static RoleEntity Role_CreateResBro(AssetsModule assetsModule, IDServer iDServer) {
            Debug.Assert(assetsModule != null, "assetsModule is null");
            GameObject prefab = assetsModule.Entity_GetRole();
            GameObject go = GameObject.Instantiate(prefab);
            RoleEntity entity = go.GetComponent<RoleEntity>();
            entity.idSig = new IDSignature(EntityType.Role, iDServer.PickRoleID());

            entity.Ctor();

            return entity;
        }

        public static BulletEntity Bullet_CreateResBro(AssetsModule assetsModule, IDServer iDServer, Transform rootPos) {
            Debug.Assert(assetsModule != null, "assetsModule is null");
            GameObject prefab = assetsModule.Entity_GetBullet();
            GameObject go = GameObject.Instantiate(prefab, rootPos);
            BulletEntity entity = go.GetComponent<BulletEntity>();
            entity.idSig = new IDSignature(EntityType.Bullet, 0);
            Debug.Log(entity.idSig.entityID);

            entity.Ctor();

            return entity;

        }
    }
}
