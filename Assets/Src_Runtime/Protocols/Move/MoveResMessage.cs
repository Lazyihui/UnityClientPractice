using System;
using UnityEngine;

namespace Game_Client {
    public struct MoveReqMessage {
        public string roleName;
        public Vector3 targetPos;
        public long timestamp;  // 用于客户端预测
    }
}