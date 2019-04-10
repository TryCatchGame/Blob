using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour {

    [SerializeField]
    private float playerJumpForce;

    private Rigidbody playerRB;

    public bool CanJump { get; set; }

    void Start() {
        playerRB = GetComponent<Rigidbody>();
        CanJump = false;
    }

    public void JumpPlayer() {
        if (CanJump) {
            playerRB.AddForce(0, playerJumpForce, 0);
            CanJump = false;
        }
    }
}
