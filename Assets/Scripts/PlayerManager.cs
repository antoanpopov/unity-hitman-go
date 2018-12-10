using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerDeath))]
public class PlayerManager : TurnManager {

    public PlayerMover playerMover;
    public PlayerInput playerInput;

    Board _board;

    public UnityEvent deathEvent;

	// Use this for initialization
	protected override void Awake () {

        base.Awake();

        playerMover = GetComponent<PlayerMover>();

        _board = FindObjectOfType<Board>().GetComponent<Board>();

        playerInput = GetComponent<PlayerInput>();
        playerInput.inputEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerMover.isMoving || _gameManager.CurrentTurn != Turn.Player) {
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

    public void Die() {
        if(deathEvent != null) {
            deathEvent.Invoke();
        }
    }

    void CaptureEnemies() {
        if(_board != null) {
            List<EnemyManager> enemies = _board.FindEnemiesAt(_board.PlayerNode);

            if(enemies.Count != 0) {
                foreach(EnemyManager enemy in enemies) {
                    if(enemy != null) {
                        enemy.Die();
                    }
                }
            }
        }
    }

    public override void Finishturn() {
        CaptureEnemies();
        base.Finishturn();
    }
}
