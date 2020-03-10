using UnityEngine;

namespace Byjus.VisionTest {

    public interface IGameManagerCtrl {
        void Init();
        void LiftMoveDone();
    }

    public class GameManagerCtrl : IGameManagerCtrl, IExtInputListener, IWizardParent {
        public IGameManagerView view;
        public IWizardCtrl wizardCtrl;

        ExWorldInfo worldInfo;
        int childLiftReqt;
        bool liftInProgress;

        public void Init() {
            wizardCtrl.Init();

            this.worldInfo = new ExWorldInfo();

            SpawnChild();
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
            if (worldInfo.FinalCount == childLiftReqt && !liftInProgress) {
                StartLift();
            }
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
            if (worldInfo.FinalCount == childLiftReqt) {
                view.StartLift(worldInfo.FinalCount);
            }
        }

        public void LiftMoveDone() {
            liftInProgress = false;
            SpawnChild();
        }
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