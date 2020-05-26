using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    public enum Layer {
        Screen,
        Popup,
        Tutorial,
        Warning,
        FX
    }
    
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UILayer : MonoBehaviour {

        [SerializeField] private Layer m_layer;

        public Layer layer => m_layer;
        public Transform container => this.transform;
        
        protected Canvas canvas;

        protected void Awake() {
            this.canvas = this.gameObject.GetComponent<Canvas>();
        }

        public void SetActive(bool isActive) {
            if (isActive) {
                this.gameObject.SetActive(true);
                this.canvas.enabled = true;
            }
            else {
                this.canvas.enabled = false;
                this.gameObject.SetActive(false);
            }
        }

        public bool HasAnyActivePopups() {
            IUIPopup[] uiPopups = this.gameObject.GetComponentsInChildren<IUIPopup>();
            foreach (var popup in uiPopups) {
                if (popup.IsActive())
                    return true;
            }

            return false;
        }

        public IUIElement[] GetAllUIElemnets() {
            List<IUIElement> uiElements = new List<IUIElement>();
            foreach (Transform child in transform) {
                IUIElement uiElement = child.GetComponent<IUIElement>();
                uiElements.Add(uiElement);
            }

            return uiElements.ToArray();
        }
    }
}