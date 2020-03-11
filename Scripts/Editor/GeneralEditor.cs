using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Byjus.VisionTest.Util;
using System.IO;

namespace Byjus.VisionTest.EditorScripts {
    [CustomEditor(typeof(GeneralHelper))]
    public class GeneralEditor : Editor {

        [MenuItem("RnC/Assign References")]
        public static void AssignRefs() {
#if CC_STANDALONE
            var fileName = Constants.STANDALONE_SCRIPTS_REF_FILE_NAME;
#else
            var fileName = Constants.OSMO_SCRIPTS_REF_FILE_NAME;
#endif

            var fileContents = File.ReadAllText(fileName);
            Debug.LogError("Writing it to scripts location");
            File.WriteAllText(Constants.SCRIPTS_ASSEMBLY_DEFINITION_PATH, fileContents);
            Debug.LogError("Writing to scripts done");
        }
    }
}