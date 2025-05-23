using System;
using UnityEngine;
using MyTelepathy;

namespace Game_Client {

    public static partial class RoleDomain {

        public static RoleEntity OnSpawn(GameSystemContext ctx) {
            RoleEntity role = GameFactory.Role_Create(ctx.assetsModule, ctx.iDServer);
            ctx.RoleRepository.Add(role);
            role.roleName = ctx.roleName;
            Debug.Log($"RoleDomain.OnSpawn: {role.idSig} {role.roleName}" + "位置" + role.GetPos());
            return role;
        }
       
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

