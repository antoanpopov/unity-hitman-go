using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour {

    public Ease ease = Ease.OutBounce;
    public Vector3 destination;
    public bool isMoving = false;
    public float moveSpeed = 1.5f;
    public float delay = 0f;

	// Use this for initialization
	void Start () {
	}
	
    public void Move(Vector3 destinationPosition, float delayTime = 0.25f) { 
        StartCoroutine(MoveRoutine(destinationPosition, delayTime));
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

        transform.DOMove(destinationPosition, 2f)
            .SetDelay(delayTime)
            .SetEase(ease);

        while(Vector3.Distance(destinationPosition, transform.position) > 0.0f) {
            yield return null;
        }

        transform.position = destinationPosition;
        isMoving = false;
    } 

    public void MoveLeft() {
        Vector3 newPosition = transform.position + new Vector3(-2, 0, 0);
        Move(newPosition);
    }
    public void MoveRight() {
        Vector3 newPosition = transform.position + new Vector3(2, 0, 0);
        Move(newPosition);
    }
    public void MoveForward() {
        Vector3 newPosition = transform.position + new Vector3(0, 0, 2);
        Move(newPosition);
    }
    public void MoveBackward() {
        Vector3 newPosition = transform.position + new Vector3(0, 0, -2);
        Move(newPosition);
    }
}
