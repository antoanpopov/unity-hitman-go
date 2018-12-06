using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType {
    Stationary,
    Patrol
}

public class EnemyMover : Mover {

    public Vector3 directionToMove = new Vector3(0f, 0f, Board.spacing);

    public MovementType movementType = MovementType.Stationary;

    public float standTime = 1f;
    public float rotateTime = 1f;


    protected override void Awake() {
        base.Awake();
        faceDestination = true;
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    public void MoveOneTurn() {

        switch (movementType) {
            case MovementType.Patrol:
                Patrol();
                break;
            case MovementType.Stationary:
                Stand();
                break;
        }
        Stand();
    }

    void Patrol() {
        StartCoroutine(PatrolCoroutine());
    }

    IEnumerator PatrolCoroutine() {
        Vector3 startPosition = _currentNode.coordinate;
        Vector3 newDestination = startPosition + transform.TransformVector(directionToMove);
        Vector3 nextDestination = startPosition + transform.TransformVector(directionToMove * 2);

        Move(newDestination, 0f);

        while (isMoving) {
            yield return null;
        }     

        if(_board != null) {
            Node newDestinationNode = _board.FindNodeAt(newDestination);
            Node nextDestinationNode = _board.FindNodeAt(nextDestination);

            if(nextDestinationNode == null) {
                destination = startPosition;
                FaceDestination();

                yield return new WaitForSeconds(rotateTime);
            }
        }

        base.finishMovementEvent.Invoke();

    }

    void Stand() {
        StartCoroutine(StandCoroutine());
    }

    IEnumerator StandCoroutine() {
        yield return new WaitForSeconds(standTime);
        base.finishMovementEvent.Invoke();
    }
}
