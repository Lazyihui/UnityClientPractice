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

            entity.Ctor();
            return entity;

        }

        public static StuffEntity Stuff_CreateResBro(AssetsModule assetsModule, IDServer iDServer) {
            Debug.Assert(assetsModule != null, "assetsModule is null");
            GameObject prefab = assetsModule.Entity_GetStuff();
            GameObject go = GameObject.Instantiate(prefab);
            StuffEntity entity = go.GetComponent<StuffEntity>();

            entity.Ctor();

            return entity;
        }
    }
}
