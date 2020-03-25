using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {
    [SerializeField] private List<Transform> shootPoints = new List<Transform>();
    [SerializeField] private GameObject fireBallPrefab = null;
    [SerializeField] private MonsterController monsterController = null;
    public void ShootFireball(Side side) {

        var fireball = Instantiate(fireBallPrefab, shootPoints[(int) side].position, Quaternion.identity);
        fireball.GetComponent<FireBallView>().Init(this, side);
    }

    public void OnLiftMoveComplete(int currentFloor) {
        var monsters = monsterController.GetMonsterModelOnFloor(currentFloor);
        if (monsters.Count != 0) {
            ShootFireball(monsters[0].spawnSide);
        }
    }

}