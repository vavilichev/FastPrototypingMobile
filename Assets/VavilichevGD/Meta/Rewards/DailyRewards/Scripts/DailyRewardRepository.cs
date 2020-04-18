using System.Collections;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardRepository : Repository {

        protected const string RESOURCES_PATH = "DailyRewards";
        protected const string PREF_KEY_LAST_REWARD_DATA = "DAILY_REWARD_DATA";

        protected string[] dailyRewardInfoFileNames;
        
        
        public DailyRewardsData lastDailyRewardsData { get; protected set; }
        public int rewardsCount => dailyRewardInfoFileNames.Length;
        
        protected override IEnumerator InitializeRoutine() {
            InitDailyRewardInfoFileNames();
            LoadFromStorage();
            yield break;
        }

        protected virtual void InitDailyRewardInfoFileNames() {
            DailyRewardInfo[] rewardsInfo = Resources.LoadAll<DailyRewardInfo>(RESOURCES_PATH);
            dailyRewardInfoFileNames = new string[rewardsInfo.Length];
            for (int index = 0; index < rewardsInfo.Length; index++)
                dailyRewardInfoFileNames[index] = rewardsInfo[index].name;
            Resources.UnloadUnusedAssets();
            Logging.Log($"REPOSITORY DAILY REWARD: initialized {dailyRewardInfoFileNames.Length} names of daily reward info");
        }

        protected override void LoadFromStorage() {
            lastDailyRewardsData = Storage.GetCustom(PREF_KEY_LAST_REWARD_DATA, DailyRewardsData.defaultValue);
            Logging.Log($"REPOSITORY DAILY REWARD: loaded data. Last date = " +
                      $"{lastDailyRewardsData.dailyRewardReceivedTimeSerialized}, the day index is " +
                      $"{lastDailyRewardsData.dailyRewardsDayIndex}");
        }


        public void SetLastDailyRewardData(DailyRewardsData newDailyRewardDate) {
            lastDailyRewardsData = newDailyRewardDate;
        }

        public override void Save() {
            this.SaveToStorage();
        }

        protected override void SaveToStorage() {
            Storage.SetCustom(PREF_KEY_LAST_REWARD_DATA, lastDailyRewardsData);
            Logging.Log($"REPOSITORY DAILY REWARD: Saved data. Last date = " +
                      $"{lastDailyRewardsData.dailyRewardReceivedTimeSerialized}, the day index is " +
                      $"{lastDailyRewardsData.dailyRewardsDayIndex}");
        }

        public DailyRewardInfo GetRewardInfo(int index) {
            Resources.UnloadUnusedAssets();
            if (index < 0 || index >= dailyRewardInfoFileNames.Length) {
                Logging.LogError($"REPOSITORY DAILY REWARD: There is no deaily reward info scriptable object " +
                               $"in the DailyRewards folder with index ({index})");
                return null;
            }

            string fileName = dailyRewardInfoFileNames[index];
            DailyRewardInfo loadedInfo = Resources.Load<DailyRewardInfo>($"{RESOURCES_PATH}/{fileName}");
            return loadedInfo;
        }
        
        public virtual int GetNextRewardDayIndex(DailyRewardsData lastRewardData) {
            int totalRewardsCount = dailyRewardInfoFileNames.Length;
            int nextDayIndex = lastRewardData.dailyRewardsDayIndex + 1;
//            if (nextDayIndex >= totalRewardsCount)
//                nextDayIndex = 0;
            return nextDayIndex;
        }

        public override void Clean() {
            Storage.ClearKey(PREF_KEY_LAST_REWARD_DATA);
            Logging.Log($"REPOSITORY DAILY REWARD: The pref key ({PREF_KEY_LAST_REWARD_DATA}) was cleaned");
        }
    }
}