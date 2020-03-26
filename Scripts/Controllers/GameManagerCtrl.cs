using Byjus.VisionTest.Verticals;
using Byjus.VisionTest.Views;
using UnityEngine;

namespace Byjus.VisionTest.Ctrls {
    public class GameManagerCtrl : MonoBehaviour, IGameManagerCtrl, IExtInputListener {
        public IGameManagerView view;
        // public IWizardCtrl wizardCtrl;
        [SerializeField] private LiftController liftController = new LiftController();

        ExWorldInfo worldInfo;
        public int childLiftReqt;
        bool liftInProgress;
        private int prevValue = 0;

        public void Init() {
            // wizardCtrl.Init();

            // wizardCtrl.ToggleInput(true);
            this.worldInfo = new ExWorldInfo();
            // SpawnChild();
        }

        void SpawnChild() {
            int tensRange = Random.Range(0, 3);
            int onesRange = Random.Range(6, 9);
            childLiftReqt = tensRange * 10 + onesRange;
            view.SpawnChild(childLiftReqt);
        }

        public void ExtInputStart() {

        }

        public void ExtInputEnd() {
            view.UpdateRodsAndCubes(worldInfo.NumBlueRods, worldInfo.NumRedCubes);
            view.UpdateInfo(worldInfo.FinalCount);
            if (liftController == null) {
                Debug.Log("null");
                liftController = FindObjectOfType<LiftController>();
            }
            if (liftController != null) {
                Debug.Log("Not null");
                if (prevValue != worldInfo.FinalCount && worldInfo.FinalCount <= 10 && worldInfo.FinalCount > -1) {
                    prevValue = worldInfo.FinalCount;
                    if (worldInfo.FinalCount > 0) {
                        liftController.AddFloorToQueue(worldInfo.FinalCount - 1); 
                    }
                }
            }

            //Update Queue
            // StartLift();
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

        void StartLift() {
            liftInProgress = true;
            // wizardCtrl.ToggleInput(false);
            // view.StartLift(worldInfo.FinalCount);
        }

        public void LiftMoveDone() {
            liftInProgress = false;
            // wizardCtrl.ToggleInput(true);
            SpawnChild();
        }
    }

    public interface IGameManagerCtrl {
        void Init();
        void LiftMoveDone();
    }

    public class ExWorldInfo {
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