using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDeath : MonoBehaviour {

    public Vector3 offscreenOffset = new Vector3(0f, 10f, 0f);

    Board _board;
    public float deathDelay = 0f;
    public float offscreenDelay = 1f;

    public float delay = 0f;
    public Ease easeType = Ease.InOutQuint;
    public float moveTime = 0.5f;

	// Use this for initialization
	void Awake () {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
	}

    // Update is called once per frame
    public void MoveOffboard (Vector3 target) {
        transform.DOMove(target, moveTime).SetDelay(delay).SetEase(easeType);
	}

    public void Die() {
        StartCoroutine(DieCorotutine());
    }

    IEnumerator DieCorotutine() {
        yield return new WaitForSeconds(deathDelay);

        Vector3 offscreenPosition = transform.position + offscreenOffset;
        MoveOffboard(offscreenPosition);

        yield return new WaitForSeconds(moveTime + offscreenDelay);

        if(_board.capturePositions.Count != 0 && _board.CurrentCapturePosition < _board.capturePositions.Count) {
            Vector3 capturePosition = _board.capturePositions[_board.CurrentCapturePosition].position;
            transform.position = capturePosition + offscreenOffset;

            MoveOffboard(capturePosition);

            yield return new WaitForSeconds(moveTime);
            _board.CurrentCapturePosition++;
            _board.CurrentCapturePosition = Mathf.Clamp(_board.CurrentCapturePosition, 0, _board.capturePositions.Count - 1);
        }
    }
}
