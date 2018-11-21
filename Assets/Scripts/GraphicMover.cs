using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum GraphicMoverMode {
    MoveTo,
    ScaleTo,
    MoveFrom
}

public class GraphicMover : MonoBehaviour {

    public GraphicMoverMode mode;

    public Transform startXform;

    public Transform endXform;

    public float moveTime = 1f;
    public float delay = 0f;
    public LoopType loopType = LoopType.Restart;
    public int loopTimes = 1;
    public Ease easeType = Ease.OutExpo;


    private void Awake() {
        if (endXform == null) {
            endXform = new GameObject(gameObject.name + "XformEnd").transform;
            endXform.position = transform.position;
            endXform.rotation = transform.rotation;
            endXform.localScale = transform.localScale;
        }

        if (startXform == null) {
            startXform = new GameObject(gameObject.name + "XformStart").transform;
            startXform.position = transform.position;
            startXform.rotation = transform.rotation;
            startXform.localScale = transform.localScale;
        }
    }

    public void Reset() {
        switch (mode) {
            case GraphicMoverMode.MoveTo:
                if (startXform != null) {
                    transform.position = startXform.position;
                }
                break;
            case GraphicMoverMode.MoveFrom:
                if (endXform != null) {
                    transform.position = endXform.position;
                }
                break;
            case GraphicMoverMode.ScaleTo:
                if(startXform != null) {
                    transform.localScale = startXform.localScale;
                }              
                break;
        }
    }

    public void Move() {
        switch (mode) {
            case GraphicMoverMode.MoveTo:
                transform.DOMove(endXform.position, moveTime).SetEase(easeType).SetDelay(delay).SetLoops(loopTimes, loopType);
                break;
            case GraphicMoverMode.MoveFrom:
                transform.DOMove(startXform.position, moveTime).SetEase(easeType).SetDelay(delay).SetLoops(loopTimes, loopType);
                break;
            case GraphicMoverMode.ScaleTo:
                transform.DOScale(endXform.localScale, moveTime).SetEase(easeType).SetDelay(delay).SetLoops(loopTimes, loopType);
                break;
        }
    }
}
