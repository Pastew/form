using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableAnimation : Activatable
{
    public float timeToMove = 5;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private IEnumerator coroutine;

    private AudioSource activatingAudioSource;
    private AudioSource deactivatingAudioSource;

    private void Start()
    {
        startPosition = transform.localPosition;
        activatingAudioSource = GetComponents<AudioSource>()[0];
        deactivatingAudioSource = GetComponents<AudioSource>()[1];
    }

    public override void Activate()
    {
        Camera.main.GetComponent<MyCamera>().Shake(0.1f, 0.003f);
        if (coroutine != null)
            StopCoroutine(coroutine);

        deactivatingAudioSource.Stop();
        activatingAudioSource.Play();
        coroutine = MoveOverSeconds(gameObject, targetPosition, 3f);
        StartCoroutine(coroutine);
    }

    public override void Deactivate()
    {
        Camera.main.GetComponent<MyCamera>().Shake(0.1f, 0.01f);
        if (coroutine != null)
            StopCoroutine(coroutine);

        activatingAudioSource.Stop();
        deactivatingAudioSource.Play();
        coroutine = MoveOverSeconds(gameObject, startPosition, 0.5f);
        StartCoroutine(coroutine);
    }


    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            transform.localPosition= Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = end;

        activatingAudioSource.Stop();
        deactivatingAudioSource.Stop();
    }
}
