using UnityEditor;
using Byjus.VisionTest.Externals;

#if !CC_STANDALONE
using Osmo.SDK;
#endif

namespace Byjus.VisionTest.EditorScripts {
#if CC_STANDALONE

    [CustomEditor(typeof(StandaloneExternalParent))]
    public class AssignRefsEditor : Editor {
        
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var tg = (StandaloneExternalParent) target;
            tg.hierarchyManager = FindObjectOfType<HierarchyManager>();
        }
    }

#else

    [CustomEditor(typeof(VisionTestMainParent))]
    public class AssignParentEditor : Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            var tg = (VisionTestMainParent) target;
            tg.mManager = FindObjectOfType<TangibleManager>();
            tg.osmoVisionServiceView = FindObjectOfType<OsmoVisionServiceView>();
            tg.hierarchyManager = FindObjectOfType<HierarchyManager>();
        }
    }

#endif
}