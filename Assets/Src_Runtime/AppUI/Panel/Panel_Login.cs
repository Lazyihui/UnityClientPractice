using System;
using UnityEngine;
using UnityEngine.UI;


namespace Game_Client {


    public class Panel_Login : MonoBehaviour {
        [SerializeField] Button button_Login;
        public Action OnLoginClickHandle;


        public void Ctor() {
            button_Login.onClick.AddListener(() => {
                OnLoginClickHandle?.Invoke();
            });

        }


        public void TearDown() {
            GameObject.Destroy(gameObject);

        }

        public void Show() {
            gameObject.SetActive(true);
        }

    }
}