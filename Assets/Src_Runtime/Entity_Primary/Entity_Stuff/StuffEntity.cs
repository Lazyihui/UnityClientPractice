using System;
using MyTelepathy;
using UnityEngine;

namespace Game_Client {

    public class StuffEntity : MonoBehaviour {
        [SerializeField] Rigidbody2D rb;

        public IDSignature idSig;


        public void Ctor() {

        }

        public void SetPos(Vector3 pos) {
            // 1.设置物体位置
            transform.position = pos;
        }

        public void TearDown() {
            // 1.销毁物体
            Destroy(gameObject);
        }
    }
}