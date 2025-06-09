using System;
using UnityEngine;

namespace Game_Client{

    public class UIEvent {

        #region Login

        public Action OnLoginClickHandle;
        public void OnLoginClick() {
            OnLoginClickHandle?.Invoke();
        }

        #endregion
    }
}