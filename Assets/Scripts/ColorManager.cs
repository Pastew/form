using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public float transitionLength = 0.3f;
    public Material fillMaterial;
    public Material edgeMaterial;

    public Color primaryFillColor = Color.white;
    public Color primaryEdgeColor = Color.white;
    public Color primaryCamColor = Color.black;

    public Color secondaryFillColor = Color.black;
    public Color secondaryEdgeColor = Color.black;
    public Color secondaryCamColor = Color.white;

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

    public void SwitchColorsSlowly()
    {
        StartCoroutine(SwichColorCoroutine());
    }

    IEnumerator SwichColorCoroutine()
    {
        for (float elapsedTime = 0; elapsedTime < transitionLength; elapsedTime += Time.deltaTime)
        {
            if (direction)
            {
                fillMaterial.color = Color.Lerp(primaryFillColor, secondaryFillColor, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(primaryEdgeColor, secondaryEdgeColor, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(primaryCamColor, secondaryCamColor, elapsedTime / transitionLength);
            }
            else
            {
                fillMaterial.color = Color.Lerp(secondaryFillColor, primaryFillColor, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(secondaryEdgeColor, primaryEdgeColor, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(secondaryCamColor, primaryCamColor, elapsedTime / transitionLength);
            }

            yield return new WaitForEndOfFrame();
        }
        direction = !direction;
    }
}
