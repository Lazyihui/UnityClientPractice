using System;
using UnityEngine;

namespace Game_Client {

    public class BulletEntity : MonoBehaviour {
        public IDSignature idSig;

        public void Ctor() {

        }

        public void SetPos(Vector3 pos) {
            transform.position = pos;
        }

        public void TearDown() {
            Destroy(gameObject);
        }
    }
}