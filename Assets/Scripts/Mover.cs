using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Mover : MonoBehaviour {

    public Ease ease = Ease.OutBounce;

    public Vector3 destination;

    public bool faceDestination = false;
    public bool isMoving = false;

    public float moveSpeed = 30f;
    public float rotateSpeed = 30f;
    public float delay = 0f;

    protected Board _board;

    protected Node _currentNode;
    public Node CurrentNode { get { return _currentNode; } }

    public UnityEvent finishMovementEvent;

    protected virtual void Awake() {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
    }

    protected virtual void Start() {
        UpdateCurrentNode();
    }

    public void Move(Vector3 destinationPosition, float delayTime = 0.25f) {

        if (isMoving) {
            return;
        }

        if (_board != null) {

            Node targetNode = _board.FindNodeAt(destinationPosition);

            if(targetNode != null && _currentNode != null) {
                if (_currentNode.linkedNodes.Contains(targetNode)) {
                    StartCoroutine(MoveRoutine(destinationPosition, delayTime));
                } else {
                    Debug.Log("MOVER: " + _currentNode.name + " not connected " + targetNode.name);
                }
            }
        }
    }

    protected virtual IEnumerator MoveRoutine(Vector3 destinationPosition, float delayTime) {

        isMoving = true;
        yield return new WaitForSeconds(delayTime);

        if (transform.Find("PlayerBase") && transform.Find("PlayerBase").Find("Hitman")) {
            transform.Find("PlayerBase").Find("Hitman").DOLookAt(destinationPosition, moveSpeed * Time.deltaTime);
        }
        

        transform.DOMove(destinationPosition, moveSpeed * Time.deltaTime)
            .SetDelay(delayTime)
            .SetEase(ease);

        while (Vector3.Distance(destinationPosition, transform.position) > 0.0f) {
            yield return null;
        }

        transform.DOKill(true);
        transform.position = destinationPosition;
        isMoving = false;

        UpdateCurrentNode();
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

    protected void UpdateCurrentNode() {
        if(_board != null) {
            _currentNode = _board.FindNodeAt(transform.position);
        }
    }

    protected void FaceDestination() {
        Vector3 relativePosition = destination - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        float newY = newRotation.eulerAngles.y;

        transform.DORotate(new Vector3(0f, newY, 0f), rotateSpeed * Time.deltaTime).SetEase(ease).SetDelay(0f);
    }
}
