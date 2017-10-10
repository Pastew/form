using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAd : MonoBehaviour {

	void StartFromAssets () {
        print("hehe");
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("InGameAds/StubAd");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ReplaceAd();
    }

    private void ReplaceAd()
    {
        StartCoroutine("ReplaceAdCoroutine");
    }

    public string url = "http://blog.visme.co/wp-content/uploads/2015/10/color1.jpg";

    IEnumerator ReplaceAdCoroutine()
    {
        WWW www = new WWW(url);
        yield return www;
        Sprite s = gameObject.GetComponent<SpriteRenderer>().sprite;
        print(s.bounds.size.x);
        s = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }

    IEnumerator Start2()
    {
        WWW www = new WWW(url);
        yield return www;
        Sprite s = gameObject.GetComponent<SpriteRenderer>().sprite;
        print(s.bounds.size.x);
        s = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
}
