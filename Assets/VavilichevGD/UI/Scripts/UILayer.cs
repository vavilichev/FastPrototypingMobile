﻿using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.UI {
    public enum Layer {
        Screen,
        FXUnderPopup,
        Popup,
        FXOverPopup
    }
    
    public class UILayer : MonoBehaviour {

        [SerializeField] private Layer m_layer;

        public Layer layer => m_layer;
        public Transform container => this.transform;
        
        public IUIView[] GetAllUIViews() {
            var uiViews = new List<IUIView>();
            var childCount = this.container.childCount;

            for (int i = 0; i < childCount; i++) {
                var viewTransform = this.container.GetChild(i);
                var view = viewTransform.GetComponent<IUIView>();
                if (view != null)
                    uiViews.Add(view);
            }
            
            return uiViews.ToArray();
        }
        
    }
}