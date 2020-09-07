using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.Meta.DefferedRewards.Example {
    public class DefferedRewardsExample : MonoBehaviour {

        [SerializeField] private DefferedRewardExampleUIController rewardControler1;
        [SerializeField] private DefferedRewardExampleUIController rewardControler2;
        
        
        private DefferedRewardsInteractor defferedRewardsInteractor;

        private IEnumerator Start() {
            this.defferedRewardsInteractor = new DefferedRewardsInteractor();
            yield return this.defferedRewardsInteractor.InitializeAsync();
            
            this.rewardControler1.SetInteractor(this.defferedRewardsInteractor);
            this.rewardControler2.SetInteractor(this.defferedRewardsInteractor);
            this.Resetup();
        }

        private void Resetup() {
            
            if (this.defferedRewardsInteractor.AnyDefferedRewardsIsActive()) {
                List<DefferedReward> activeRewards = this.defferedRewardsInteractor.activeRewardsList;

                int count = activeRewards.Count;
                var reward1 = activeRewards[0];
                var reward2 = count == 2 ? activeRewards[0] : null;
                
                this.rewardControler1.Setup(reward1);
                this.rewardControler2.Setup(reward2);
            }
            else {
                this.rewardControler1.Setup();
                this.rewardControler2.Setup();
            }
        }
    }
}