using System;
using UnityEngine;
using VavilichevGD.UI;


namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UIWidgetFortuneWheel : UIWidget<UIWidgetFortuneWheelProperties> {

        private FortuneWheelInteractorFreeAndAD interactor;

        protected override void OnStart() {
            FortuneWheelConfig config = this.properties.fortuneWheel.config;
            FortuneWheelSectorData[] sectorsData = config.GetSectorsData();
            UIWidgetFortuneWheelSectorText[] widgets =
                this.gameObject.GetComponentsInChildren<UIWidgetFortuneWheelSectorText>();

            if (sectorsData.Length != widgets.Length)
                throw new Exception("Count of sectors data does not equals to ui widgets count");

            this.interactor = new FortuneWheelInteractorFreeAndAD(this.properties.fortuneWheel);
            
            int count = sectorsData.Length;
            for (int i = 0; i < count; i++)
                widgets[i].Setup(sectorsData[i].rewardInfo);
            
            this.UpdateButtonRotate();
        }

        protected override void OnEnabled() {
            this.properties.btnRotate.AddListener(this.OnRotateBtnClick);
            this.UpdateButtonRotate();
        }

        protected override void OnDisabled() {
            this.properties.btnRotate.RemoveListener(this.OnRotateBtnClick);
        }

        private void UpdateButtonRotate() {
            if (this.interactor == null)
                return;
            
            string textBtnRotate = this.interactor.freeSpinAvailable ? "Rotate (free)" : "Rotate (for AD)";
            this.properties.textBtnRotate.text = textBtnRotate;
        }
        
        #region EVENTS

        private void OnRotateBtnClick() {
            this.interactor.Purchase(success => {
                if (success)
                    this.Rotate();
            });
        }

        private void Rotate() {
            this.properties.btnRotate.interactable = false;
                    
            var fortuneWheel = this.properties.fortuneWheel;
            fortuneWheel.OnRewardReceivedEvent += OnFortuneWheelRewardReceived;
            fortuneWheel.OnRotateOverEvent += OnFortuneWheelRotateOver;
            fortuneWheel.Rotate();
        }

        private void OnFortuneWheelRotateOver(FortuneWheel fortunewheel) {
            this.properties.fortuneWheel.OnRotateOverEvent -= OnFortuneWheelRotateOver;
            this.properties.btnRotate.interactable = true;
            this.UpdateButtonRotate();
        }

        private void OnFortuneWheelRewardReceived(FortuneWheel fortunewheel, RewardInfo rewardInfo) {
            this.properties.fortuneWheel.OnRewardReceivedEvent -= OnFortuneWheelRewardReceived;
            Reward reward = new Reward(rewardInfo);
            reward.Apply();
            Debug.Log($"Reward: {rewardInfo}");
        }

        #endregion
    }
}