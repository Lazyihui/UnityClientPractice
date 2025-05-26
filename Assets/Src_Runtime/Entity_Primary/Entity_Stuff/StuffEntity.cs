using System;
using MyTelepathy;
using UnityEngine;

namespace Game_Client {

    public class StuffEntity : MonoBehaviour {
        [SerializeField] Rigidbody2D rb;

        public int idSig;


        public void Ctor() {

        }

        public void SetPos(Vector3 pos) {
            // 1.设置物体位置
            transform.position = pos;
        }

        public Vector3 GetPos() {
            // 1.获取物体位置
            return transform.position;
        }

        public void TearDown() {
            // 1.销毁物体
            Destroy(gameObject);
        }
    }
}