using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Game_Client {

    public class InputModule : MonoBehaviour {

        InputController_Player player;

        public InputController_Player Player => player;

        Vector3 moveDir;
        public Vector3 MoveDir => moveDir;

        public void Ctor() {
            player = new InputController_Player();
            player.Enable();
        }


        public void Process(float dt) {
            var world = player.World;
            {
                moveDir.x = world.MoveRight.ReadValue<float>() - world.MoveLeft.ReadValue<float>();
                moveDir.y = world.MoveUp.ReadValue<float>() - world.MoveDown.ReadValue<float>();
            }
        }

    }
}