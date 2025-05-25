using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static partial class RoleDomain {
        public static RoleEntity OnSpawnByBro(GameSystemContext ctx, SpawnRoleBroMessage bro) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = bro.roleName;
            role.SetPos(bro.pos);
            role.roleType = bro.roleType;
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            ctx.RoleRepository.Add(role);

            return role;
        }

        public static RoleEntity OnSpawnByRes(GameSystemContext ctx, SpawnRoleResMessage res) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = res.roleName;
            role.SetPos(res.pos);
            role.roleType = res.roleType;
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            ctx.RoleRepository.Add(role);
            return role;
        }

        public static void OnMove(GameSystemContext ctx, MoveBroMessage bro) {

            ctx.RoleRepository.TryGetByString(bro.roleName, out RoleEntity Role);

            if (Role == null) {
                Debug.LogError($"RoleDomain.OnMove: 未找到角色 {bro.roleName}");
                return;
            }

            Role.SetPos(bro.targetPos);
            if (bro.roleName == ctx.roleName) {
                ctx.localPlayerPos = bro.targetPos;
                ctx.lastMoveTimestamp = bro.timestamp;
            }

        }

        public static void UpdateLocalPlayerPos(GameSystemContext ctx, Vector3 newPos, RoleEntity owner) {

            ctx.localPlayerPos = newPos;
            owner.SetPos(newPos);
        }


    }
}