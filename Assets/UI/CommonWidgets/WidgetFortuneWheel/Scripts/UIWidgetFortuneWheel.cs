using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UIWidgetFortuneWheel : UIWidget {

        [SerializeField] private UIWidgetFortuneWheelProperties properties;
        private FortuneWheelInteractorFreeAndAD interactor;

        private Dictionary<string, Reward> rewardsMap;

        protected override void OnStart() {
            var config = this.properties.fortuneWheel.config;
            var sectorsData = config.GetSectorsData();
            var widgets = this.gameObject.GetComponentsInChildren<UIWidgetFortuneWheelSectorText>();
            
            this.rewardsMap = new Dictionary<string, Reward>();
            foreach (var wheelSectorData in sectorsData) {
                var reward = new Reward(wheelSectorData.rewardInfo);
                this.rewardsMap[reward.id] = reward;
            }

            if (sectorsData.Length != widgets.Length)
                throw new Exception("Count of sectors data does not equals to ui widgets count");

            this.interactor = new FortuneWheelInteractorFreeAndAD(this.properties.fortuneWheel);
            
            int count = sectorsData.Length;
            var rewards = this.rewardsMap.Values.ToArray();
            for (int i = 0; i < count; i++)
                widgets[i].Setup(rewards[i]);
            
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

        private Reward GetReward(string id) {
            return this.rewardsMap[id];
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

        private void OnFortuneWheelRewardReceived(FortuneWheel fortunewheel, string rewardInfoId) {
            this.properties.fortuneWheel.OnRewardReceivedEvent -= OnFortuneWheelRewardReceived;
            var reward = this.GetReward(rewardInfoId);
            reward.Apply();
            Debug.Log($"Reward: {reward.info}");
        }

        #endregion
    }
}