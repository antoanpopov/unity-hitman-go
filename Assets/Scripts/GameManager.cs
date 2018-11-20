using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Board _board;
    PlayerManager _player;

    bool _hasLevelStarted = false;
    public bool HasLevelStarted { get { return _hasLevelStarted; } set { _hasLevelStarted = value; } }

    bool _isGamePlaying = false;
    public bool IsGamePlaying { get { return _isGamePlaying; } set { _isGamePlaying = value; } }

    bool _isGameOver = false;
    public bool IsGameOver { get { return _isGameOver; } set { _isGameOver = value; } }

    bool _hasLevelFinished = false;
    public bool HasLevelFinished { get { return _hasLevelFinished; } set { _hasLevelFinished = value; } }

    public float delay = 1f;

    // Use this for initialization
    void Awake() {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    private void Start() {
        if (_player != null && _board != null) {
            StartCoroutine(RunGameLoop());
        }
    }

    IEnumerator RunGameLoop() {
        yield return StartCoroutine(StartLevelCoroutine());
        yield return StartCoroutine(PlayLevelCoroutine());
        yield return StartCoroutine(EndLevelCoroutine());
    }

    IEnumerator StartLevelCoroutine() {
        Debug.Log("START LEVEL");

        _player.playerInput.inputEnabled = false;
        while (!_hasLevelStarted) {
            //Show start screen
            yield return null;
        }
    }
    IEnumerator PlayLevelCoroutine() {
        Debug.Log("PLAY LEVEL");
        _isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        _player.playerInput.inputEnabled = true;

        while (!_isGameOver) {
            yield return null;
        }

    }
    IEnumerator EndLevelCoroutine() {
        Debug.Log("END LEVEL");
        _player.playerInput.inputEnabled = false;
        while (!_hasLevelFinished) {


            yield return null;
        }

        RestartLevel();
    }

    void RestartLevel() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel() {
        _hasLevelStarted = true;
    }
}
