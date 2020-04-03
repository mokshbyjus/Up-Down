using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Byjus.VisionTest.Verticals {
    public interface IVisionService {
        void Init();
        List<DominosOutput> GetVisionObjects();
    }

    public class StandaloneVisionService : IVisionService {
        public List<DominosOutput> GetVisionObjects() {
            var numRed = Random.Range(0, 5);
            var numBlue = Random.Range(0, 5);

            var ret = new List<DominosOutput>();
            // for (int i = 0; i < numBlue; i++) { ret.Add(new DominosOutput { type = DominosOutput.TileType.BLUE_ROD, id = i }); }
            // for (int i = 0; i < numRed; i++) { ret.Add(new DominosOutput { type = DominosOutput.TileType.RED_CUBE, id = (numBlue + i) + 1000 }); }

            return ret;
        }

        public void Init() {

        }
    }

    // private class DominosOutput {
    //     public int id;

    //     public override string ToString() {
    //         return id + " ";
    //     }
    // }
}