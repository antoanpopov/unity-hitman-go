using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Node : MonoBehaviour {

    public GameObject geometry;

    public float delay = 1f;
    public float scaleTime = 0.3f;

    public Ease easeType = Ease.InExpo;

    public bool autoRun = false;

	void Start () {
		if(geometry != null) {
            geometry.transform.localScale = Vector3.zero;

            if (autoRun) {
                ShowGeometry();
            }
        }
	}

    public void ShowGeometry() {
        if(geometry != null) {
            geometry.transform
                .DOScale(Vector3.one, scaleTime)
                .SetDelay(delay)
                .SetEase(easeType);
        }
    }
	
}
