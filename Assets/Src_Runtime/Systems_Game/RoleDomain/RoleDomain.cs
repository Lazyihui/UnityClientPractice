using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Client {

    public static class RoleDomain {

        public static RoleEntity Spawn(GameSystemContext ctx) {
            RoleEntity role = GameFactory.Role_Create(ctx.assetsModule);

            // 临时来一个随机位置
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            role.transform.position = pos;

            ctx.RoleRepository.Add(role);
            return role;
        }

        public static void Input_Record(GameSystemContext ctx, RoleEntity role) {
            // (后面加入) InputComponent
            role.moveDir = ctx.inputModule.MoveDir;

        }

        public static void Move(RoleEntity role) {
            // 角色移动
            role.Move(role.moveDir);
        }
    }
}

