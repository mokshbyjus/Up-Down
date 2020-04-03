using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Byjus.VisionTest.Verticals {
    public class InputParser : MonoBehaviour {
        public IExtInputListener inputListener;

        IVisionService visionService;

        List<DominosOutput> currentObjects;

        int inputCount;

        public void Init() {
            visionService = Factory.GetVisionService();
            currentObjects = new List<DominosOutput>();
            inputCount = 0;

            StartCoroutine(ListenForInput());
        }

        IEnumerator ListenForInput() {
            yield return new WaitForSeconds(3f);

            inputListener.ExtInputStart();

            var str = "Input count: " + inputCount + "\n";

            var visionObjects = visionService.GetVisionObjects();
            visionObjects.Sort((x, y) => x.id - y.id);
            // // compare current ojbects with new Objects
            // // notify GameManager of the input.
            
            // Segregate(visionObjects, out List<DominosOutput> oldObjects, out List<DominosOutput> commonObjects, out List<DominosOutput> newObjects);

            // foreach (var old in oldObjects) {
            //     str += "Old: " + old + "\n";
            //     if (old.type == DominosOutput.TileType.BLUE_ROD) {
            //         str += "Removing blue\n";
            //         inputListener.OnBlueRodRemoved();
            //     } else {
            //         str += "Removing red\n";
            //         inputListener.OnRedCubeRemoved();
            //     }
            // }

            // foreach (var newO in newObjects) {
            //     str += "New: " + newO + "\n";
            //     if (newO.type == DominosOutput.TileType.BLUE_ROD) {
            //         str += "Adding blue\n";
            //         inputListener.OnBlueRodAdded();
            //     } else {
            //         str += "Adding red\n";
            //         inputListener.OnRedCubeAdded();
            //     }
            // }

            Debug.LogError(str);
            inputCount++;

            currentObjects.Clear();
            currentObjects.AddRange(visionObjects);

            inputListener.OnDominosUpdate(currentObjects);

            inputListener.ExtInputEnd();

            StartCoroutine(ListenForInput());
        }

        void Segregate(List<DominosOutput> visionObjects, out List<DominosOutput> oldObjects, out List<DominosOutput> commonObjects, out List<DominosOutput> newObjects) {
            oldObjects = new List<DominosOutput>();
            commonObjects = new List<DominosOutput>();
            newObjects = new List<DominosOutput>();

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
        void OnDominosUpdate(List<DominosOutput> dominosOutput);
    }

}