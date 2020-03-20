using UnityEngine;
using Byjus.VisionTest.Ctrls;
using Byjus.VisionTest.Views;
using Byjus.VisionTest.Verticals;

namespace Byjus.VisionTest.Externals {
    public class HierarchyManager : MonoBehaviour {
        [SerializeField] InputParser inputParser;
        [SerializeField] GameManagerView gameManager;
        // [SerializeField] WizardView wizard;

        public void Setup() {
            GameManagerCtrl gameCtrl = new GameManagerCtrl();
            // WizardCtrl wizardCtrl = new WizardCtrl();

            inputParser.inputListener = gameCtrl;

            gameManager.ctrl = gameCtrl;
            gameCtrl.view = gameManager;
            // gameCtrl.wizardCtrl = wizardCtrl;

            // wizard.ctrl = wizardCtrl;
            // wizardCtrl.view = wizard;
            // wizardCtrl.parent = gameCtrl;

            ((IGameManagerCtrl) gameCtrl).Init();
            inputParser.Init();
        }
    }
}