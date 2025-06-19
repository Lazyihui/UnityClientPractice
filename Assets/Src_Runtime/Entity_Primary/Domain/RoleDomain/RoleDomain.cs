using System;
using UnityEngine;
using MyTelepathy;
using UnityEngine.Rendering;
namespace Game_Client {

    public static partial class RoleDomain {
        public static bool IsUsingURP() {
            // 如果 GraphicsSettings.currentRenderPipeline 不为 null，说明正在使用 URP/HDRP
            return GraphicsSettings.currentRenderPipeline != null;
        }

        public static string GetCurrentRenderPipeline() {
            var pipeline = GraphicsSettings.currentRenderPipeline;
            if (pipeline == null) {
                return "Built-in RP (0代)";
            } else if (pipeline.GetType().Name.Contains("UniversalRenderPipelineAsset")) {
                return "URP (1代)";
            } else if (pipeline.GetType().Name.Contains("HDRenderPipelineAsset")) {
                return "HDRP (1代)";
            }
            return "Unknown";
        }

        public static void Input_Record(GameSystemContext ctx, RoleEntity role) {
            // (后面加入) InputComponent
            role.moveDir = ctx.inputModule.MoveDir;
        }

        public static void Input_Apply(GameSystemContext ctx, RoleEntity Owner, float dt) {
            // 角色输入应用

            if (Owner.moveDir != Vector2.zero) {

                Vector3 newPos = Owner.GetPos() + (Vector3)Owner.moveDir * Owner.movespeed * dt;

                // // 发送移动消息
                MoveReqMessage req = new MoveReqMessage();
                req.Init(ctx.roleName, newPos);


                byte[] data = MessageHelper.ToData(req);
                ctx.AddToPool(data);

                ctx.client.Send(data);
                // 本地预测（立即更新位置）
                ctx.localPlayerPos = newPos;
                RoleDomain.UpdateLocalPlayerPos(ctx, newPos, Owner);
            }
        }

        public static void UpdateLocalPlayerPos(GameSystemContext ctx, Vector3 newPos, RoleEntity owner) {

            ctx.localPlayerPos = newPos;
            owner.SetPos(newPos);
        }

    }
}

