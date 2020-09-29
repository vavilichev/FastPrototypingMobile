using System;
using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIElement : MonoBehaviour, IUIElement {

        #region DELEGATES

        public delegate void UIElementHandler(UIElement uiElement);
        public event UIElementHandler OnElementShownEvent;
        public event UIElementHandler OnElementStartHideEvent;
        public event UIElementHandler OnElementHiddenCompletelyEvent;

        #endregion
        
        
        public bool isActive { get; protected set; } = true;
        public bool isInitialized { get; protected set; }
        public Transform myTransform { get; private set; }
        public List<IUIElement> childElements;


        public bool IsActive() {
            return this.isActive;
        }

        public bool IsInitialized() {
            return this.isInitialized;
        }


        #region LIFECYCLE

        protected void Awake() {
            this.myTransform = this.transform;
            this.RefreshChildsInfo();
            this.OnAwake();
        }
        
        protected virtual void OnAwake() { }

        protected void Start() {
            this.OnStart();
            this.isInitialized = true;
        }
        
        protected virtual void OnStart() { }


        
        protected void OnEnable() {
            this.OnEnabled();
            if (this.isInitialized) {
                this.OnEnabledInitialized();
                this.isActive = true;
            }
        }
        
        protected virtual void OnEnabled() { }
        protected virtual void OnEnabledInitialized() { }


        
        protected void OnDisable() {
            this.OnDisabled();
            if (this.isInitialized) {
                this.OnDisabledInitialized();
                this.isActive = false;
            }
        }

        protected virtual void OnDisabled() { }
        protected virtual void OnDisabledInitialized() { }

        #endregion


        public void RefreshChildsInfo() {
            if (this.childElements == null)
                this.childElements = new List<IUIElement>();
            else
                this.childElements.Clear();

            var childCount = myTransform.childCount;
            for (int i = 0; i < childCount; i++) {
                var child = myTransform.GetChild(i);
                var element = child.GetComponent<IUIElement>();
                if (element != null)
                    this.childElements.Add(element);
            }
        }


        public virtual void Show() {
            if (this.isActive)
                return;

            this.isActive = true;
            this.NotifyAboutElementShown();
        }

        public virtual void Hide() {
            if (!isActive)
                return;
            
            this.NotifyAboutElementStartHide();
            this.HideInstantly();
        }

        public virtual void HideInstantly() {
            if (!this.isActive)
                return;
            
            this.isActive = false;
            this.gameObject.SetActive(false);        
            this.NotifyAboutElementHiddenCompletely();
        }


        public T GetElement<T>() where T : IUIElement {
            foreach (var childElement in this.childElements) {
                if (childElement is T convertedElement)
                    return convertedElement;
            }

            throw new NullReferenceException(
                $"There is no child of type {typeof(T)} in the Element ({this.name}). Don't forget use RefreshChildsInfo when you change childs structure.");
        }

        public T[] GetElements<T>() where T : IUIElement {
            var requiredElements = new List<T>();
            foreach (var childElement in this.childElements) {
                if (childElement is T convertedElement)
                    requiredElements.Add(convertedElement);
            }

            return requiredElements.ToArray();
        }

        
        
        
        protected void NotifyAboutElementShown() {
            this.OnElementShownEvent?.Invoke(this);
        }

        protected void NotifyAboutElementStartHide() {
            this.OnElementStartHideEvent?.Invoke(this);
        }

        protected void NotifyAboutElementHiddenCompletely() {
            this.OnElementHiddenCompletelyEvent?.Invoke(this);
        }
        
    }
}