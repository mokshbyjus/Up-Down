using System.Collections.Generic;
using Byjus.VisionTest.Verticals;
using Byjus.VisionTest.Views;
using UnityEngine;

namespace Byjus.VisionTest.Ctrls {
    public class GameManagerCtrl : MonoBehaviour, IGameManagerCtrl, IExtInputListener {
        public IGameManagerView view;
        // public IWizardCtrl wizardCtrl;
        [SerializeField] private LiftController liftController = new LiftController();

        public ExWorldInfo worldInfo;
        public int childLiftReqt;
        bool liftInProgress;
        private int prevValue = 0;

        public void Init() {
            // wizardCtrl.Init();

            // wizardCtrl.ToggleInput(true);
            this.worldInfo = new ExWorldInfo();
            // SpawnChild();
        }

        public void ExtInputEnd() {
            view.UpdateRodsAndCubes(worldInfo.NumBlueRods, worldInfo.NumRedCubes);
            view.UpdateInfo(worldInfo.FinalCount);
            if (liftController == null) {
                liftController = FindObjectOfType<LiftController>();
            }
            if (liftController != null) {
                if (worldInfo.dominos.Count > 0) {
                    liftController.AddFloorToQueue(worldInfo.dominos[0].id);
                }
            } else {
                liftController.LiftSmash();
            }
        }

        public void ExtInputStart() {

        }

        public void OnBlueRodAdded() {
            worldInfo.AddBlueRod();
        }

        public void OnRedCubeAdded() {
            worldInfo.AddRedCube();
        }

        public void OnBlueRodRemoved() {
            worldInfo.RemoveBlueRod();
        }

        public void OnRedCubeRemoved() {
            worldInfo.RemoveRedCube();
        }

        public void OnDominosUpdate(List<DominosOutput> dominosOutput) {
            worldInfo.dominos = dominosOutput;
        }

        // public void

        void StartLift() {
            liftInProgress = true;
            // wizardCtrl.ToggleInput(false);
            // view.StartLift(worldInfo.FinalCount);
        }

        public void LiftMoveDone() {
            liftInProgress = false;
            // wizardCtrl.ToggleInput(true);
            // SpawnChild();
        }
    }

    public interface IGameManagerCtrl {
        void Init();
        void LiftMoveDone();
    }

    public class ExWorldInfo {
        public List<DominosOutput> dominos;
        int numRedCubes;
        int numBlueRods;

        public int NumRedCubes { get { return numRedCubes; } }
        public int NumBlueRods { get { return numBlueRods; } }
        public int FinalCount { get { return numBlueRods * 10 - numRedCubes; } }

        public void AddRedCube() {
            numRedCubes++;
        }

        public void RemoveRedCube() {
            numRedCubes--;
        }

        public void AddBlueRod() {
            numBlueRods++;
        }

        public void RemoveBlueRod() {
            numBlueRods--;
        }

        public bool LiftPoweredUp() {
            return numBlueRods > 0;
        }

        public override string ToString() {
            return "Blue: " + NumBlueRods + ", Red: " + NumRedCubes + ", Final: " + FinalCount;
        }
    }
}