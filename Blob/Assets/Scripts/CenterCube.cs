using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCube : Singleton<CenterCube> {
    [SerializeField, Tooltip("The button capsules this center cube has")]
    private ButtonCapsule[] buttonCapsules;

    [SerializeField, Tooltip("Rotation Speed Modifier"), Range(1f, 10f)]
    private float rotationSpeedModifier = 1f;

    [SerializeField]
    private PlayerCube player;

    // Waiting for the player to move the cube.
    private bool waitingForPlayer;

    private bool canRotate;

    private CapsulePos lastPoppedCapsulePosition;

    private void Start() {
        waitingForPlayer = false;
        canRotate = false;

        foreach (var capsule in buttonCapsules) {
            capsule.OnCapsuleCollisionWithPlayer += HandleCapsuleCollisionWithPlayer;
        }

        PopCapsuleAtPosition(CapsulePos.Top, delegate { player.CanJump = true; });
        lastPoppedCapsulePosition = CapsulePos.Top;
    }

    public void RotateCube(bool left) {
        if (canRotate) {
            canRotate = false;
            waitingForPlayer = false;
            float rotationAngle = left ? -90f : 90f;

            StartCoroutine(LerpRotateCube(rotationAngle));
        }
    }

    private IEnumerator LerpRotateCube(float rotationAngle) {
        float init = transform.eulerAngles.z;
        float end = init + rotationAngle;

        float progress = 0;

        Vector3 temp;

        while (progress <= 1f) {
            temp = transform.eulerAngles;
            temp.z = Mathf.Lerp(init, end, progress);
            transform.eulerAngles = temp;

            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime * rotationSpeedModifier;
        }

        temp = transform.eulerAngles;
        temp.z = end;
        transform.eulerAngles = temp;

        yield return null;
    }

    private void HandleCapsuleCollisionWithPlayer(CapsulePos pos) {
        if (!waitingForPlayer) {
            waitingForPlayer = true;
            lastPoppedCapsulePosition = pos;
            HideCapsuleAtPosition(pos);

            SetNewCapsulePosition();
        }
    }

    private void SetNewCapsulePosition() {
        CapsulePos newCapsulePos = GenerateNewCapsulePositionBasedOnLastPos();

        PopCapsuleAtPosition(newCapsulePos, delegate { canRotate = true; player.CanJump = true; });
    }

    private CapsulePos GenerateNewCapsulePositionBasedOnLastPos() {
        List<CapsulePos> possibleNewPositions = new List<CapsulePos> {
            // Make the next appear either at the left or right of the cube;
            CapsulePos.Left,
            CapsulePos.Right
        };


        return possibleNewPositions[Random.Range(0, possibleNewPositions.Count)];
    }

    private void HideCapsuleAtPosition(CapsulePos pos, System.Action callback = null) {
        foreach (var capsule in buttonCapsules) {
            if (capsule.Position == pos) {
                capsule.Hide(callback);
                break;
            }
        }
    }

    private void PopCapsuleAtPosition(CapsulePos pos, System.Action callback = null) {
        foreach (var capsule in buttonCapsules) {
            if (capsule.Position == pos) {
                capsule.Pop(callback);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!waitingForPlayer && collision.gameObject.CompareTag("PlayerCube")) {
            GameManager.Instance.MakeGameOver();
        }
    }
}
