using System;
using UnityEngine;

namespace Game_Client {

    public class UIContext {

        // panel 
        public Panel_Login panel_Login;
        // module 
        public AssetsModule assetsModule;
        public void Inject(AssetsModule assetsModule) {

            this.assetsModule = assetsModule;

        }

    }
}