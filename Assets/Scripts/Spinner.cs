using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spinner : MonoBehaviour {

    public float rotateSpeed = 20f;

	// Use this for initialization
	void Start () {
        transform.DORotate(new Vector3(0f, 180f, 0f), rotateSpeed).SetLoops(-1,LoopType.Incremental).SetEase(Ease.Linear);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
