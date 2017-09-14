using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool dying = false;
 
    private PostProcessingEffects postProcessingManager;
    AudioSource audioSource;

    PlayerForm[] playerForms;
    private PlayerForm playerForm;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        postProcessingManager = FindObjectOfType<PostProcessingEffects>();

        playerForms = GetComponentsInChildren<PlayerForm>();
        foreach (PlayerForm pf in playerForms)
        {
            if (pf.gameObject.activeSelf)
                playerForm = pf;
        }
    }

    internal void Jump(float jumpForce)
    {
        playerForm.Jump(jumpForce);
        PlayRandomSound(playerForm.jumpClips);
    }

    internal void Move(float horizontal, float maxSpeed, float rollForce, float moveForce)
    {
        playerForm.Move(horizontal, maxSpeed, rollForce, moveForce);
    }

    internal void Turbo(float turboPower)
    {
        print("Turbo");
        playerForm.Turbo(turboPower);
        postProcessingManager.BloomBoom();
    }

    internal void Stomp(float stompPower)
    {
        print("Stomp");
        playerForm.Stomp(stompPower);
        postProcessingManager.BloomBoom();
    }

    internal void TeleportToPosition(Vector3 position)
    {
        playerForm.TeleportToPosition(position);
    }

    internal void Die()
    {
        if (!dying)
            StartCoroutine("DieCoroutine");
    }

    IEnumerator DieCoroutine()
    {
        dying = true;
        PlayRandomSound(playerForm.dieClips);

        AudioSource musicAudioSource = FindObjectOfType<Music>().GetComponent<AudioSource>();
        //GetComponent<JelloBody>().IsKinematic = true;
        FindObjectOfType<PostProcessingEffects>().VignetteBoom();
        StickyDemoCamera cam = Camera.main.GetComponent<StickyDemoCamera>();
        cam.Shake(0.4f);
        float camSpeed = cam.followSpeed;
        cam.followSpeed = 0.03f;

        for (float t = 1; t > 0.1f; t -= Time.deltaTime)
        {
            musicAudioSource.pitch = t;
            yield return new WaitForEndOfFrame();
        }

        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        playerForm.FreezePosition(true);

        for (float t = 0; t < 3; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        for (float t = 0.1f; t <= 1 ; t += Time.deltaTime)
        {
            musicAudioSource.pitch = t;
            yield return new WaitForEndOfFrame();
        }
        musicAudioSource.pitch = 1;
        cam.followSpeed = camSpeed;
        playerForm.FreezePosition(false);
        dying = false;
    }

    internal Vector3 GetPosition()
    {
        return transform.position;
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        int index = UnityEngine.Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[index]);
    }
}
