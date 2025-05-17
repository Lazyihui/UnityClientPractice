using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Client {

    public static class GameFactory {

        public static RoleEntity Role_Create(AssetsModule assetsModule, IDServer iDServer) {
            Debug.Assert(assetsModule != null, "assetsModule is null");
            GameObject prefab = assetsModule.Entity_GetRole();
            GameObject go = GameObject.Instantiate(prefab);
            RoleEntity entity = go.GetComponent<RoleEntity>();
            entity.idSig = new IDSignature(EntityType.Role, iDServer.PickRoleID());

            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0f);

            entity.SetPos(pos);


            entity.Ctor();

            return entity;
        }

        public static RoleEntity Role_CreateResBro(AssetsModule assetsModule, IDServer iDServer) {
            Debug.Assert(assetsModule != null, "assetsModule is null");
            GameObject prefab = assetsModule.Entity_GetRole();
            GameObject go = GameObject.Instantiate(prefab);
            RoleEntity entity = go.GetComponent<RoleEntity>();


            entity.Ctor();

            return entity;
        }
    }
}
