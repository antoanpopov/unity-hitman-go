using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour {

    public Vector3 directionToSearch = new Vector3(0f, 0f, 2f);

    Node _nodeToSearch;
    Board _board;

    bool _foundPlayer = false;
    public bool FoundPlayer { get { return _foundPlayer; } }

	// Use this for initialization
	void Awake () {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
	}

    public void UpdateSensor(Node enemyNode) {
        Vector3 worldSpacePositionToSearch = transform.TransformVector(directionToSearch) + transform.position;

        if(_board != null) {
            _nodeToSearch = _board.FindNodeAt(worldSpacePositionToSearch);

            if (!enemyNode.linkedNodes.Contains(_nodeToSearch)) {
                _foundPlayer = false;
                return;
            }

            if(_nodeToSearch == _board.PlayerNode) {
                _foundPlayer = true;
            }
        }
    }
	
}
