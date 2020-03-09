using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !CC_STANDALONE
using Osmo.SDK;
using Osmo.Container.Common;
using Osmo.Dominocode;
using Osmo.SDK.VisionPlatformModule;

namespace Byjus.VisionTest {
    public class VisionTestMainParent : OsmoGameBase, IOsmoVisionHelper, IOsmoEditorVisionHelper {
        [SerializeField] TangibleManager mManager;
        [SerializeField] GameManagerView gameManager;
        [SerializeField] VisionTestInputParser inputParser;
        [SerializeField] OsmoVisionServiceView osmoVisionServiceView;

        public TangibleManager tangibleManager { get { return mManager; } }

        protected override void GameStart() {
            if (Bridge != null) {
                Bridge.Helper.SetOnMainMenuScreen(false);
                Bridge.Helper.OnSettingsButtonClick += OnSettingsButtonClicked;
                Bridge.Helper.SetSettingsButtonVisibility(true);
                Bridge.Helper.SetVisionActive(true);
                Bridge.Helper.SetOsmoWorldStickersAllowed(true);

                Factory.Init(osmoVisionServiceView, this);
                gameManager.Init();
                inputParser.Init();

            } else {
                Debug.LogWarning("[VisionTest] You are running without the Osmo bridge. No Osmo services will be loaded. Bridge.Helper will be null");
            }
        }

        private void OnEnable() {
            mManager.OnObjectEnter += OnVisionObjectEnter;
            mManager.OnObjectExit += OnVisionObjectExit;
        }

        void OnSettingsButtonClicked() {
            Debug.LogWarning("Settings Clicked");
        }

        void OnVisionObjectEnter(TangibleObject obj) {
            var config = (DominocodeIdConfig) mManager.deck_.IdConfig;

        }

        void OnVisionObjectExit(TangibleObject obj) {
        }

        private void Update() {
            if (!gameReady_) {
                return;
            }

        }
    }



    public interface IOsmoEditorVisionHelper {
        TangibleManager tangibleManager { get; }
    }


    public class OsmoEditorVisionService : IVisionService {
        IOsmoEditorVisionHelper visionHelper;

        public OsmoEditorVisionService(IOsmoEditorVisionHelper visionHelper) {
            this.visionHelper = visionHelper;
        }

        public List<VisionTestExtInput> GetVisionObjects() {
            var aliveObjs = visionHelper.tangibleManager.AliveObjects;
            var ret = new List<VisionTestExtInput>();
            foreach (var obj in aliveObjs) {
                if (obj.Id < 10) {
                    ret.Add(new VisionTestExtInput { id = obj.Id, type = VisionTestExtInput.TileType.BLUE_ROD });
                } else {
                    ret.Add(new VisionTestExtInput { id = obj.Id, type = VisionTestExtInput.TileType.RED_CUBE });
                }
            }
            return ret;
        }

        public void Init() {
            
        }
    }
}
#endif