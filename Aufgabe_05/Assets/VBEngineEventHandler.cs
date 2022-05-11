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
	private Vector3 initialCylinderWidth;
	private bool isVideoPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
		// Register Virtual Button Events
		VirtualButtonBehaviour[] vbArr = GetComponentsInChildren<VirtualButtonBehaviour> ();
		foreach (VirtualButtonBehaviour vb in vbArr) {
			vb.RegisterEventHandler(this);
		}

		initialCylinderWidth = ProgressCylinder.transform.localScale;
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

		var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;
	}


	private void animateProgressBar() {
		float curFrame = (float)player.frame;
		float totalFrames = (float)player.frameCount;
		float percent = curFrame/totalFrames;
		float yScale = initialCylinderWidth.y * percent;
	
		if(percent <=0){
			ProgressCylinder.transform.localScale = new Vector3(0f, 0f, 0f);
		} else {
			ProgressCylinder.transform.localScale = new Vector3(initialCylinderWidth.x, yScale, initialCylinderWidth.z);
		}
	}
}
