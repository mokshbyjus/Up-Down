using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private LiftController liftController;
    [SerializeField] private MonsterController monsterController;

    public void OnLiftMovementComplete()
    {   
        //Wizard shoots
    }

    public void OnFloorPassed(int floor)
    {
        //monstereController move monsters
    }

    public void OnMonsterShot()
    {
        
    }

}