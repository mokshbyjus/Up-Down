using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallView : MonoBehaviour {
    [SerializeField] private float bulletSpeed = 10f;
    private WizardController wizardController = null;
    private Transform fireBallTransform = null;
    private Rigidbody2D fireBallRb = null;
    public void Init(WizardController wizardController, Side side) {
        this.wizardController = wizardController;
        fireBallTransform = GetComponent<Transform>();
        fireBallRb = GetComponent<Rigidbody2D>();
        Shoot(side);
    }

    public void Shoot(Side side) {
        Debug.LogError($"Shooting {side}");
        if (side == Side.LEFT) {
            fireBallTransform.localScale = new Vector3(-fireBallTransform.localScale.x, fireBallTransform.localScale.y, fireBallTransform.localScale.z);
            fireBallRb.AddForce(transform.right * -bulletSpeed, ForceMode2D.Impulse);
        } else {
            fireBallTransform.localScale = new Vector3(fireBallTransform.localScale.x, fireBallTransform.localScale.y, fireBallTransform.localScale.z);
            fireBallRb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}