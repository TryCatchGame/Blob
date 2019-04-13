using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour {

    [SerializeField]
    private float playerJumpForce;

    [SerializeField]
    private Vector3 playerCubeSize;

    [SerializeField, Range(1f, 5f)]
    private float startingExpandMultiplier;

    private Rigidbody playerRB;

    public bool CanJump { get; set; }

    private void Start() {
        playerRB = GetComponent<Rigidbody>();
        CanJump = false;

        StartCoroutine(StartPlayer());
    }

    private IEnumerator StartPlayer() {

        Vector3 start = new Vector3(0, 0, 0);
        Vector3 end = playerCubeSize;

        float progress = 0f;
        while (progress < 1f) {
            transform.localScale = Vector3.Lerp(start, end, progress);

            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime * startingExpandMultiplier;
        }

        transform.localScale = end;
        playerRB.useGravity = true;
        GameManager.Instance.StartGame();

        yield return null;
    }

    public void JumpPlayer() {
        if (CanJump) {
            playerRB.AddForce(0, playerJumpForce, 0);
            CanJump = false;
        }
    }
}
