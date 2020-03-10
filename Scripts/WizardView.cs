using UnityEngine;
using System.Collections;

namespace Byjus.VisionTest {
    public class WizardView : MonoBehaviour, IWizardView {
        public WizardCtrl ctrl;
    }

    public interface IWizardView {

    }

    public class WizardCtrl : IWizardCtrl {
        public IWizardParent parent;
        public WizardView view;

        public void Init() {

        }
    }

    public interface IWizardCtrl {
        void Init();
    }

    public interface IWizardParent {

    }
}