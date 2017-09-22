using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour {

    public Sprite[] cloudSprites;
    public float minDelayBetweenCreatingClouds = 3;
    public float maxDelayBetweenCreatingClouds = 5;
    public float minSpeed = 1, maxSpeed = 4;
    public int cloudsCreatedOnStart = 35;

    private float maxY, minY;
    private float timeToCreateCloud = 0;

    private GameObject cloudPrefab;

    private void Start()
    {
        maxY = GetComponentInChildren<BoxCollider2D>().bounds.size.y/2;
        minY = -maxY;

        Vector3 cloudDestroyerPos = GetComponentInChildren<CloudDestroyer>().transform.localPosition;
        for (float x = 0; x < cloudDestroyerPos.x; x += cloudDestroyerPos.x / cloudsCreatedOnStart)
            CreateCloud(x);
    }

    void Update () {
        timeToCreateCloud -= Time.deltaTime;	
        if(timeToCreateCloud < 0)
        {
            CreateCloud();
            timeToCreateCloud = UnityEngine.Random.Range(minDelayBetweenCreatingClouds, maxDelayBetweenCreatingClouds);
        }
    }

    private void CreateCloud(float x=0)
    {
        GameObject newCloud = new GameObject();
        int cloudSpriteIndex = UnityEngine.Random.Range(0, cloudSprites.Length - 1);

        SpriteRenderer spriteRenderer = newCloud.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = cloudSprites[cloudSpriteIndex];
        spriteRenderer.sortingLayerName = "Background";
        spriteRenderer.flipX = UnityEngine.Random.Range(-1,1)>0;
        spriteRenderer.flipY = UnityEngine.Random.Range(-1, 1) > 0;

        newCloud.AddComponent<Cloud>().speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        newCloud.AddComponent<CircleCollider2D>().isTrigger = true;
        newCloud.AddComponent<Rigidbody2D>().isKinematic = true;

        newCloud.transform.parent = transform;
        newCloud.transform.position = new Vector2(transform.position.x + x, transform.position.y + UnityEngine.Random.Range(minY, maxY));
    }
}
