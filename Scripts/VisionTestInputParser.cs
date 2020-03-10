using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Byjus.VisionTest {
    public class VisionTestInputParser : MonoBehaviour {
        public IExtInputListener inputListener;

        IVisionService visionService;

        List<ExtInput> currentObjects;

        int inputCount;

        public void Init() {
            visionService = Factory.GetVisionService();
            currentObjects = new List<ExtInput>();
            inputCount = 0;

            StartCoroutine(ListenForInput());
        }

        IEnumerator ListenForInput() {
            yield return new WaitForSeconds(3f);

            inputListener.ExtInputStart();

            var str = "Input count: " + inputCount + "\n";

            var visionObjects = visionService.GetVisionObjects();
            visionObjects.Sort((x, y) => x.id - y.id);
            // compare current ojbects with new Objects
            // notify GameManager of the input.
            
            Segregate(visionObjects, out List<ExtInput> oldObjects, out List<ExtInput> commonObjects, out List<ExtInput> newObjects);

            foreach (var old in oldObjects) {
                str += "Old: " + old + "\n";
                if (old.type == ExtInput.TileType.BLUE_ROD) {
                    str += "Removing blue\n";
                    inputListener.OnBlueRodRemoved();
                } else {
                    str += "Removing red\n";
                    inputListener.OnRedCubeRemoved();
                }
            }

            foreach (var newO in newObjects) {
                str += "New: " + newO + "\n";
                if (newO.type == ExtInput.TileType.BLUE_ROD) {
                    str += "Adding blue\n";
                    inputListener.OnBlueRodAdded();
                } else {
                    str += "Adding red\n";
                    inputListener.OnRedCubeAdded();
                }
            }

            Debug.LogError(str);
            inputCount++;

            currentObjects.Clear();
            currentObjects.AddRange(visionObjects);

            inputListener.ExtInputEnd();

            StartCoroutine(ListenForInput());
        }

        void Segregate(List<ExtInput> visionObjects, out List<ExtInput> oldObjects, out List<ExtInput> commonObjects, out List<ExtInput> newObjects) {
            oldObjects = new List<ExtInput>();
            commonObjects = new List<ExtInput>();
            newObjects = new List<ExtInput>();

            int i = 0, j = 0;
            while (i < currentObjects.Count && j < visionObjects.Count) {
                var curr = currentObjects[i];
                var newO = visionObjects[j];
                if (curr.id == newO.id) {
                    commonObjects.Add(curr);
                    i++;
                    j++;
                } else if (curr.id > newO.id) {
                    newObjects.Add(newO);
                    j++;
                } else {
                    oldObjects.Add(curr);
                    i++;
                }
            }
            while (i < currentObjects.Count) {
                oldObjects.Add(currentObjects[i]);
                i++;
            }
            while (j < visionObjects.Count) {
                newObjects.Add(visionObjects[j]);
                j++;
            }
        }
    }

    public interface IExtInputListener {
        void ExtInputStart();
        void ExtInputEnd();
        void OnBlueRodAdded();
        void OnBlueRodRemoved();
        void OnRedCubeAdded();
        void OnRedCubeRemoved();
    }

}