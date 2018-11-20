using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour {

    public Color solidColor = Color.white;
    public Color clearColor = new Color(1f, 1f, 1f, 0f);

    public float delay = 0.5f;
    public float timeToFade = 1f;
    public Ease easeType = Ease.OutExpo;

    MaskableGraphic graphic;

    void Awake() {
        graphic = GetComponent<MaskableGraphic>();
    }

    void UpdateColor(Color newColor) {
        graphic.color = newColor;
    }

    public void FadeOff() {
        graphic.DOFade(0f, timeToFade)
            .SetDelay(delay)
            .SetEase(easeType);
    }

    public void FadeOn() {
        graphic.DOFade(1f, timeToFade)
            .SetDelay(delay)
            .SetEase(easeType);
    }
}
