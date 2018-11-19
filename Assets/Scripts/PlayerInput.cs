using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private float _horizontal;
    public float horizontal { get { return _horizontal; } }

    private float _vertical;
    public float vertical { get { return _vertical; } }

    private bool _inputEnabled = false;
    public bool inputEnabled { get { return _inputEnabled; } set { _inputEnabled = value; } }

    public void GetKeyInput() {

        if (_inputEnabled) {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }

    }
}
