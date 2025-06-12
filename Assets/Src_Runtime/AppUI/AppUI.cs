using System;
using UnityEngine;


namespace Game_Client {
    public class AppUI : MonoBehaviour {

        public UIContext ctx;
        public UIEvent uIEvent;

        public void Ctor() {

            ctx = new UIContext();
            uIEvent = new UIEvent();

        }

        public void Inject(AssetsModule assetsModule, Canvas canvas) {
            ctx.Inject(assetsModule, canvas);
        }

        #region Panel_Login
        public void Panel_Login_Open() {
            Panel_Login panel = ctx.panel_Login;
            if (panel == null) {
                GameObject prefab = ctx.assetsModule.Panel_GetLogin();
                GameObject go = GameObject.Instantiate(prefab, ctx.canvas.transform);
                panel = go.GetComponent<Panel_Login>();
                panel.Ctor();

                panel.OnLoginClickHandle += () => {
                    uIEvent.OnLoginClick();
                };

            }
            ctx.panel_Login = panel;
            panel.Show();
        }

        public void Panel_Login_Close() {
            Panel_Login panel = ctx.panel_Login;
            if (panel == null) {
                return;
            }   
            panel.TearDown();
            ctx.panel_Login = null;
        }

        #endregion 



    }
}
