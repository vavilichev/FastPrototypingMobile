using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Core.Loadging;

namespace FastPrototype.Architecture {
    public class BootSceneManager : MonoBehaviour {
        private void Start() {
//            Loading.LoadScene(1);
            GameFastPrototype.Run();
        }
    }
}