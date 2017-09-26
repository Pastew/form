using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMusic : MonoBehaviour {

    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        if (!GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();

        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(int i)
    {
        if (audioClips[i] != null) {
            audioSource.clip = audioClips[i];
            audioSource.Play();
        }
    }
}
