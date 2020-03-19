﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Byjus.VisionTest.Ctrls;

namespace Byjus.VisionTest.Views {

    public class GameManagerView : MonoBehaviour, IGameManagerView {
        [SerializeField] GameObject blueRodPrefab;
        [SerializeField] GameObject redCubePrefab;
        [SerializeField] GameObject lift;
        [SerializeField] GameObject childPrefab;
        [SerializeField] Text infoText;
        [SerializeField] Text childText;
        [SerializeField] Vector2 blueRodsParentPos;
        [SerializeField] float redCubesParentStartY;

        public IGameManagerCtrl ctrl;

        GameObject blueRodsParent;
        GameObject redCubesParent;
        GameObject child;

        const float SCREEN_TILES_GAP = 0.05f;

        public void SpawnChild(int childLiftReqt) {
            // if (child != null) {
            //     Destroy(child.gameObject);
            // }

            // child = Instantiate(childPrefab, childPrefab.transform.position, Quaternion.identity, transform);
            // childText.text = childLiftReqt + "";
        }

        public void UpdateRodsAndCubes(int numBlueRods, int numRedCubes) {
            // if (blueRodsParent != null) {
            //     Destroy(blueRodsParent);
            // }
            // blueRodsParent = new GameObject("BlueRodsParent");
            // blueRodsParent.transform.position = blueRodsParentPos;
            // for (int i = 0; i < numBlueRods; i++) {
            //     var pos = blueRodsParent.transform.position + new Vector3(i * (1 + SCREEN_TILES_GAP), 0);
            //     Instantiate(blueRodPrefab, pos, Quaternion.identity, blueRodsParent.transform);
            // }

            // if (redCubesParent != null) {
            //     Destroy(redCubesParent);
            // }
            // redCubesParent = new GameObject("RedCubesParent");
            // redCubesParent.transform.position = blueRodsParent.transform.position + new Vector3(numBlueRods * (1 + SCREEN_TILES_GAP), redCubesParentStartY);
            // for (int i = 0; i < numRedCubes; i++) {
            //     var pos = redCubesParent.transform.position + new Vector3(0, -i * (1 + SCREEN_TILES_GAP));
            //     Instantiate(redCubePrefab, pos, Quaternion.identity, redCubesParent.transform);
            // }
        }

        public void UpdateInfo(int finalCount) {
            // infoText.text = "Floor Num: " + finalCount;
        }

        public void StartLift(int floors) {
            // StartCoroutine(MoveLift(floors));
        }

        IEnumerator MoveLift(int floors) {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < floors; i++) {
                lift.transform.position += new Vector3(0, 0.50f);
                yield return new WaitForSeconds(0.2f);
            }
            ctrl.LiftMoveDone();
        }
    }

    public interface IGameManagerView {
        void SpawnChild(int childLiftReqt);
        void UpdateRodsAndCubes(int numBlueRods, int numRedCubes);
        void UpdateInfo(int finalCount);
        void StartLift(int floors);
    }
}