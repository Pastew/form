using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    public float transitionLength = 1;
    public Material fillMaterial;
    public Material edgeMaterial;

    private Camera cam;

    private bool blackAndWhite = true;
    

    void Start () {
        cam = Camera.main;	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
            SwitchColorsSlowly();
	}

    private void SwitchColorsSlowly()
    {
        StartCoroutine(SwichColorCoroutine());
    }

    public void SwitchColorsInstant()
    {
        print("SwitchColorsInstant");
        if (blackAndWhite)
        {
            fillMaterial.color = Color.black;
            edgeMaterial.color = Color.black;
            cam.backgroundColor = Color.white;
        }
        else
        {

            fillMaterial.color = Color.white;
            edgeMaterial.color = Color.white;
            cam.backgroundColor = Color.black;
        }

        blackAndWhite = !blackAndWhite;
    }

    IEnumerator SwichColorCoroutine()
    {
        print("SwichColorCoroutine");
        
        for (float elapsedTime = 0; elapsedTime < transitionLength; elapsedTime += Time.deltaTime)
        {
            if (blackAndWhite)
            {
                fillMaterial.color = Color.Lerp(Color.white, Color.black, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(Color.white, Color.black, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(Color.black, Color.white, elapsedTime / transitionLength);
            }
            else
            {
                fillMaterial.color = Color.Lerp(Color.black, Color.white, elapsedTime / transitionLength);
                edgeMaterial.color = Color.Lerp(Color.black, Color.white, elapsedTime / transitionLength);
                cam.backgroundColor = Color.Lerp(Color.white, Color.black, elapsedTime / transitionLength);
            }

            yield return new WaitForEndOfFrame();
        }
        blackAndWhite = !blackAndWhite;
    }
}
