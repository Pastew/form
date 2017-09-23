using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool dying = false;
 
    private PostProcessingEffects postProcessingManager;
    AudioSource audioSource;

    List<Transform> playerForms;
    private PlayerForm currentPlayerForm;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        postProcessingManager = FindObjectOfType<PostProcessingEffects>();

        currentPlayerForm = GetComponentInChildren<PlayerForm>();

        playerForms = new List<Transform>();
        for (int i = 0; i <  transform.childCount; ++i)
        {
            playerForms.Add(transform.GetChild(i));
        }

        Camera.main.GetComponent<MyCamera>().target = currentPlayerForm.transform;
    }
    

    internal void JumpEnd()
    {
        currentPlayerForm.JumpEnd();
    }

    internal void Jump()
    {
        currentPlayerForm.Jump();
        PlayRandomSound(currentPlayerForm.jumpClips);
    }

    internal void Move(float horizontal)
    {
        currentPlayerForm.Move(horizontal);
    }

    internal void Turbo( )
    {
        print("Turbo");
        currentPlayerForm.Turbo();
        postProcessingManager.BloomBoom();
    }

    internal void Stomp( )
    {
        print("Stomp");
        currentPlayerForm.Stomp();
        postProcessingManager.BloomBoom();
    }

    internal void TeleportToPosition(Vector3 position)
    {
        currentPlayerForm.TeleportToPosition(position);
    }

    internal void SwitchForm()
    {
        Vector3 pos = currentPlayerForm.transform.position;

        currentPlayerForm.gameObject.SetActive(false);

        int currentPlayerFormIndex = playerForms.IndexOf(currentPlayerForm.transform);
        print(currentPlayerFormIndex);
        int nextPlayerFormIndex = currentPlayerFormIndex + 1;
        if (nextPlayerFormIndex >= playerForms.Count)
            nextPlayerFormIndex = 0;

        print(nextPlayerFormIndex);
        currentPlayerForm = playerForms[nextPlayerFormIndex].GetComponent<PlayerForm>();

        currentPlayerForm.gameObject.SetActive(true);
        currentPlayerForm.TeleportToPosition(pos);


        Camera.main.GetComponent<MyCamera>().target = currentPlayerForm.transform;
    }

    internal void Die()
    {
        if (!dying)
            StartCoroutine("DieCoroutine");
    }

    IEnumerator DieCoroutine()
    {
        dying = true;
        PlayRandomSound(currentPlayerForm.dieClips);

        AudioSource musicAudioSource = FindObjectOfType<Music>().GetComponent<AudioSource>();
        //GetComponent<JelloBody>().IsKinematic = true;
        FindObjectOfType<PostProcessingEffects>().VignetteBoom();
        MyCamera cam = Camera.main.GetComponent<MyCamera>();
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
        currentPlayerForm.FreezePosition(true);

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
        currentPlayerForm.FreezePosition(false);
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
