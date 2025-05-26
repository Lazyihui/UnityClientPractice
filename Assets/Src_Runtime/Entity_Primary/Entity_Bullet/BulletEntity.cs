using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public class BulletEntity : MonoBehaviour {
        
        public int idSig;
        
        public void Ctor() {

        }

        public void SetPos(Vector3 pos) {
            transform.position = pos;
        }

        public Vector3 GetPos() {
            return transform.position;
        }

        public void TearDown() {
            Destroy(gameObject);
        }
    }
}