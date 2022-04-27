using System.Collections;
using UnityEngine;
using Vuforia;

public class VBEngineEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject saturnArena;
	public float rotationSpeed = 1;
	private bool doSaturnAnimation = false;

	public UnityEngine.Video.VideoPlayer player;
	public GameObject ProgressCylinder;

    // Start is called before the first frame update
    void Start()
    {
		// Register Virtual Button Events
		VirtualButtonBehaviour[] vbArr = GetComponentsInChildren<VirtualButtonBehaviour> ();
		foreach (VirtualButtonBehaviour vb in vbArr) {
			vb.RegisterEventHandler(this);
			// Debug.Log("--------------------Button registered: " + vb.VirtualButtonName);

		}
    }

	public void OnButtonPressed (VirtualButtonBehaviour vb)
	{
        string buttonName = vb.VirtualButtonName;
        if (buttonName == "animate_saturn")
        {
			doSaturnAnimation = !doSaturnAnimation;
        }
        else
        {
            Debug.LogWarning("Unkown button ID: " + buttonName);
        }
    }

	public void OnButtonReleased (VirtualButtonBehaviour vb)
    {
       // Do nothing
    }

	private void animateSaturn() {
		if(doSaturnAnimation) {
			saturnArena.transform.Rotate(new Vector3(0f, 0f, 20*rotationSpeed) * Time.deltaTime);
		}		
	}

	private void Update() {
		animateSaturn();
		animateProgressBar();
	}

	private void animateProgressBar() {
		float curFrame = (float)player.frame;
		float totalFrames = (float)player.frameCount;
		float percent = curFrame/totalFrames;

		Debug.Log("--------------------GetType(curFrame) = " + curFrame.GetType() );
		Debug.Log("--------------------GetType(totalFrames) = " + totalFrames.GetType() );
		Debug.Log("--------------------Current Frame = " + curFrame);
		Debug.Log("--------------------Total Frames = " + totalFrames);
		Debug.Log("--------------------Percent: " + percent);
	}
}
