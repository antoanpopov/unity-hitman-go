using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour {

    public PlayerMover playerMover;
    public PlayerInput playerInput;

	// Use this for initialization
	void Awake () {
        playerMover = GetComponent<PlayerMover>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.inputEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMover.isMoving) {
            return;
        }

        playerInput.GetKeyInput();

        if(playerInput.vertical == 0) {

            if(playerInput.horizontal < 0) {
                playerMover.MoveLeft();
            } else if(playerInput.horizontal > 0) {
                playerMover.MoveRight();
            }
        } else if (playerInput.horizontal == 0) {
            if (playerInput.vertical < 0) {
                playerMover.MoveBackward();
            } else if (playerInput.vertical > 0) {
                playerMover.MoveForward();
            }
        }
	}
}
