using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAnim : MonoBehaviour {
   [SerializeField] private Transform visualTransform;
   [SerializeField] private Button btn;
   
   private void Reset() {
      if (!visualTransform)
         visualTransform = transform;
      if (!btn) {
         btn = gameObject.GetComponentInChildren<Button>();
         if (btn)
            btn.transition = Selectable.Transition.None;
      }
   }
}
