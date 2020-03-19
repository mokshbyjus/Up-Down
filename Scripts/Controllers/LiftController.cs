using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LiftController : MonoBehaviour {
    [SerializeField] private List<Transform> levelsPositionsList;
    [SerializeField] private LiftView liftView;
    LiftModel liftModel = new LiftModel();
    private bool liftIsMoving = false;
    private Coroutine queueChecker = null;

    #region --------------------------- Private Methods -------------------------------------

    private void Start() {
        // liftModel.currentLevel = 0;
        // liftModel.Enqueue(0);
        // liftModel.Enqueue(4);
        // liftModel.Enqueue(1);
        // liftModel.Enqueue(9);
        // liftModel.Enqueue(2);
        // liftModel.Enqueue(4);
        OnLiftMoveComplete();
        queueChecker = StartCoroutine(QueueChecker());
        liftModel.currentLevel = -1;
        MoveLift(0);
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
        StartCoroutine(LiftMovementCoroutine(to));
    }

    private IEnumerator LiftMovementCoroutine(int to) {
        int liftDir = -1;
        if (to > liftModel.currentLevel) {
            liftDir = 1;
        }
        while (liftModel.currentLevel != to) {
            int nextFloor = liftModel.currentLevel + liftDir;
            liftView.MoveLiftTo(levelsPositionsList[nextFloor]);
            UpdateCurrentFloor(nextFloor);
            yield return new WaitForSeconds(liftView.timePerFloor);
        }
        OnLiftMoveComplete();
    }

    private IEnumerator QueueChecker() {
        while (true) {
            if (liftModel.floorsQueue.Count != 0) {
                MoveLift(liftModel.floorsQueue[0]);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion ------------------------------------------------------------------------------
    #region  --------------------------- Public Methods -------------------------------------

    public void AddFloorToQueue(int floor) {
        Debug.Log($"Adding {floor} to queue");
        Enqueue(floor);
    }

    public void RemoveFloorFromQueue() {
        Dequeue();
    }

    public void UpdateCurrentFloor(int floor) {
        liftModel.currentLevel = floor;
    }

    public void OnLiftMoveComplete() {
        Dequeue();
        if (liftModel.floorsQueue.Count != 0) {
            MoveLift(liftModel.floorsQueue[0]);
        } else {
            StartCoroutine(QueueChecker());
        }
    }

    #endregion ------------------------------------------------------------------------------
}