using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour {

    public Ease ease = Ease.OutBounce;
    public Vector3 destination;
    public bool isMoving = false;
    public float moveSpeed = 30f;
    public float delay = 0f;

    Board _board;

	void Awake () {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
	}

    void Start() {
        UpdateBoard();
        if(_board != null && _board.PlayerNode != null) {
            _board.PlayerNode.InitNode();
        }
    }

    public void Move(Vector3 destinationPosition, float delayTime = 0.25f) { 
    
        if(_board != null) {
            Node targetNode = _board.FindNodeAt(destinationPosition);
            
            if(targetNode !=null && _board.PlayerNode.linkedNodes.Contains(targetNode)){
                StartCoroutine(MoveRoutine(destinationPosition, delayTime));
            }
        
        }
    }

    IEnumerator Test() {
        MoveRight();
        yield return new WaitForSeconds(2f);
        MoveLeft();
        yield return new WaitForSeconds(2f);
        MoveForward();
        yield return new WaitForSeconds(2f);
        MoveBackward();
        yield return new WaitForSeconds(2f);
        
        
    }

    IEnumerator MoveRoutine(Vector3 destinationPosition, float delayTime) {
        isMoving = true;
        yield return new WaitForSeconds(delayTime);
        transform.DOLookAt(destinationPosition, moveSpeed * Time.deltaTime);
        transform.DOMove(destinationPosition, moveSpeed * Time.deltaTime)
            .SetDelay(delayTime)
            .SetEase(ease);

        while(Vector3.Distance(destinationPosition, transform.position) > 0.0f) {
            yield return null;
        }

        transform.position = destinationPosition;
        isMoving = false;

        UpdateBoard();
    } 

    public void MoveLeft() {
        Vector3 newPosition = transform.position + new Vector3(-Board.spacing, 0, 0);
        Move(newPosition);
    }
    public void MoveRight() {
        Vector3 newPosition = transform.position + new Vector3(Board.spacing, 0, 0);
        Move(newPosition);
    }
    public void MoveForward() {
        Vector3 newPosition = transform.position + new Vector3(0, 0, Board.spacing);
        Move(newPosition);
    }
    public void MoveBackward() {
        Vector3 newPosition = transform.position + new Vector3(0, 0, -Board.spacing);
        Move(newPosition);
    }

    void UpdateBoard() {
        if(_board != null) {
            _board.UpdatePlayerNode();
        }
    }
}
