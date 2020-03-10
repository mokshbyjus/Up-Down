using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !CC_STANDALONE
using Osmo.SDK;
#endif

namespace Byjus.VisionTest {
    public class GameStarter : MonoBehaviour {
        [SerializeField] VisionTestInputParser inputParser;
        [SerializeField] GameManagerView gameManager;
        [SerializeField] WizardView wizard;

        private void Start() {
            SetupHierarchy();
            SetupParent();
        }

        void SetupHierarchy() {
            GameManagerCtrl gameCtrl = new GameManagerCtrl();
            WizardCtrl wizardCtrl = new WizardCtrl();

            inputParser.inputListener = gameCtrl;

            gameManager.ctrl = gameCtrl;
            gameCtrl.view = gameManager;
            gameCtrl.wizardCtrl = wizardCtrl;

            wizard.ctrl = wizardCtrl;
            wizardCtrl.view = wizard;
            wizardCtrl.parent = gameCtrl;

            gameCtrl.Init();
        }

        void SetupParent() {
#if CC_STANDALONE
            var parent = gameObject.AddComponent<StandaloneExternalParent>();
            parent.inputParser = inputParser;
            parent.gameManager = gameManager;
#else
            var parent = gameObject.AddComponent<VisionTestMainParent>();
            parent.mManager = FindObjectOfType<TangibleManager>();
            parent.inputParser = FindObjectOfType<VisionTestInputParser>();
            parent.gameManager = FindObjectOfType<GameManagerView>();
            parent.osmoVisionServiceView = FindObjectOfType<OsmoVisionServiceView>();
#endif
        }
    }
}