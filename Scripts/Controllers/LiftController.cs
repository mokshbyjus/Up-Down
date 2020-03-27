using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LiftController : MonoBehaviour {
    [SerializeField] private WizardController wizardController = null;
    [SerializeField] private MonsterController monsterController = null;
    [SerializeField] private CameraView cameraView = null;
    [SerializeField] private List<Transform> levelsPositionsList;
    [SerializeField] private LiftView liftView;
    [HideInInspector] public LiftModel liftModel = new LiftModel();
    private bool liftIsMoving = false;
    private Coroutine queueChecker = null;
    private Coroutine liftMoveCr = null;
    [HideInInspector] public bool isLiftMoving = false;

    #region --------------------------- Private Methods -------------------------------------

    private void Start() {
        // queueChecker = StartCoroutine(QueueChecker());
        liftModel.currentLevel = 0;
        // liftModel.Enqueue(0);
        // liftModel.Enqueue(4);
        // liftModel.Enqueue(1);
        // liftModel.Enqueue(9);
        // liftModel.Enqueue(2);
        // liftModel.Enqueue(4);
        // OnLiftMoveComplete();
        // liftModel.currentLevel = -1;
        // MoveLift(0);
        // ClearQueue();

    }

    private void ClearQueue() {
        liftModel.floorsQueue.Clear();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            AddFloorToQueue(3);
            AddFloorToQueue(4);
            AddFloorToQueue(3);
            AddFloorToQueue(2);
            AddFloorToQueue(7);
            AddFloorToQueue(0);
        }

        if (!isLiftMoving) {
            if (liftModel.floorsQueue.Count != 0) {
                MoveLift(liftModel.floorsQueue[0]);
            }
        }
    }

    private void Dequeue() {
        if (liftModel.floorsQueue.Count != 0) {
            liftModel.Dequeue();
        }
    }

    private void Enqueue(int floor) {
        liftModel.Enqueue(floor);
    }

    private void MoveLift(int to) {
        Debug.LogError($"Moving lift to:{to} ");

        // liftMoveCr = StartCoroutine(LiftMovementCoroutineBlocky(to));
        liftMoveCr = StartCoroutine(LiftMovementCoroutineSmooth(to));
    }

    private IEnumerator LiftMovementCoroutineBlocky(int to) {
        isLiftMoving = true;
        int liftDir = -1;
        if (to > liftModel.currentLevel) {
            liftDir = 1;
        }
        while (liftModel.currentLevel != to) {
            int nextFloor = liftModel.currentLevel + liftDir;
            while (false) //While wizard is shooting, chill
            {
                yield return new WaitForEndOfFrame();
            }
            // Debug.LogError($"Floor Numner: {nextFloor}");
            liftView.MoveLiftTo(levelsPositionsList[nextFloor]);
            yield return new WaitForSeconds(liftView.timePerFloor);
            UpdateCurrentFloor(nextFloor);
        }

        monsterController.OnLiftMoveComplete();
        yield return new WaitForSeconds(0.5f);
        wizardController.OnLiftMoveComplete(liftModel.currentLevel);
        yield return new WaitForSeconds(0.5f);
        wizardController.IdlePosition();
        OnLiftMoveComplete();
    }

    private IEnumerator LiftMovementCoroutineSmooth(int to) {
        isLiftMoving = true;
        int liftDir = -1;
        if (to > liftModel.currentLevel) {
            liftDir = 1;
        }
        cameraView.StartCamera(levelsPositionsList[to].position);
        float distance = Vector3.Distance(liftView.liftTransform.position, levelsPositionsList[to].position);
        float time = distance / liftView.liftSpeed;
        liftView.MoveLiftToSmooth(levelsPositionsList[to], time);
        yield return new WaitForSeconds(time);
        UpdateCurrentFloor(to);
        monsterController.OnLiftMoveComplete();
        yield return new WaitForSeconds(0.5f); //Monsters Move before wizard shoots.
        wizardController.OnLiftMoveComplete(liftModel.currentLevel);
        yield return new WaitForSeconds(0.5f);
        wizardController.IdlePosition();
        OnLiftMoveComplete();
    }

    private IEnumerator QueueChecker() {
        Debug.LogError("QueueCheke Startedr");
        while (liftMoveCr == null) {
            if (liftModel.floorsQueue.Count != 0) {
                MoveLift(liftModel.floorsQueue[0]);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.LogError("QueueCheke Enderd");
    }

    #endregion ------------------------------------------------------------------------------
    #region  --------------------------- Public Methods -------------------------------------

    public void AddFloorToQueue(int floor) {
        if (floor > -1) {
            Debug.Log($"Adding {floor} to queue");
            Enqueue(floor);
        }
    }

    public void RemoveFloorFromQueue() {
        Dequeue();
    }

    public void UpdateCurrentFloor(int floor) {
        liftModel.currentLevel = floor;
    }

    public void OnLiftMoveComplete() {
        // cameraView.EndCamera();
        isLiftMoving = false;
        Dequeue();
        if (liftModel.floorsQueue.Count != 0) {
            MoveLift(liftModel.floorsQueue[0]);
        }
        // } else {
        //     StartCoroutine(QueueChecker());
        // }
    }

    #endregion ------------------------------------------------------------------------------
}