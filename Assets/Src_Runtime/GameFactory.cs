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
          

            entity.Ctor();

            return entity;
        }
    }
}
