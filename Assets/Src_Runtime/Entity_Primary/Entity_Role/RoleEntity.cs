using System;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Differences;
using Unity.VisualScripting;
using UnityEngine;
using MyTelepathy;


namespace Game_Client {
    public class RoleEntity : MonoBehaviour {

        public RoleType roleType;
        public string roleName;

        // 要发送的消息
        public int id;
        public Vector2 moveDir;


        [SerializeField] Rigidbody2D rb;
        public float movespeed;

        [SerializeField] Transform bulletRoot;
        public Transform BulletRoot => bulletRoot;

        public void Ctor() {
            movespeed = 18f;
        }

        public void SetPos(Vector3 pos) {
            this.transform.position = pos;
        }

        public Vector3 GetPos() {
            return this.transform.position;
        }

        public void Move(Vector3 dir) {
            dir.Normalize();
            var oldVelocity = rb.velocity;
            oldVelocity.x = dir.x * movespeed;
            oldVelocity.y = dir.y * movespeed;
            rb.velocity = oldVelocity;
        }

        public void TearDown() {
            // 销毁角色
            Destroy(gameObject);
        }

    }
}