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
            Debug.Log($"RoleDomain.OnSpawn: {role.id} {role.roleName}" + "位置" + role.GetPos());
            ctx.roleRepository.Add(role);

            return role;
        }

        public static void OnDestory(GameSystemContext ctx, RoleEntity entity) {
            // 1.销毁角色
            entity.TearDown();
            ctx.roleRepository.Remove(entity);

        }

        public static RoleEntity OnSpawnByRes(GameSystemContext ctx, SpawnRoleResMessage res) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = res.roleName;
            role.SetPos(res.pos);
            role.roleType = res.roleType;
            Debug.Log($"RoleDomain.OnSpawn: {role.id} {role.roleName}" + "位置" + role.GetPos());
            ctx.roleRepository.Add(role);
            return role;
        }

        public static void UnSpawnByBro(GameSystemContext ctx, int idSig) {

            if (!ctx.roleRepository.TryGet(idSig, out RoleEntity role)) {
                Debug.LogWarning($"RoleDomain.UnSpawnByBro: 找不到角色实体 {idSig}");
                return;
            }

            // 1.销毁角色
            OnDestory(ctx, role);
        }

        public static void OnMove(GameSystemContext ctx, MoveBroMessage bro) {

            ctx.roleRepository.TryGetByString(bro.roleName, out RoleEntity Role);

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


    }
}