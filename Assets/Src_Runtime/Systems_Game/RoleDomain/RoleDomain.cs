using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static class RoleDomain {

        public static RoleEntity OnSpawn(GameSystemContext ctx) {
            RoleEntity role = GameFactory.Role_Create(ctx.assetsModule, ctx.iDServer);
            ctx.RoleRepository.Add(role);
            role.roleName = ctx.roleName;
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            return role;
        }

        #region  OnMessage
        public static RoleEntity OnSpawnByBro(GameSystemContext ctx, SpawnRoleBroMessage bro) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = bro.roleName;
            role.SetPos(bro.pos);
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            ctx.RoleRepository.Add(role);

            return role;
        }

        public static RoleEntity OnSpawnByRes(GameSystemContext ctx, SpawnRoleResMessage res) {
            RoleEntity role = GameFactory.Role_CreateResBro(ctx.assetsModule, ctx.iDServer);
            role.roleName = res.roleName;
            role.SetPos(res.pos);
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

        #endregion
        public static void Input_Record(GameSystemContext ctx, RoleEntity role) {
            // (后面加入) InputComponent
            role.moveDir = ctx.inputModule.MoveDir;
        }

        public static void Input_Apply(GameSystemContext ctx, RoleEntity Owner, float dt) {
            // 角色输入应用

            if (Owner.moveDir != Vector2.zero) {
                Vector3 newPos = Owner.GetPos() + (Vector3)Owner.moveDir * Owner.movespeed * dt;

                // 发送移动消息
                MoveReqMessage req = new MoveReqMessage {
                    roleName = Owner.roleName,
                    targetPos = newPos,
                    timestamp = DateTime.UtcNow.Ticks
                };

                byte[] data = MessageHelper.ToData(req);
                ctx.client.Send(data);

                // 本地预测（立即更新位置）
                ctx.localPlayerPos = newPos;
                RoleDomain.UpdateLocalPlayerPos(ctx, newPos, Owner);
            }
        }

        public static void Move(RoleEntity role) {
            // 角色移动
            role.Move(role.moveDir);
        }
    }
}

