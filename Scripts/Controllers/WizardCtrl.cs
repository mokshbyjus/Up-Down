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
    }

    public interface IWizardCtrl {
        void Init();
    }

    public interface IWizardParent {

    }
}