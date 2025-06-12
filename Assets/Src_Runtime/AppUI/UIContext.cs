using System;
using UnityEngine;

namespace Game_Client {

    public class UIContext {

        // panel 
        public Panel_Login panel_Login;
        // module 
        public AssetsModule assetsModule;

        public Canvas canvas;
        public void Inject(AssetsModule assetsModule,Canvas canvas) {

            this.assetsModule = assetsModule;
            this.canvas = canvas;

        }

    }
}