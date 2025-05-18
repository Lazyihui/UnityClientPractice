using System;
using Protocoles;
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

        #region  OnMessage
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
            ctx.RoleRepository.Add(role);
            return role;
        }

        public static void OnMove(GameSystemContext ctx, MoveBroMessage bro) {
            bool has = ctx.roleDict.TryGetValue(bro.roleName, out RoleEntity role);

            if (has) {
                role.SetPos(bro.targetPos);

                if (bro.roleName == ctx.roleName) {
                    ctx.localPlayerPos = bro.targetPos;
                    ctx.lastMoveTimestamp = bro.timestamp;
                }

            }

        }

        public static void UpdateLocalPlayerPos(GameSystemContext ctx, Vector3 newPos) {
            ctx.localPlayerPos = newPos;
            bool has = ctx.roleDict.TryGetValue(ctx.roleName, out RoleEntity role);
            if (has) {
                role.SetPos(newPos);
            }
        }

        #endregion
        public static void Input_Record(GameSystemContext ctx, RoleEntity role) {
            // (后面加入) InputComponent
            role.moveDir = ctx.inputModule.MoveDir;
        }

        public static void Input_Apply(GameSystemContext ctx, RoleEntity role, float dt) {
            // 角色输入应用


            if (role.moveDir != Vector2.zero) {
                Vector3 newPos = role.GetPos() + (Vector3)role.moveDir * role.movespeed * dt;

                // 发送移动消息
                MoveReqMessage req = new MoveReqMessage {
                    roleName = role.roleName,
                    targetPos = newPos,
                    timestamp = DateTime.UtcNow.Ticks
                };

                byte[] data = MessageHelper.ToData(req);
                ctx.client.Send(data);

                // 本地预测（立即更新位置）
                ctx.localPlayerPos = newPos;
                RoleDomain.UpdateLocalPlayerPos(ctx, newPos);
            }
        }

        public static void Move(RoleEntity role) {
            // 角色移动
            role.Move(role.moveDir);
        }
    }
}

