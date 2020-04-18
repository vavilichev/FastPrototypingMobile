using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public static class Bank {

        public static bool isInitialized => interactor != null;

        private static BankInteractor interactor;

        public static event BankStateChangeHandler OnBankStateChangedEvent;

        public static void Initialize(BankInteractor _interactor) {
            interactor = _interactor;
            interactor.OnBankStateChanged += () => { OnBankStateChangedEvent?.Invoke(); };
            Logging.Log($"BANK: Initialized.");
        }


        public static T GetCurrency<T>() where T : ICurrency {
            return interactor.GetCurrency<T>();
        }

        public static bool IsEnoughCurrency<T>(T value) where T : ICurrency {
            return interactor.IsEnoughCurrency(value);
        }

        public static void AddCurrency<T>(T value) where T : ICurrency {
            interactor.AddCurrency(value);
        }

        public static void SpendCurrency<T>(T value) where T : ICurrency {
            interactor.SpendCurrency(value);
        }
    }
}