using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Node : MonoBehaviour {

    Vector3 _coordinate;
    public Vector3 coordinate { get { return Utility.Vector3Round(_coordinate); } }

    List<Node> _neighborNodes = new List<Node>();
    public List<Node> neighborNodes { get { return _neighborNodes; } }

    List<Node> _linkedNodes = new List<Node>();
    public List<Node> linkedNodes { get { return _linkedNodes; } }

    Board _board;

    public GameObject geometry;
    public GameObject linkPrefab;

    public float delay = 1f;
    public float scaleTime = 0.3f;

    public Ease easeType = Ease.InExpo;

    public bool autoRun = false;
    bool _isInitialized = false;

    public LayerMask obstacleLayer;

    void Awake() {
        _board = FindObjectOfType<Board>();
        _coordinate = transform.position;
    }

    void Start() {
        if (geometry != null) {
            geometry.transform.localScale = Vector3.zero;

            if (autoRun) {
                InitNode();
            }

            if (_board != null) {
                _neighborNodes = FindNeighbors(_board.allNodes);
            }
        }
    }

    public void ShowGeometry() {
        if (geometry != null) {
            geometry.transform
                .DOScale(Vector3.one, scaleTime)
                .SetDelay(delay)
                .SetEase(easeType);
        }
    }

    public List<Node> FindNeighbors(List<Node> nodes) {
        List<Node> nodeList = new List<Node>();

        foreach (Vector3 dir in Board.directions) {
            Node foundNeighbor = nodes.Find(n => n.coordinate == coordinate + dir);

            if (foundNeighbor != null && !nodeList.Contains(foundNeighbor)) {
                nodeList.Add(foundNeighbor);
            }
        }

        return nodeList;
    }

    public void InitNode() {

        if (!_isInitialized) {
            ShowGeometry();
            InitNeighbors();
            _isInitialized = true;
        }
    }

    void InitNeighbors() {
        StartCoroutine(InitNeighborsCoroutine());
    }

    IEnumerator InitNeighborsCoroutine() {
        yield return new WaitForSeconds(delay);

        foreach (Node node in _neighborNodes) {
            if (!_linkedNodes.Contains(node)) {
                Obstacle obstacle = FindObstacle(node);
                if(obstacle == null) {
                    LinkNode(node);
                    node.InitNode();
                }
            }           
        }
    }

    void LinkNode(Node targetNode) {
        if (linkPrefab != null) {
            GameObject linkInstance = Instantiate(linkPrefab, transform.position, Quaternion.identity);
            linkInstance.transform.parent = transform;

            Link link = linkInstance.GetComponent<Link>();
            if (link != null) {
                link.DrawLink(transform.position, targetNode.transform.position);
            }

            if (!_linkedNodes.Contains(targetNode)) {
                _linkedNodes.Add(targetNode);
            }

            if (!targetNode.linkedNodes.Contains(this)){
                targetNode.linkedNodes.Add(this);
            }

        }
    }

    Obstacle FindObstacle(Node targetNode) {
        Vector3 checkDirection = targetNode.transform.position - transform.position;
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, checkDirection, out raycastHit, Board.spacing + 0.1f, obstacleLayer)) {
            Debug.Log("Node FindObstacle: Hit an obstacle from" + this.name + " to " + targetNode.name);
            return raycastHit.collider.GetComponent<Obstacle>();
        }
        return null;
    }
}
