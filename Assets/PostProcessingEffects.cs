using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingEffects : MonoBehaviour {

    [Header("Bloom Boom!")]
    [SerializeField] private float boomBloomFadeInSpeed = 5f;
    [SerializeField] private float boomBloomMaxValue = 15f;
    [SerializeField] private float boomBloomFadeOutSpeed = 0.2f;
    [SerializeField] private float boomBloomMinValue = 0f;
    [SerializeField] private bool boomBloomDisableAfterBoom = false;

    private PostProcessingProfile profile;
    private BloomModel.Settings bloomSettings;


    void Start () {
        profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        bloomSettings = profile.bloom.settings;
    }


    public void BloomBoom()
    {
        StartCoroutine("BloomBoomCoroutine");
    }

    IEnumerator BloomBoomCoroutine()
    {
        profile.bloom.enabled = true;

        for (float i = boomBloomMinValue; i < boomBloomMaxValue; i += boomBloomFadeInSpeed)
        {
            bloomSettings.bloom.intensity = i;
            profile.bloom.settings = bloomSettings;
            yield return new WaitForEndOfFrame();
        }

        for (float i = boomBloomMaxValue; i > boomBloomMinValue; i -= boomBloomFadeOutSpeed)
        {
            bloomSettings.bloom.intensity = i;
            profile.bloom.settings = bloomSettings;
            yield return null;
        }

        if (boomBloomDisableAfterBoom)
            profile.bloom.enabled = false;
    }

}
