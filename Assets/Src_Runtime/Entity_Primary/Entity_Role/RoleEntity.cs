using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game_Client {
    public class RoleEntity : MonoBehaviour {

        [SerializeField] Rigidbody2D rb;

        public IDSignature idSig;
        public int id;
        public int typeID;

        public float movespeed;
        public Vector2 moveDir;

        public void Ctor() {
            movespeed = 5f;
        }

        public void Move(Vector3 dir) {
            dir.Normalize();
            var oldVelocity = rb.velocity;
            oldVelocity.x = dir.x * movespeed;
            oldVelocity.y = dir.y * movespeed;
            rb.velocity = oldVelocity;
        }

    }
}