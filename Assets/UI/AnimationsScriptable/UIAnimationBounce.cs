using System.Collections;
using UnityEngine;

namespace VavilichevGD.UI {
    public class UIAnimationBounce : MonoBehaviour {

        public Transform transformTarget;
        public AnimationCurve scaleCurve = AnimationCurve.Constant(0f, 1f, 1f);
        public float speed = 1f;
        public float restDuration;
        public bool loop = true;
        public bool startWithRestTime;

        
        private void OnEnable() {
            this.StartCoroutine("LifeRoutine");
        }

        private void OnDisable() {
            this.StopAllCoroutines();
        }
        

        private IEnumerator LifeRoutine() {
            if (this.startWithRestTime)
                yield return new WaitForSeconds(this.restDuration);

            while (true) {
                yield return this.StartCoroutine("AnimationRoutine");
                
                if (this.restDuration > 0f)
                    yield return new WaitForSeconds(this.restDuration);
                
                if (!loop)
                    yield break;
            }
        }

        private IEnumerator AnimationRoutine() {
            var timer = 0f;
            var duration = 1f / this.speed;
            while (timer < 1f) {
                timer = Mathf.Min(timer + Time.deltaTime / duration, 1f);
                var newScale = this.scaleCurve.Evaluate(timer);
                this.transformTarget.localScale = Vector3.one * newScale;
                yield return null;
            }
        }
        
        
        #if UNITY_EDITOR
        private void Reset() {
            if (this.transformTarget == null)
                this.transformTarget = this.transform;
        }
        #endif
    }
}