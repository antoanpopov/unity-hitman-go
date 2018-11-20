using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public static float spacing = 2f;

    public static readonly Vector3[] directions = {
        new Vector3(spacing, 0f, 0f),
        new Vector3(-spacing, 0f, 0f),
        new Vector3(0f, 0f, spacing),
        new Vector3(0f, 0f, -spacing),
    };

    List<Node> _allNodes = new List<Node>();
    public List<Node> AllNodes { get { return _allNodes; } }

    private Node _playerNode;
    public Node PlayerNode { get { return _playerNode; } }

    PlayerMover _player;

    void Awake() {
        _player = FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();
    }

    public void GetNodeList() {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        _allNodes = new List<Node>(nodeList);
    }

    public Node FindNodeAt(Vector3 position) {
        Vector3 boardPosition = Utility.Vector3Round(position);
        return _allNodes.Find(n => n.coordinate == boardPosition);
    }

    public Node FindPlayerNode() {
        if (_player != null && !_player.isMoving) {
            return FindNodeAt(_player.transform.position);
        }
        return null;
    }

    public void UpdatePlayerNode() {
        _playerNode = FindPlayerNode();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
        if(_playerNode != null) {
            Gizmos.DrawSphere(_playerNode.transform.position, 0.3f);
        }
    }
}
