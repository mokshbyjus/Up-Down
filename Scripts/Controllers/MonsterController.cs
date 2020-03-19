using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    [SerializeField] private List<List<Transform>> monsterSpawnPosList = null;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float monsterSpeed;
    public void SpawnMonster(int floor, Side side) {
        var monster = Instantiate(monsterPrefab, monsterSpawnPosList[(int) side][floor].position, Quaternion.identity, transform);
        MonsterView monsterView = monster.GetComponent<MonsterView>();
        monsterView.Init(this, new MonsterModel(monsterSpeed, side));
        monsterView.StartMoving();
    }

    // public void 
}

public enum Side {
    LEFT,
    RIGHT
}