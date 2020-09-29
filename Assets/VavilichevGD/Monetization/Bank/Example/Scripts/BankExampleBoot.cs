using System;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Monetization.Examples {
    public class BankExampleBoot : MonoBehaviour {
        private void Start() {
            GameBankExample.Run();
            Game.OnGameInitializedEvent += OnGameInitializedEvent;
            LoadingScreen.Show();
        }

        private void OnGameInitializedEvent() {
            Game.OnGameInitializedEvent -= OnGameInitializedEvent;
            LoadingScreen.Hide();
        }

        private void OnApplicationQuit() {
            var bankRepo = Game.GetRepository<BankRepository>();
            bankRepo.Save();
        }
    }
}