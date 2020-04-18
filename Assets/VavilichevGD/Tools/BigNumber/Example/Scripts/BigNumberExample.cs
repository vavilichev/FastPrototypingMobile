using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.Tools {
    public class BigNumberExample : MonoBehaviour {

        [SerializeField] private BigNumber defaultValue;
        [Space]
        [SerializeField] private Text textFullValue;
        [SerializeField] private Text textShortValue;
        [Space] 
        [SerializeField] private Button btnX2;
        [SerializeField] private Button btnX3;
        [SerializeField] private Button btnX1_2;
        [SerializeField] private Button btnX1_75;

        private BigNumber value;


        private void Awake() {
            Initialize();
            UpdateView();
        }

        private void Initialize() {
            value = defaultValue;
        }
        

        private void OnEnable() {
            btnX2.onClick.AddListener(OnX2BtnClick);
            btnX3.onClick.AddListener(OnX3BtnClick);
            btnX1_2.onClick.AddListener(OnX1_2BtnClick);
            btnX1_75.onClick.AddListener(OnX1_75BtnClick);
        }

        private void OnX2BtnClick() {
            value = value * 2;
            UpdateView();
        }

        private void OnX3BtnClick() {
            value = value * 3;
            UpdateView();
        }

        private void OnX1_2BtnClick() {
            value = value * 1.2f;
            UpdateView();
        }

        private void OnX1_75BtnClick() {
            value = value * 1.75f;
            UpdateView();
        }

        private void UpdateView() {
            textFullValue.text = value.ToStringFull();
            textShortValue.text = value.ToStringShort();
        }

        private void OnDisable() {
            btnX2.onClick.RemoveListener(OnX2BtnClick);
            btnX3.onClick.RemoveListener(OnX3BtnClick);
            btnX1_2.onClick.RemoveListener(OnX1_2BtnClick);
            btnX1_75.onClick.RemoveListener(OnX1_75BtnClick);
        }
    }
}