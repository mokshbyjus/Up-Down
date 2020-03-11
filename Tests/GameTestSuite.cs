using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Byjus.VisionTest.Verticals;
using Byjus.VisionTest.Ctrls;
using Byjus.VisionTest.Views;

namespace Byjus.VisionTest.Tests {
    public class GameTestSuite {
        GameManagerCtrl gameCtrl;
        IExtInputListener inputListener;
        TestWizard testWizard;
        TestGameView testView;

        [SetUp]
        public void Setup() {
            gameCtrl = new GameManagerCtrl();
            testWizard = new TestWizard();
            testView = new TestGameView();

            inputListener = gameCtrl;
            gameCtrl.view = testView;
            gameCtrl.wizardCtrl = testWizard;

            gameCtrl.Init();
        }

        [TearDown]
        public void Cleanup() {
            gameCtrl = null;
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestGameWithBasicInput() {
            inputListener.ExtInputStart();
            inputListener.OnRedCubeAdded();
            inputListener.OnBlueRodAdded();
            inputListener.OnBlueRodAdded();
            inputListener.ExtInputEnd();

            yield return null;

            Assert.AreEqual(2, testView.numBlue);
            Assert.AreEqual(1, testView.numRed);
        }
    }

    class TestGameView : IGameManagerView {
        public int numRed;
        public int numBlue;

        public void SpawnChild(int childLiftReqt) {
            Debug.Log("View: Child Spawned for reqt: " + childLiftReqt);
        }

        public void StartLift(int floors) {
            Debug.Log("View: Lift started for floors: " + floors);
        }

        public void UpdateInfo(int finalCount) {
            Debug.Log("View: Updating info: " + finalCount);
        }

        public void UpdateRodsAndCubes(int numBlueRods, int numRedCubes) {
            this.numBlue = numBlueRods;
            this.numRed = numRedCubes;
            Debug.Log("View: Updating rods and cubes: " + numBlueRods + ", " + numRedCubes);
        }
    }

    class TestWizard : IWizardCtrl {
        public void Init() {
            Debug.Log("Wizard: Init");
        }

        public void ToggleInput(bool enable) {
            Debug.Log("Wizard: Toggle Input: " + enable);
        }
    }
}
