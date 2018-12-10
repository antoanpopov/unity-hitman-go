using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    Node _goalNode;
    public Node GoalNode { get { return _goalNode; } }

    public GameObject goalPrefab;
    public float drawGoalTime = 2f;
    public float drawGoalDelay = 2f;
    public Ease drawGoalEaseType = Ease.OutExpo;

    [Header("Capture Slots")]
    public List<Transform> capturePositions;
    int _currentCapturePosition = 0;
    public int CurrentCapturePosition { get { return _currentCapturePosition; } set { _currentCapturePosition = value; } }
    public float capturePositionIconSize = 0.4f;
    public Color capturePositionIconColor = Color.blue;

    void Awake() {
        _player = FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();
        _goalNode = FindGoalNode();
    }

    public void GetNodeList() {
        Node[] nodeList = GameObject.FindObjectsOfType<Node>();
        _allNodes = new List<Node>(nodeList);
    }

    public Node FindNodeAt(Vector3 position) {
        Vector3 boardPosition = Utility.Vector3Round(position);
        return _allNodes.Find(n => n.coordinate == boardPosition);
    }

    public Node FindGoalNode() {
        return _allNodes.Find(n => n.isLevelGoal);
    }

    public Node FindPlayerNode() {
        if (_player != null && !_player.isMoving) {
            return FindNodeAt(_player.transform.position);
        }
        return null;
    }

    public List<EnemyManager> FindEnemiesAt(Node node) {
        List<EnemyManager> foundEnemies = new List<EnemyManager>();
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>() as EnemyManager[];

        foreach(EnemyManager enemy in enemies) {
            EnemyMover mover = enemy.GetComponent<EnemyMover>();

            if(mover.CurrentNode == node) {
                foundEnemies.Add(enemy);
            }
        }

        return foundEnemies;
    }

    public void UpdatePlayerNode() {
        _playerNode = FindPlayerNode();
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
        if(_playerNode != null) {
            Gizmos.DrawSphere(_playerNode.transform.position, 0.3f);
        }

        Gizmos.color = capturePositionIconColor;
        foreach (Transform capturePosition in capturePositions) {
            Gizmos.DrawCube(capturePosition.position, Vector3.one * capturePositionIconSize);
        }
    }

    public void DrawGoal() {
        if(goalPrefab != null && _goalNode != null) {
            GameObject goalInstance = Instantiate(goalPrefab, _goalNode.transform.position, Quaternion.identity);
            goalInstance.transform.localScale = Vector3.zero;
            goalInstance.transform.DOScale(Vector3.one, drawGoalTime)
            .SetDelay(drawGoalDelay)
            .SetEase(drawGoalEaseType);
        }
    }

    public void InitBoard() {
        if (_playerNode != null) {
            _playerNode.InitNode();
        }
    }
}
