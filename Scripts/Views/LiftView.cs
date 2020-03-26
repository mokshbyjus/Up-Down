using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LiftView : MonoBehaviour {
    [Header("Block lift movement")]
    public float timePerFloor = 2;
    [Header("Smooth lift movement")]
    public float liftSpeed = 10f;
    [Space(4)]
    public Transform liftTransform = null;
    [SerializeField] private LiftController liftController = null;
    [SerializeField] private Transform testFloor = null;
    public Ease liftEase;

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     MoveLiftTo(testFloor);
        // }
    }
    public void MoveLiftTo(Transform toPos, float timePerFloor = -1) {
        if (timePerFloor == -1) {
            timePerFloor = this.timePerFloor;
        }
        liftTransform.DOMove(toPos.position, timePerFloor)
        .SetEase(liftEase);
    }

    public void MoveLiftToSmooth(Transform toPos,float time) {
        MoveLiftTo(toPos, time);
    }

}