using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VavilichevGD.Tools;

namespace VavilichevGD.UI.Extentions {
    public static class UIExtentionsRectTransform {

        #region CONSTANTS

        private const int CORNERS_COUNT = 4;

        #endregion


        #region VISIBILITY

        public static bool IsFullyVisible(this RectTransform rectTransform, RectTransform containerRectTransform, Camera camera, RectTransform rootRectTransform) {

            Vector3[] currentRectCorners = new Vector3[CORNERS_COUNT];
            rectTransform.GetWorldCorners(currentRectCorners);
            RectBounds currentRectBounds = new RectBounds(currentRectCorners, rectTransform.rect.size, camera, rootRectTransform);
            
            Vector3[] containerRectCorners = new Vector3[CORNERS_COUNT];
            containerRectTransform.GetWorldCorners(containerRectCorners);
            RectBounds containerRectBounds = new RectBounds(containerRectCorners, containerRectTransform.rect.size, camera, rootRectTransform);
            
            return containerRectBounds.ContainsFull(currentRectBounds);
        }

        public static bool IsVisible(this RectTransform rectTransform, RectTransform containerRectTransform, Camera camera, RectTransform rootRectTransform) {
            
            Vector3[] currentRectCorners = new Vector3[CORNERS_COUNT];
            rectTransform.GetWorldCorners(currentRectCorners);
            RectBounds currentRectBounds = new RectBounds(currentRectCorners, rectTransform.rect.size, camera, rootRectTransform);
            
            Vector3[] containerRectCorners = new Vector3[CORNERS_COUNT];
            containerRectTransform.GetWorldCorners(containerRectCorners);
            RectBounds containerRectBounds = new RectBounds(containerRectCorners, containerRectTransform.rect.size, camera, rootRectTransform);
            
            return containerRectBounds.ContainsAny(currentRectBounds);
        }

        #endregion


        #region RECALCULATE

        public static void RecalculateWithHorizontalFitterInside(this RectTransform rectTransform,
            ContentSizeFitter.FitMode fitMode) {
            ContentSizeFitter fitter = rectTransform.GetComponentInChildren<ContentSizeFitter>(true);
            fitter.horizontalFit = fitMode;

            HorizontalLayoutGroup hlg = rectTransform.GetComponentInParent<HorizontalLayoutGroup>();
            if (hlg)
                hlg.enabled = true;

            rectTransform.Recalculate(() => {
                if (fitter)
                    fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

                if (hlg)
                    hlg.enabled = false;
            });
        }

        public static void RecalculateWithVerticalFitterInside(this RectTransform rectTransform,
            ContentSizeFitter.FitMode fitMode) {
            ContentSizeFitter fitter = rectTransform.GetComponentInChildren<ContentSizeFitter>(true);
            fitter.verticalFit = fitMode;

            VerticalLayoutGroup vlg = rectTransform.GetComponentInParent<VerticalLayoutGroup>();
            if (vlg)
                vlg.enabled = true;

            rectTransform.Recalculate(() => {
                fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                if (vlg)
                    vlg.enabled = false;
            });
        }

        public static void Recalculate(this RectTransform rectTransform, UnityAction callback = null) {
            Coroutines.StartRoutine(RecalculateContainerRoutine(rectTransform, callback));
        }

        private static IEnumerator RecalculateContainerRoutine(RectTransform container, UnityAction callback = null) {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(container);
            callback?.Invoke();
        }

        #endregion
       
    }
}