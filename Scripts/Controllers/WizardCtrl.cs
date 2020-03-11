using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Byjus.VisionTest.Views;

namespace Byjus.VisionTest.Ctrls {
    public class WizardCtrl : IWizardCtrl {
        public IWizardParent parent;
        public IWizardView view;

        public void Init() {
            
        }

        public void ToggleInput(bool enable) {
            view.ShowText(enable ? "READY" : "STOP");
        }
    }

    public interface IWizardCtrl {
        void Init();
        void ToggleInput(bool enable);
    }

    public interface IWizardParent {

    }
}