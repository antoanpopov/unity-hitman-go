using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemySensor))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyManager : TurnManager {

    EnemyMover _enemyMover;
    EnemySensor _enemySensor;
    EnemyAttack _enemyAttack;
    Board _board;

    bool _isDead = false;
    public bool IsDead { get { return _isDead; } }
    public UnityEvent deathEvent;


    protected override void Awake() {

        base.Awake();

        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemySensor = GetComponent<EnemySensor>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    public void PlayTurn() {
        if (_isDead) {
            Finishturn();
            return;
        }
        StartCoroutine(PlayTurnRoutine());
    }

    IEnumerator PlayTurnRoutine() {

        if (_gameManager != null && !_gameManager.IsGameOver) {

            //detect player
            _enemySensor.UpdateSensor(_enemyMover.CurrentNode);

            //wait
            yield return new WaitForSeconds(0f);

            if (_enemySensor.FoundPlayer) {

                //notify GameManager to lose event
                _gameManager.LoseLevel();

                Vector3 playerPosition = _board.PlayerNode.coordinate;
                _enemyMover.Move(playerPosition);

                while (_enemyMover.isMoving) {
                    yield return null;
                }
                //attack player
                _enemyAttack.Attack();


            } else {
                //move
                _enemyMover.MoveOneTurn();
            }


        }
    }

    public void Die() {
        if (_isDead) {
            return;
        }

        _isDead = true;
        if(deathEvent != null) {
            deathEvent.Invoke();
        }
    }
}
