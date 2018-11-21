﻿using System;
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

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;

    // Use this for initialization
    void Awake() {
        _board = FindObjectOfType<Board>().GetComponent<Board>();
        _player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    private void Start() {
        if (_player != null && _board != null) {
            StartCoroutine(RunGameLoop());
        } else {
            Debug.LogWarning("GAMEMANAGER ERROR: no player or board found");
        }
    }

    IEnumerator RunGameLoop() {
        yield return StartCoroutine(StartLevelCoroutine());
        yield return StartCoroutine(PlayLevelCoroutine());
        yield return StartCoroutine(EndLevelCoroutine());
    }

    IEnumerator StartLevelCoroutine() {

        Debug.Log("SETUP LEVEL");
        if(setupEvent != null) {
            setupEvent.Invoke();
        }
        Debug.Log("START LEVEL");

        _player.playerInput.inputEnabled = false;
        while (!_hasLevelStarted) {
            //Show start screen
            yield return null;
        }

        if(startLevelEvent != null) {
            startLevelEvent.Invoke();
        }
    }
    IEnumerator PlayLevelCoroutine() {

        Debug.Log("PLAY LEVEL");

        _isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        _player.playerInput.inputEnabled = true;

        if (playLevelEvent != null) {
            playLevelEvent.Invoke();
        }

        while (!_isGameOver) {
            yield return null;

            _isGameOver = IsWinner();

        }

        Debug.Log("WIN! ==========");
    }

    private bool IsWinner() {
        if(_board.PlayerNode == _board.GoalNode) {
            return true;
        }
        return false;
    }

    IEnumerator EndLevelCoroutine() {
        Debug.Log("END LEVEL");
        _player.playerInput.inputEnabled = false;

        if (endLevelEvent != null) {
            endLevelEvent.Invoke();
        }

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
