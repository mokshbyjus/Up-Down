using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class CameraView : MonoBehaviour {
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private LiftController liftController = null;
    [SerializeField] private float offsetTravelTime = 10f;
    [SerializeField] private float upOffset = 10f;
    [SerializeField] private float downOffset = 10f;
    [SerializeField] private Ease cameraEase = new Ease();
    [SerializeField] float effectThreshold = 0;
    private Sequence lerpTween = DOTween.Sequence();
    private Coroutine startCameraCr = null;
    private Coroutine endCameraCr = null;
    public int dir = 1;
    Transform cameraTransform;

    private void Start() {
        cameraTransform = GetComponent<Transform>();
    }

    private IEnumerator CameraMovementCoroutine(Vector3 endPoint, float offset) {
        // float offset = Vector3.Distance(cameraTransform.position, endPoint);
        lerpTween
            .Append(cameraTransform.DOMove(endPoint, offsetTravelTime))
            .SetEase(cameraEase);
        lerpTween.Play();
        while (liftController.isLiftMoving) {
            if (playerTransform.position.y < cameraTransform.position.y) {
                continue;
            }
            Debug.LogError("MOVING");
            cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartCamera(int direction, int threshold) {
        if (threshold < effectThreshold) {
            return;
        }
        Vector3 startPoint = cameraTransform.position;
        Vector3 endPoint = new Vector3();
        float offset = 0;
        if (direction == 1) {
            endPoint = new Vector3(playerTransform.position.x, playerTransform.position.y + upOffset, cameraTransform.position.z);
            offset = upOffset;
        } else if (direction == -1) {
            endPoint = new Vector3(playerTransform.position.x, playerTransform.position.y - downOffset, cameraTransform.position.z);
            offset = -downOffset;
        }
        startCameraCr = StartCoroutine(CameraMovementCoroutine(endPoint, offset));
    }

    public void EndCamera(int direction = 0) {
        lerpTween.Kill();
        if (startCameraCr != null) {
            StopCoroutine(startCameraCr);
        }
        cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
    }

}