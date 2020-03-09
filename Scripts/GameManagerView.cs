using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Byjus.VisionTest {
    public class GameManagerView : MonoBehaviour {
        [SerializeField] GameObject blueRodPrefab;
        [SerializeField] GameObject redCubePrefab;
        [SerializeField] GameObject lift;
        [SerializeField] GameObject childPrefab;
        [SerializeField] GameObject wizard;
        [SerializeField] Text infoText;
        [SerializeField] Text childText;

        ExWorldInfo worldInfo;

        GameObject blueRodsParent;
        GameObject redCubesParent;
        GameObject child;
        int childLiftReqt;

        public void Init() {
            worldInfo = new ExWorldInfo();

            SpawnChild();
        }

        void SpawnChild() {
            if (child != null) {
                Destroy(child.gameObject);
            }

            child = Instantiate(childPrefab, childPrefab.transform.position, Quaternion.identity, transform);
            int tensRange = Random.Range(0, 3);
            int onesRange = Random.Range(6, 9);
            childLiftReqt = tensRange * 10 + onesRange;
            childText.text = childLiftReqt + "";
        }

        public void ExtInputStart() {

        }

        public void ExtInputEnd() {
            UpdateState();
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

        bool liftInProgress;

        public void OnLiftStarted() {
            Debug.LogError("Started Lift");
            liftInProgress = true;
            if (worldInfo.FinalCount == childLiftReqt) {
                StartCoroutine(MoveLift(worldInfo.FinalCount));
            } else {
                infoText.text = "Need more energy !!!";
            }
        }

        public void OnLiftStopped() {

        }

        void UpdateState() {
            Debug.LogError("Updating state. world info: " + worldInfo);
            if (blueRodsParent != null) {
                Destroy(blueRodsParent);
            }
            blueRodsParent = new GameObject("BlueRodsParent");
            for (int i = 0; i < worldInfo.NumBlueRods; i++) {
                var pos = blueRodsParent.transform.position + new Vector3(i * 1, 0);
                Instantiate(blueRodPrefab, pos, Quaternion.identity, blueRodsParent.transform);
            }

            if (redCubesParent != null) {
                Destroy(redCubesParent);
            }
            redCubesParent = new GameObject("RedCubesParent");
            redCubesParent.transform.position = blueRodsParent.transform.position + new Vector3(worldInfo.NumBlueRods * 1, 0);
            for (int i = 0; i < worldInfo.NumRedCubes; i++) {
                var pos = redCubesParent.transform.position + new Vector3(i * 1, 0);
                Instantiate(redCubePrefab, pos, Quaternion.identity, redCubesParent.transform);
            }

            infoText.text = "Floor Num: " + worldInfo.FinalCount;

            if (worldInfo.FinalCount == childLiftReqt && !liftInProgress) {
                OnLiftStarted();
            }
        }

        IEnumerator MoveLift(int floors) {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < floors; i++) {
                lift.transform.position += new Vector3(0, 0.50f);
                yield return new WaitForSeconds(0.2f);
            }

            for (int i = 0; i < floors; i++) {
                lift.transform.position += new Vector3(0, -0.50f);
                yield return new WaitForSeconds(0.1f);
            }

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