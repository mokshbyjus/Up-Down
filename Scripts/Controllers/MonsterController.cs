using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    [SerializeField] private List<List<Transform>> monsterSpawnPosList = null;
    [SerializeField] private List<MonsterInfo> monsterInfos;
    [SerializeField] private GameObject monsterPrefab;
    public float monsterSpeed;

    private void Start() {

    }

    private void InitMonsters() {
        for (int i = 0, j = monsterInfos.Count; i < j; i++) {
            for (int k = 0, l = monsterInfos[i].monsterGo.Count; k < l; i++) {
                monsterInfos[i].monsterGo[k].GetComponent<MonsterView>().Init(this, new MonsterModel(monsterSpeed, Side.LEFT));
            }
        }
    }

    public void SpawnMonster(int floor, Side side) {
        var monster = Instantiate(monsterPrefab, monsterSpawnPosList[(int) side][floor].position, Quaternion.identity, transform);
        MonsterView monsterView = monster.GetComponent<MonsterView>();
        monsterView.Init(this, new MonsterModel(monsterSpeed, side));
        // monsterView.StartMoving();
    }

    public int GetNumberOfMonstersOnFLoor(int floor) {
        return monsterInfos[floor].monsterGo.Count;
    }

    public List<MonsterModel> GetMonsterModelOnFloor(int floor) {
        List<MonsterModel> monsterModels = new List<MonsterModel>();
        try {
            for (int i = 0, l = monsterInfos[floor].monsterGo.Count; i < l; i++) {
                monsterModels.Add(monsterInfos[floor].monsterGo[i].GetComponent<MonsterView>().myModel);
            }
            Debug.Log(monsterModels + " sadas");
        } catch (Exception e) {
            Debug.Log(e.StackTrace);
        }
        return monsterModels;
    }

    public void OnLiftMoveComplete() {
        MoveAllMonstersOneStep();
    }

    public void MoveAllMonstersOneStep() {
        for (int i = 0, j = monsterInfos.Count; i < j; i++) {
            for (int k = 0, l = monsterInfos[i].monsterGo.Count; k < l; k++) {
                if (monsterInfos[i].monsterGo[k] != null) {
                    monsterInfos[i].monsterGo[k].GetComponent<MonsterView>().TakeOneStep();
                }
            }
        }
    }

    public void OnFall(MonsterModel monsterModel ,GameObject monsterGo) {
        monsterInfos[monsterModel.floor].monsterGo.RemoveAt(0);
    }
}

public enum Side {
    LEFT,
    RIGHT
}

[Serializable]
public class MonsterInfo {
    public List<GameObject> monsterGo;
}