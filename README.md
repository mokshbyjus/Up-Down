Use rods and cubes to power up the elevator and tell it which floor to go to.
Blue rod (10 blue cubes) are positive floors
Red cube are negative floors

*********

Switching between Standalone and Osmo

Use the player settings flag CC_STANDALONE to switch to Standalone. In standalone, the project can be run separately i.e. outside Osmo.

The main thing which changes is the assembly definition (Scripts.asmdef) file between the two modes. I'm not able to find a way to do it automatically on switch of build flag.

There will be an error in standalone mode saying reference missing in assembly definition. In that case, just remove the references (only in standalone mode). In Osmo mode, the references will be present.

In case, still there is an issue, there are files in Files/ folder which correspond to the correct json for the two modes. You can copy paste the corresponding file contents in Scripts.asmdef, and it should work after that.

*********

Architecture Info:

- Osmo services have two variants Osmo*** and Standalone***. Example: OsmoExternalParent and StandaloneExternalParent, OsmoVisionService and StandaloneVisionService
  Depending on the flag CC_STANDALONE, it switches between services.
  
- All services are interfaced (also, everything else in the game for testability). This is managed by Factory. 
  So, game has access to IVisionService which is mapped to either OsmoVisionService or OsmoEditorVisionService or StandaloneVisionService.

- Reading input from Vision is two step. 
  First, the OsmoVisionService registers to VisionConnector, and it just converts from Osmo models to Game models. Basic dump.
  Second, there is an InputParser which reads from IVisionService. This is the one who determines what to do with the vision data. So, pure game logic. 
  For example, in this game, it maintaines current list of rods and cubes, and on receiving new objects determines which objects were removed, and which were added newly.
  
- The interface between VisionParser and Game is defined in IExtInputListener.
  
- HierarchyManager has responsibility of creating the hierarchy. Since, there are controllers (non mono-behaviour objects).
  This class takes all view refs, creates reqd controllers, and assigns all child/parent references.

- MVC
  Views only have info about themselves, and have a ref to their controller. Absolutely no child related information.
  Controllers have child/parent ctrl refs which they can communicate with (interfaces), and those controllers in turn communicate with their views.
  
- Testability
  Check initial game tests in GameTestSuite.
  
  Great starting resources for writing tests.
  - https://www.raywenderlich.com/9454-introduction-to-unity-unit-testing
  - https://stackoverflow.com/questions/1093020/unit-testing-and-checking-private-variable-value
