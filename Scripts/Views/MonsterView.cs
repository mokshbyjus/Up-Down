using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour {
    private Animator monsterAnimator = null;
    public MonsterController monsterController;
    private Rigidbody2D monsterRb;
    private Coroutine moveCr;
    public MonsterModel myModel;

    private void Start() {
        // StartMoving();
        monsterAnimator = GetComponent<Animator>();
        monsterRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.K)) {
            TakeOneStep();
        }
        if (Input.GetKey(KeyCode.F)) {
            FlyOff();
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

    public void FlyOff() {
        Vector2 flyOffDir = Vector2.zero;
            float randomY = Random.Range(1f,4f);
        if (myModel.spawnSide == Side.LEFT) {
            monsterAnimator.SetTrigger("FlyLeft");
            flyOffDir = new Vector2(-1, randomY);
        } else {
            monsterAnimator.SetTrigger("FlyRight");
            flyOffDir = new Vector2(1, randomY);
        }
        monsterRb.gravityScale = 0;
        GetComponent<BoxCollider2D>().enabled = false;
        monsterRb.AddForce(flyOffDir * monsterController.flyOffForce, ForceMode2D.Impulse);
    }

    #region  ------------------------- Collisions ---------------------------------

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

    #endregion --------------------------------------------------------------------
}