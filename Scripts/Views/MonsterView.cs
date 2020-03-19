using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour {
    private MonsterController monsterController;
    private Rigidbody2D monsterRb;
    private Coroutine moveCr;
    private MonsterModel myModel;

    private void Start() {
        StartMoving();
        monsterRb = GetComponent<Rigidbody2D>();
    }

    public void Init(MonsterController monsterController, MonsterModel monsterModel) {
        monsterRb = GetComponent<Rigidbody2D>();
        this.monsterController = monsterController;
        myModel = monsterModel;
    }

    public void StartMoving() {
        // float distance = Vector3.Distance(transform.position, position);
        // float time = distance / speed;
        if (myModel.spawnSide == Side.LEFT) {
            moveCr = StartCoroutine(MoveCoroutine(myModel.speed));
        } else {
            moveCr = StartCoroutine(MoveCoroutine(-myModel.speed));
        }

    }

    private IEnumerator MoveCoroutine(float speed) {
        Debug.Log("Working");
        for (;;) {
            if (monsterRb == null) {
                monsterRb = GetComponent<Rigidbody2D>();
            }
            monsterRb.velocity = Vector2.right * speed;
            yield return new WaitForFixedUpdate();
        }
    }
}