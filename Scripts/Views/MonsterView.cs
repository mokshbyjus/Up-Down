using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour {
    public MonsterController monsterController;
    private Rigidbody2D monsterRb;
    private Coroutine moveCr;
    public MonsterModel myModel;

    private void Start() {
        // StartMoving();
        monsterRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.K)) {
            TakeOneStep();
        }
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
            moveCr = StartCoroutine(MoveCoroutine(monsterController.monsterSpeed));
        } else {
            moveCr = StartCoroutine(MoveCoroutine(-monsterController.monsterSpeed));
        }

    }

    public void TakeOneStep() {
        if (myModel.spawnSide == Side.LEFT) {
            monsterRb.AddForce(monsterRb.transform.right * monsterController.monsterSpeed, ForceMode2D.Impulse);
        } else {
            // moveCr = StartCoroutine(MoveCoroutine(-monsterController.monsterSpeed));
            monsterRb.AddForce(monsterRb.transform.right * -monsterController.monsterSpeed, ForceMode2D.Impulse);
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

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            monsterRb.velocity = Vector2.zero;
            monsterController.OnFall(myModel, gameObject);
            // StopCoroutine(moveCr);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "FireBall") {
            Destroy(gameObject);
        }
    }
}