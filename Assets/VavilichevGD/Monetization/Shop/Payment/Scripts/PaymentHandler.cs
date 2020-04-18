using System.Collections;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {

    public delegate void PaymentResultHandler(Product product, bool success);
    
    public abstract class PaymentHandler {

        protected const bool FAIL = false;
        protected const bool SUCCESS = true;

        public Coroutine StartPayment(Product product, PaymentResultHandler callback) {
            return Coroutines.StartRoutine(PaymentRoutine(product, callback));
        }

        protected abstract IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback);
    }
}