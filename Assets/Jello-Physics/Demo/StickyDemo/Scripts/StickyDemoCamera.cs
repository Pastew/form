﻿/* /*
Copyright (c) 2014 David Stier

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


******Jello Physics was born out of Walabers JellyPhysics. As such, the JellyPhysics license has been include.
******The original JellyPhysics library may be downloaded at http://walaber.com/wordpress/?p=85.


Copyright (c) 2007 Walaber

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Sticky demo camera.
/// </summary>
public class StickyDemoCamera : MonoBehaviour {

	/// <summary>
	/// The target body to follow when zoomed in.
	/// </summary>
	public Transform target;

	/// <summary>
	/// The orthographic camera size when zoomed in.
	/// </summary>
	public float zoomedInSize;
	
	/// <summary>
	/// The orthographic camera size when zoomed out.
	/// </summary>
	public float zoomedOutSize;

	/// <summary>
	/// The state of the cam.
	/// </summary>
	private CameraState camState = CameraState.zoomedOut;
	private enum CameraState { zoomedIn, zoomedOut };

	/// <summary>
	/// Are we transitioning between camera states.
	/// </summary>
	private bool transitioning;

	/// <summary>
	/// The zoom button text.
	/// </summary>
	private string zoomText = "Zoom Out";

    public bool followSmooth = true;

    [Range(0,5)]
    public float followSpeed = 0.8f;

    // Shake
    //private Vector3 originPosition;
    //private Quaternion originRotation;
    public float shakeDecay = 0.01f;
    private float shakePower = 0.0f;

    // Use this for initialization
    void Start () 
	{
		//set appropriate zoom when starting.
		if(camState == CameraState.zoomedIn)
		{
			zoomText = "Zoom Out";
			StartCoroutine(SmoothChangeSize(zoomedInSize));
		}
		else
		{
			zoomText = "Zoom In";
			StartCoroutine(SmoothChangeSize(zoomedOutSize));
		}

        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = position;
	}

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () 
	{
		//exit if no target is assigned
		if(target == null)
			return;

        if (!followSmooth)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.G))
            Shake(0.2f);

        if (shakePower > 0)
        {
            transform.position = transform.position + Random.insideUnitSphere * shakePower;
            transform.rotation = new Quaternion(
            transform.rotation.x,
            transform.rotation.y,
            transform.rotation.z + Random.Range(-shakePower, shakePower) * .1f,
            transform.rotation.w);
            shakePower -= shakeDecay;
        }
        else if(Mathf.Abs(transform.rotation.z) > 0.01)
        {
            transform.Rotate(Vector3.back, transform.rotation.z * 1.5f);
        }
    }


    void FixedUpdate()
    {
        if (followSmooth)
        {
            Vector3 newPosition = target.position;
            newPosition.z = transform.position.z;
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed );
            //transform.position = transform.position + (newPosition - transform.position) * followSpeed;
            //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, followSpeed);
        }
    }

    public void Shake(float power = 0.3f, float shakeDecay = 0.01f)
    {
        shakePower = power;
        this.shakeDecay = shakeDecay;
    }

    void OnGUI()
	{
		//zoom button
		if(GUILayout.Button(zoomText) && !transitioning)
		{
			if(camState == CameraState.zoomedIn)
			{
				zoomText = "Zoom In";
				camState = CameraState.zoomedOut;
				StartCoroutine(SmoothChangeSize(zoomedOutSize));
			}
			else
			{
				zoomText = "Zoom Out";
				StartCoroutine(SmoothChangeSize(zoomedInSize));
				camState = CameraState.zoomedIn;
			}
		}
	}
	
	/// <summary>
	/// Zoom in or out smoothly
	/// </summary>
	/// <returns>IEnumerator.</returns>
	/// <param name="newSize">New size.</param>
	IEnumerator SmoothChangeSize(float newSize)
	{
		if(transitioning)//don't process if already processing.
			yield break;

		transitioning = true;//procesing.
		float duration = 1f;//the amount of time it takes to transition from the current zoom to the new zoom.
		float oldSize = Camera.main.orthographicSize;
		while(duration > 0f)
		{
			duration -= Time.deltaTime;

			//apply portion of zoom
			Camera.main.orthographicSize = newSize + duration * (oldSize - newSize);
			yield return null;
		}

		transitioning = false;//process complete.
	}
}
