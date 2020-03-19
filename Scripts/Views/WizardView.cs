using UnityEngine;
using UnityEngine.UI;
using Byjus.VisionTest.Ctrls;

namespace Byjus.VisionTest.Views {
    public class WizardView : MonoBehaviour, IWizardView {
        [SerializeField] Text wizardText;
        public IWizardCtrl ctrl;

        public void ShowText(string text) {
            // wizardText.text = text;
        }
    }

    public interface IWizardView {
        void ShowText(string text);
    }
}