using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Monetization.Examples {
    public class BankExampleBoot : MonoBehaviour {
        private void Start() {
            GameBankExample.Run();
            Game.OnGameInitializedEvent += OnGameInitializedEvent;
        }

        private void OnGameInitializedEvent() {
            Game.OnGameInitializedEvent -= OnGameInitializedEvent;
        }
    }
}