using UnityEngine;
using VavilichevGD.Meta;

public class TestScript : MonoBehaviour {
    [SerializeField] private RewardInfo rewardInfo1;
    [SerializeField] private RewardInfo rewardInfo2;

    private void Start() {
        Reward reward1 = new Reward(this.rewardInfo1);
        Debug.Log(reward1.info);
        Reward reward2 = new Reward(this.rewardInfo2);
        Debug.Log(reward2.info);
    }
}
