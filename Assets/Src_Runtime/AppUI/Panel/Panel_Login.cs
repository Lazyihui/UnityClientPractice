using System;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

namespace Game_Client {

    public class Panel_Login : MonoBehaviour {
        [SerializeField] Button button_Login;
        public Action OnLoginClickHandle;

        // [SerializeField] TextMeshProGUI text_Title;


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