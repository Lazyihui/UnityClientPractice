using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Differences;
using Unity.VisualScripting;
using UnityEngine;

namespace Game_Client {
    public class RoleEntity : MonoBehaviour {
        // 要发送的消息
        public IDSignature idSig;
        public Vector2 moveDir;


        [SerializeField] Rigidbody2D rb;
        public int id;
        public int typeID;

        public float movespeed;

        public void Ctor() {
            movespeed = 5f;
        }

        public void SetPos(Vector3 pos){
            this.transform.position = pos;
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