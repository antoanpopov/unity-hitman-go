using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Link : MonoBehaviour {

    public float borderWidth = 0.07f;
    public float lineThickness = 0.3f;
    public float scaleTime = 1f;
    public float delay = 0.1f;

    public Ease easeType = Ease.InOutExpo;

    public void DrawLink(Vector3 startPosition, Vector3 endPosition) {
        transform.localScale = new Vector3(lineThickness, 1f, 0f);

        Vector3 directionVector = endPosition - startPosition;

        float zScale = directionVector.magnitude - borderWidth * 2f;

        Vector3 newScale = new Vector3(lineThickness, 1f, zScale);
        transform.rotation = Quaternion.LookRotation(directionVector);
        transform.position = startPosition + (transform.forward * borderWidth);

        transform
            .DOScale(newScale, scaleTime)
            .SetDelay(delay)
            .SetEase(easeType);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
