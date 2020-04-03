using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #if !CC_STANDALONE
using Osmo.SDK;
using Osmo.SDK.Vision;
using Osmo.SDK.VisionPlatformModule;

namespace Byjus.VisionTest.Verticals {
    public class OsmoVisionService : MonoBehaviour, IVisionService {
        string lastJson;

        public void Init() {
            lastJson = "{}";
            VisionConnector.Register(
                apiKey: API.Key,
                objectName: "OsmoVisionServiceView",
                functionName: "DispatchEvent",
                mode : 46,
                async : false,
                hires : false
            );
        }

        public void DispatchEvent(string json) {
            if (json == null) { return; }
            lastJson = json;
        }

        public List<DominosOutput> GetVisionObjects() {
            try {
                var output = JsonUtility.FromJson<JOutput>(lastJson);
                if (output == null || output.items == null) {
                    Debug.LogError("Returning empty for json " + lastJson);
                }

                var ret = new List<DominosOutput>();
                ret = output.items;
                // int numBlues = 0, numReds = 0;
                // foreach (var item in output.items) {
                //     if (string.Equals(item.color, "blue")) {
                //         numBlues++;
                //     } else if (string.Equals(item.color, "red")) {
                //         numReds++;
                //     }
                // }

                // numBlues = Mathf.CeilToInt(numBlues / 10);
                // //Debug.LogError("Returning vision objects: Number of Blues " + numBlues + ", Number of reds: " + numReds);

                // for (int i = 0; i < numBlues; i++) { ret.Add(new DominosOutput { id = i, type = DominosOutput.TileType.BLUE_ROD }); }
                // for (int i = 0; i < numReds; i++) { ret.Add(new DominosOutput { id = (i + numBlues) + 1000, type = DominosOutput.TileType.RED_CUBE }); }

                Debug.Log("Output: " + lastJson);

                return ret;

            } catch (System.Exception e) {
                Debug.LogError("Error while parsing lastJson: " + lastJson + "\nException: " + e.Message);
                return new List<DominosOutput>();
            }
        }
    }

    [System.Serializable]
    public class JOutput {
        public List<float> detectionArea;
        public int frameCounter;
        public float handProbability;
        public List<DominosOutput> items;
    }

     [System.Serializable]
    public class DominosOutput {
        // public string color;
        public int id;
    }

}
// #endif