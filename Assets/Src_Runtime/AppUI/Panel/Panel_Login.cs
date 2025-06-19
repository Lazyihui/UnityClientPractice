using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game_Client {

    public class Panel_Login : MonoBehaviour {
        [SerializeField] Button button_Login;
        public Action OnLoginClickHandle;

        // [SerializeField] TMP_InputField inputField_Account;

        string text_name;
        public void Ctor() {
            button_Login.onClick.AddListener(() => {
                OnLoginClickHandle?.Invoke();
            });
            // text_name = inputField_Account.text;
        }

        public void Set_name() {
            // text_name = inputField_Account.text;
        }

        public void TearDown() {
            GameObject.Destroy(gameObject);

        }

        public void Show() {
            gameObject.SetActive(true);
        }

    }
}