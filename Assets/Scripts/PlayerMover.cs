using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : Mover {

    PlayerCompass _playerCompass;

    protected override void Awake() {
        base.Awake();
        _playerCompass = FindObjectOfType<PlayerCompass>().GetComponent<PlayerCompass>();
    }

    protected override void Start() {
        base.Start();
        UpdateBoard();
    }

    void UpdateBoard() {
        if(_board != null) {
            _board.UpdatePlayerNode();
        }
    }

    protected override IEnumerator MoveRoutine(Vector3 destinationPosition, float delayTime) {

        if (_playerCompass != null) {
            _playerCompass.ShowArrows(false);
        }

        yield return StartCoroutine(base.MoveRoutine(destinationPosition, delayTime));

        UpdateBoard();

        if (_playerCompass != null) {
            _playerCompass.ShowArrows(true);
        }

        base.finishMovementEvent.Invoke();
    }
}
