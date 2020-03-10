using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Byjus.VisionTest {
    public class StandaloneExternalParent : MonoBehaviour {
        public GameManagerView gameManager;
        public VisionTestInputParser inputParser;

        private void Start() {
            Factory.Init();
            inputParser.Init();
        }
    }
}