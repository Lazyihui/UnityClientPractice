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
            // 临时来一个随机位置
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            entity.SetPos(pos);

            entity.Ctor();

            return entity;
        }
    }
}
