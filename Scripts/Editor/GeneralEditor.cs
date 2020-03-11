using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Byjus.VisionTest.Util;
using System.IO;

namespace Byjus.VisionTest.EditorScripts {
    public class GeneralEditor : Editor {

        [MenuItem("RnC/Assign References")]
        public static void AssignRefs() {
            var fileName = Constants.SCRIPTS_REF_FILE_NAME;
            var fileContents = File.ReadAllText(fileName);
            Debug.LogError("Writing it to scripts location");
            File.WriteAllText(Constants.SCRIPTS_ASSEMBLY_DEF_OUTPUT_PATH, fileContents);
            Debug.LogError("Writing to scripts done");
        }
    }
}