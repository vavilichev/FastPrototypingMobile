using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.UI {
    public abstract class UIControllerBase : MonoBehaviour {

        #region DELEGATES

        public delegate void UIControllerStateHandler(UIControllerBase uiController);
        public event UIControllerStateHandler OnStateChangedEvent;

        #endregion
        

        #region CONSTANTS

        protected const string PREFABS_FOLDER = "UIElements";

        #endregion

        
        [SerializeField] protected List<UILayer> layers;
        
        protected Dictionary<Type, string> uiElementPathsMap;
        protected Dictionary<Type, IUIElement> uiCachedElementsMap;

        protected Routine routineInitializing;


        #region INITIALIZE

        public Coroutine Initialize() {
            this.routineInitializing = new Routine(InitializeRoutine, this);
            return routineInitializing.Start();
        }

        protected IEnumerator InitializeRoutine() {
            uiCachedElementsMap = new Dictionary<Type, IUIElement>();
            uiElementPathsMap = new Dictionary<Type, string>();
            
            UIElement[] uiElements = Resources.LoadAll<UIElement>(PREFABS_FOLDER);
            
            foreach (UIElement uiElement in uiElements) {
                if (uiElement is IUIPopup uiPopup) {
                    if (uiPopup.IsPreCached()) {
                        CreateAndCache(uiElement);
                        yield return null;
                    }
                }

                Type type = uiElement.GetType();
                uiElementPathsMap[type] = uiElement.name;
                yield return null;
            }

            Resources.UnloadUnusedAssets();
            this.OnInitialized();
            Logging.Log($"UI is initialized. Total elements count = {uiElementPathsMap.Count}, pre-cached = {uiCachedElementsMap.Count}");
        }

        protected void CreateAndCache<T>(T pref) where T : UIElement {
            var container = this.GetContainer(pref.layer);
            
            var createdElement = Instantiate(pref, container);
            createdElement.name = pref.name;
            
            var type = pref.GetType();
            uiCachedElementsMap[type] = createdElement;
            createdElement.HideInstantly();
        }

        protected Transform GetContainer(Layer layer) {
            foreach (UILayer uiLayer in this.layers) {
                if (uiLayer.layer == layer)
                    return uiLayer.container;
            }
            
            throw new Exception($"There is no {layer} layer in the UI.");
        }
        
        protected virtual void OnInitialized(){ }

        #endregion


        #region SHOW

        public T ShowElement<T>() where T : UIElement {
            Type type = typeof(T);
            if (uiCachedElementsMap.ContainsKey(type))
                return this.ShowCachedElement<T>(type);

            if (uiElementPathsMap.ContainsKey(type))
                return this.CreateAndShowElement<T>(type);
            
            Logging.LogError($"There is no element initialized in cached maps or paths. Type: {type}");
            return null;
        }

        protected T ShowCachedElement<T>(Type type) where T : UIElement {
            var uiElement = this.uiCachedElementsMap[type] as UIElement;
            uiElement.OnStateChangedEvent += this.OnStateChanged;
            uiElement.Show();
            return (T) uiElement;
        }

        protected T CreateAndShowElement<T>(Type type) where T : UIElement {
            var elementName = this.uiElementPathsMap[type];
            var path = $"{PREFABS_FOLDER}/{elementName}";
            var pref = Resources.Load<T>(path);
            var container = this.GetContainer(pref.layer);
            var createdElement = Instantiate(pref, container);
            createdElement.name = pref.name;
            createdElement.OnStateChangedEvent += this.OnStateChanged;
            createdElement.Show();

            Resources.UnloadUnusedAssets();
            return createdElement;
        }

        #endregion
        
       
        protected void OnStateChanged(UIElement uiElement, bool isActive) {
            this.OnStateChangedEvent?.Invoke(this);

            if (!isActive && uiElement is IUIPopup uiPopup && !uiPopup.IsPreCached())
                uiElement.OnStateChangedEvent -= this.OnStateChanged;
        }

        public bool HasAnyActivePopups() {
            foreach (UILayer layer in this.layers) {
                if (layer.HasAnyActivePopups())
                    return true;
            }

            return false;
        }
    }
}