// Request(Req): 客户端 发-> 服务端

using System;
using UnityEngine;

namespace Game_Client {

    public struct SpawnRoleBroMessage {
        public RoleType roleType;

        public string roleName;

        public IDSignature idSig;

        public Vector2 pos;


    }

}