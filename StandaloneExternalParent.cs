using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if CC_STANDALONE
namespace Byjus.VisionTest {
    public class StandaloneExternalParent : MonoBehaviour {
        public HierarchyManager hierarchyManager;

        private void Start() {
            Factory.Init();
            hierarchyManager.Setup();
        }
    }
}
#endif