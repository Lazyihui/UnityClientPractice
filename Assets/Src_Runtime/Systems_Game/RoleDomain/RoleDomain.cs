using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Client {

    public static class RoleDomain {

        public static RoleEntity OnSpawn(GameSystemContext ctx) {
            RoleEntity role = GameFactory.Role_Create(ctx.assetsModule, ctx.iDServer);
            ctx.RoleRepository.Add(role);
            role.roleName = ctx.roleName;
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            return role;
        }

        public static RoleEntity OnSpawnByBro(GameSystemContext ctx, SpawnRoleBroMessage bro) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = bro.roleName;
            role.SetPos(bro.pos);
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());

            return role;
        }

        public static RoleEntity OnSpawnByRes(GameSystemContext ctx, SpawnRoleResMessage res) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = res.roleName;
            role.SetPos(res.pos);
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());

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

