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
            Vector3 directionVector = new Vector3(direction.normalized.x, direction.normalized.y, direction.normalized.z);
            Quaternion rotation = Quaternion.LookRotation(directionVector);
            GameObject arrowInstance = Instantiate(arrowPrefab, transform.position + directionVector * startDistance, Quaternion.identity);
            arrowInstance.transform.LookAt(direction);
            arrowInstance.transform.localScale = new Vector3(scale, scale, scale);
            arrowInstance.transform.parent = transform;
            _arrows.Add(arrowInstance);
        }
    }

    void MoveArrow(GameObject arrowInstance) {
        if(arrowInstance.transform.position.x != 0) {
            arrowInstance.transform.DOMoveX(arrowInstance.transform.position.x+ (endDistance - startDistance), moveTime).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        }

        if (arrowInstance.transform.position.z != 0) {
            arrowInstance.transform.DOMoveZ(arrowInstance.transform.position.z + (endDistance - startDistance), moveTime).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        }
    }

    void MoveArrows() {
        foreach(GameObject arrow in _arrows) {
            MoveArrow(arrow);
        }
    }
}
