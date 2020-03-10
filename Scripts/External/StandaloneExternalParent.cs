using Byjus.VisionTest.Verticals;
using UnityEngine;

#if CC_STANDALONE
namespace Byjus.VisionTest.Externals {
    public class StandaloneExternalParent : MonoBehaviour {
        public HierarchyManager hierarchyManager;

        private void Start() {
            Factory.Init();
            hierarchyManager.Setup();
        }
    }
}
#endif