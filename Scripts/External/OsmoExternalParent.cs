using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Byjus.VisionTest.Verticals;

#if !CC_STANDALONE
using Osmo.SDK;
using Osmo.Container.Common;

namespace Byjus.VisionTest.Externals {
    public class VisionTestMainParent : OsmoGameBase, IOsmoEditorVisionHelper {
        public TangibleManager mManager;
        public OsmoVisionServiceView osmoVisionServiceView;
        public HierarchyManager hierarchyManager;

        public TangibleManager tangibleManager { get { return mManager; } }

        protected override void GameStart() {
            if (Bridge != null) {
                Bridge.Helper.SetOnMainMenuScreen(false);
                Bridge.Helper.OnSettingsButtonClick += OnSettingsButtonClicked;
                Bridge.Helper.SetSettingsButtonVisibility(true);
                Bridge.Helper.SetVisionActive(true);
                Bridge.Helper.SetOsmoWorldStickersAllowed(true);

#if UNITY_EDITOR
                Factory.Init(this);
#else
                Factory.Init(osmoVisionServiceView);
#endif
                hierarchyManager.Setup();

            } else {
                Debug.LogWarning("[VisionTest] You are running without the Osmo bridge. No Osmo services will be loaded. Bridge.Helper will be null");
            }
        }

        void OnSettingsButtonClicked() {
            Debug.LogWarning("Settings Clicked");
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

        public List<ExtInput> GetVisionObjects() {
            var aliveObjs = visionHelper.tangibleManager.AliveObjects;
            var ret = new List<ExtInput>();
            foreach (var obj in aliveObjs) {
                if (obj.Id < 10) {
                    ret.Add(new ExtInput { id = obj.Id, type = ExtInput.TileType.BLUE_ROD });
                } else {
                    ret.Add(new ExtInput { id = obj.Id, type = ExtInput.TileType.RED_CUBE });
                }
            }
            return ret;
        }

        public void Init() {
            
        }
    }
}
#endif