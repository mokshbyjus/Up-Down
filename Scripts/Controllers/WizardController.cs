using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour {
    [SerializeField] private List<Transform> shootPoints = new List<Transform>();
    [SerializeField] private GameObject fireBallPrefab = null;
    [SerializeField] private MonsterController monsterController = null;
    [SerializeField] private GameObject idleWizard = null;
    [SerializeField] private GameObject shootingWizard = null;
    public void ShootFireball(Side side) {

        ShootingPosition(side);
        var fireball = Instantiate(fireBallPrefab, shootPoints[(int) side].position, Quaternion.identity);
        fireball.GetComponent<FireBallView>().Init(this, side);
    }

    public void ShootingPosition(Side side) {
        shootingWizard.SetActive(true);
        idleWizard.SetActive(false);
        if (side == Side.LEFT) {
            var invertedVec = new Vector3(-shootingWizard.transform.localScale.x, shootingWizard.transform.localScale.y, shootingWizard.transform.localScale.z);
            if (shootingWizard.transform.localScale.x < 0) {
                shootingWizard.transform.localScale = invertedVec;
            }
        } else if (side == Side.RIGHT) {
            if (shootingWizard.transform.localScale.x > 0) {
                var invertedVec = new Vector3(-shootingWizard.transform.localScale.x, shootingWizard.transform.localScale.y, shootingWizard.transform.localScale.z);
                shootingWizard.transform.localScale = invertedVec;
            }
        }

    }

    public void IdlePosition() {
        shootingWizard.SetActive(false);
        idleWizard.SetActive(true);
    }

    public void OnLiftMoveComplete(int currentFloor) {
        var monsters = monsterController.GetMonsterModelOnFloor(currentFloor);
        if (monsters.Count != 0) {
            ShootFireball(monsters[0].spawnSide);
        }
    }

    public void OnLiftSmash() {

    }

}