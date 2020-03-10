using UnityEngine;
using System.Collections;
using Byjus.VisionTest.Ctrls;

namespace Byjus.VisionTest.Views {
    public class WizardView : MonoBehaviour, IWizardView {
        public IWizardCtrl ctrl;
    }

    public interface IWizardView {

    }
}