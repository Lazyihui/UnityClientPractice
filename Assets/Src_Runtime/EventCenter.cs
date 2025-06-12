using System;
using UnityEngine;

namespace Game_Client {

    public class EventCenter {

        #region panel_login
        public Action OnLoginClickHandle;
        public void OnLoginClick() {
            OnLoginClickHandle?.Invoke();
        }
        #endregion
    }
}