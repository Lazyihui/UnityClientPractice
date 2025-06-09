using System;
using UnityEngine;


namespace Game_Client {
    public class AppUI : MonoBehaviour {

        public UIContext uIContext;
        public UIEvent uIEvent;

        public void Ctor() {

            uIContext = new UIContext();
            uIEvent = new UIEvent();

        }

        public void Inject(AssetsModule assetsModule) {

        }


    }
}
