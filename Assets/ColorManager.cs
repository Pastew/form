using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public float transitionLength = 0.3f;
    public Material fillMaterial;
    public Material edgeMaterial;

    public Color startFillColor = Color.white;
    public Color startEdgeColor = Color.white;
    public Color startCamBackgroundColor = Color.black;

    public Color endFillColor = Color.black;
    public Color endEdgeColor = Color.black;
    public Color endCamBackgroundColor = Color.white;

    private Camera cam;
    private bool direction = false;    

    void Start () {
        cam = Camera.main;
        SwitchColorsSlowly();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
            SwitchColorsSlowly();
	}

    private void SwitchColorsSlowly()
    {
        StartCoroutine(SwichColorCoroutine());
    }

    IEnumerator SwichColorCoroutine()
    {
        for (float elapsedTime = 0; elapsedTime < transitionLength; elapsedTime += Time.deltaTime)
        {
            if (direction)
            {
                fillMaterial.color = Color.Lerp(startFillColor, endFillColor, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(startEdgeColor, endEdgeColor, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(startCamBackgroundColor, endCamBackgroundColor, elapsedTime / transitionLength);
            }
            else
            {
                fillMaterial.color = Color.Lerp(endFillColor, startFillColor, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(endEdgeColor, startEdgeColor, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(endCamBackgroundColor, startCamBackgroundColor, elapsedTime / transitionLength);
            }

            yield return new WaitForEndOfFrame();
        }
        direction = !direction;
    }
}
