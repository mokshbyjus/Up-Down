using System;
namespace Byjus.VisionTest.Util {
    public class Constants {
#if OSMO
        public static string SCRIPTS_REF_FILE_NAME = "Assets/Games/container-tester-games/RncElevator/Files/OsmoScriptRefs.json";
        public static string SCRIPTS_ASSEMBLY_DEF_OUTPUT_PATH = "Assets/Games/container-tester-games/RncElevator/Scripts/Scripts.asmdef";
#else
        public static string SCRIPTS_REF_FILE_NAME = "Assets/Files/StandaloneScriptRefs.json";
        public static string SCRIPTS_ASSEMBLY_DEF_OUTPUT_PATH = "Assets/Scripts/Scripts.asmdef";
#endif
    }
}