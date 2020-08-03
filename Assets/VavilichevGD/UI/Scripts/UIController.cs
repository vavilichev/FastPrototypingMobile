using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Tools;

namespace VavilichevGD.UI {
    public abstract class UIController : MonoBehaviour {

        #region CONSTANTS

        protected const string PREFABS_FOLDER = "UIElements";

        #endregion

        [SerializeField] protected List<UILayer> layers;
        
        public static Camera cameraUI { get; private set; }
        public static CanvasScaler canvasScaler { get; private set; }
        public static RectTransform rootRectTransform { get; private set; }
        public static UIController main { get; protected set; }
        
        protected Dictionary<Type, string> uiViewsPathsMap;
        protected Dictionary<Type, IUIView> uiCachedViewsMap;
        protected Dictionary<Type, IUIView> uiCreatedViewsMap;

        protected Routine routineInitializing;


        #region AWAKE

        private void Awake() {
            cameraUI = this.GetComponentInChildren<Camera>();
            canvasScaler = this.GetComponentInChildren<CanvasScaler>();
            rootRectTransform = canvasScaler.GetComponent<RectTransform>();
            this.OnAwake();
        }
        
        protected virtual void OnAwake() { }

        #endregion
        
        
        #region INITIALIZE

        public Coroutine Initialize() {
            if (this.routineInitializing != null)
                return this.routineInitializing.routine;
            
            this.routineInitializing = new Routine(InitializeRoutine, this);
            return routineInitializing.Start();
        }

        protected IEnumerator InitializeRoutine() {
            this.uiCachedViewsMap = new Dictionary<Type, IUIView>();
            this.uiViewsPathsMap = new Dictionary<Type, string>();
            this.uiCreatedViewsMap = new Dictionary<Type, IUIView>();
            
            var uiElements = Resources.LoadAll<UIView>(PREFABS_FOLDER);
            
            foreach (var uiElement in uiElements) {
                if (uiElement is IUIPopup uiPopup) {
                    if (uiPopup.isPreCached)
                        this.CreateAndCache(uiElement);
                }

                var type = uiElement.GetType();
                uiViewsPathsMap[type] = uiElement.name;
                yield return null;
            }

            Resources.UnloadUnusedAssets();
            this.OnInitialized();
            
            Logging.Log($"UI is initialized. Total elements count = {uiViewsPathsMap.Count}, pre-cached = {uiCachedViewsMap.Count}");
        }

        protected void CreateAndCache<T>(T pref) where T : UIView {
            var container = this.GetContainer(pref.layer);
            
            var createdElement = Instantiate(pref, container);
            createdElement.name = pref.name;
            
            var type = pref.GetType();
            this.uiCachedViewsMap[type] = createdElement;
            this.uiCreatedViewsMap[type] = createdElement;
            
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

        public T ShowElement<T>() where T : UIView {
            var type = typeof(T);
            if (this.uiCachedViewsMap.ContainsKey(type))
                return this.ShowCachedElement<T>(type);

            if (this.uiViewsPathsMap.ContainsKey(type))
                return this.CreateAndShowElement<T>(type);
            
            Logging.LogError($"There is no element initialized in cached maps or paths. Type: {type}");
            return null;
        }

        protected T ShowCachedElement<T>(Type type) where T : UIView {
            var uiElement = this.uiCachedViewsMap[type];
            uiElement.Show();
            return (T) uiElement;
        }

        protected T CreateAndShowElement<T>(Type type) where T : UIView {
            var elementName = this.uiViewsPathsMap[type];
            var path = $"{PREFABS_FOLDER}/{elementName}";
            var pref = Resources.Load<T>(path);
            var container = this.GetContainer(pref.layer);
            var createdElement = Instantiate(pref, container);
            createdElement.name = pref.name;
            createdElement.Show();

            this.uiCreatedViewsMap[type] = createdElement;
            Resources.UnloadUnusedAssets();
            return createdElement;
        }

        #endregion
        
        public bool HasAnyActivePopups() {
            var allViews = this.uiCachedViewsMap.Values;
            
            foreach (var view in allViews) {
                if (view is IUIPopup popup && popup.IsActive())
                    return true;
            }

            return false;
        }
        
        public object TryToGetView<T>() where T : IUIView {
            
            this.RefreshCreatedViewsMap();

            var requiredType = typeof(T);
            if (this.uiCreatedViewsMap.ContainsKey(requiredType))
                return this.uiCreatedViewsMap[requiredType];
            
            return null;
        }

        private void RefreshCreatedViewsMap() {
            foreach (var createdViewPair in this.uiCreatedViewsMap) {
                var key = createdViewPair.Key;
                var createdView = createdViewPair.Value;
                
                if (createdView == null)
                    this.uiCreatedViewsMap.Remove(key);
            }
        }
    }
}