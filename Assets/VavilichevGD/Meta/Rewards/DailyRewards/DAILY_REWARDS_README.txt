
Daily rewards system

There are few attention points:
1. DailyRewardRepository and DailyRewardInteractor - two main things for implementation of the system. Firstly you must to initialize Repository, and than interactor. See example for details.
2. For correct work of the system you must place daily rewards in the Resources/DailyRewards/ folder or change path in the DailyRewardRepository script.
3. You should delete Resources folder in the DailyRewards folder because it can ruin your system. The examples of daily rewards are placed there.
4. For creating your own reward you should create reward script inherited from DailyReward.cs and handler script inherited from DailyRewardHandler (it will handle your reward). See example for details.
