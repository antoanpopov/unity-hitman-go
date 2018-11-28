using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCompass : MonoBehaviour {

    Board _board;

    public GameObject arrowPrefab;

    List<GameObject> _arrows = new List<GameObject>();

    public float scale = 1f;

    public float startDistance = 0.25f;
    public float endDistance = 0.5f;

    public float moveTime = 1f;
    public Ease easeType = Ease.OutExpo;
    public float delay = 0f;

    private void Awake() {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        SetupArrows();
        MoveArrows();

    }

    void SetupArrows() {
        if(arrowPrefab == null) {
            Debug.LogWarning("PAYERCOMPASS SetupArrows: ERROR missing arrow prefab");
        }

        foreach(Vector3 direction in Board.directions) {
            Vector3 normalizedDirection = new Vector3(direction.normalized.x, direction.normalized.y, direction.normalized.z);
            Quaternion rotation = Quaternion.LookRotation(normalizedDirection);

            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position + normalizedDirection * startDistance, rotation);
            arrowInstance.transform.localScale = new Vector3(scale, scale, scale);
            arrowInstance.transform.parent = transform;
            _arrows.Add(arrowInstance);
        }
    }

    void MoveArrow(GameObject arrowInstance) {        
        arrowInstance.transform.DOMove(arrowInstance.transform.position + arrowInstance.transform.forward * (endDistance - startDistance), moveTime).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }

    void MoveArrows() {
        for(int i = 0; i < _arrows.Count; i++) {
            MoveArrow(_arrows[i]);
        }
    }

    public void ShowArrows(bool state) {
        if(_board == null) {
            Debug.LogWarning("PLAYARCOMPASS ShowArrows ERROR: no Board found!");
            return;
        }
        if (_arrows == null || _arrows.Count != Board.directions.Length) {
            Debug.LogWarning("PLAYARCOMPASS ShowArrows ERROR: no arrows found!");
            return;
        }

        if(_board.PlayerNode != null) {
           
            for(int i = 0; i < Board.directions.Length; i++) {
                Node neighbor = _board.PlayerNode.FindNeighborAt(Board.directions[i]);

                

                if(neighbor == null || !state) {
                    _arrows[i].SetActive(false);
                } else {
                    bool activeState = _board.PlayerNode.linkedNodes.Contains(neighbor);
                    _arrows[i].SetActive(activeState);
                }
            }
        }

        ResetArrows();
        MoveArrows();
    }

     void ResetArrows() {
        for(int i = 0; i < _arrows.Count; i++) {
            _arrows[i].transform.position = transform.position + Board.directions[i] * (endDistance - startDistance);
            _arrows[i].transform.DOKill();
        }
    }
}
