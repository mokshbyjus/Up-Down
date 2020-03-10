using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !CC_STANDALONE
using Osmo.SDK;
#endif

namespace Byjus.VisionTest {
    public class HierarchyManager : MonoBehaviour {
        [SerializeField] VisionTestInputParser inputParser;
        [SerializeField] GameManagerView gameManager;
        [SerializeField] WizardView wizard;

        public void Setup() {
            GameManagerCtrl gameCtrl = new GameManagerCtrl();
            WizardCtrl wizardCtrl = new WizardCtrl();

            inputParser.inputListener = gameCtrl;

            gameManager.ctrl = gameCtrl;
            gameCtrl.view = gameManager;
            gameCtrl.wizardCtrl = wizardCtrl;

            wizard.ctrl = wizardCtrl;
            wizardCtrl.view = wizard;
            wizardCtrl.parent = gameCtrl;

            ((IGameManagerCtrl) gameCtrl).Init();
            inputParser.Init();
        }
    }
}