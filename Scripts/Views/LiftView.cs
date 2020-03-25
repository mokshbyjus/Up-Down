using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LiftView : MonoBehaviour {
    public float timePerFloor = 2;
    [SerializeField] private Transform liftTransform = null;
    [SerializeField] private LiftController liftController = null;
    [SerializeField] private Transform testFloor = null;

    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     MoveLiftTo(testFloor);
        // }
    }
    public void MoveLiftTo(Transform toPos) {
        liftTransform.DOMove(toPos.position, timePerFloor)
            .SetEase(Ease.OutQuint);
    }


}