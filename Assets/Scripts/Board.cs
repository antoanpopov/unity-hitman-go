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
    public List<Node> allNodes { get { return _allNodes; } }

    public void GetNodeList() {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        _allNodes = new List<Node>(nodeList);
    }

    void Awake() {
        GetNodeList();
    }

    public Node FindNodeAt(Vector3 position) {
        Vector3 boardPosition = Utility.Vector3Round(position);
        return _allNodes.Find(n => n.coordinate == boardPosition);
    }
}
