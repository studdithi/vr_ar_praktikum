using System.Collections;
using UnityEngine;
using Vuforia;

public class VBEngineEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject saturnArena;
	public float rotationSpeed = 1;
	private bool doSaturnAnimation = true;


	public UnityEngine.Video.VideoPlayer player;
	private bool isVideoPlaying = false;
	public GameObject pauseIcon;


    // Start is called before the first frame update
    void Start()
    {
		// Register Virtual Button Events
		VirtualButtonBehaviour[] vbArr = GetComponentsInChildren<VirtualButtonBehaviour> ();
		foreach (VirtualButtonBehaviour vb in vbArr) {
			vb.RegisterEventHandler(this);
		}
    }

	private void Update() {
		videoPlaySOB();
		animateSaturn();
	}

	public void OnButtonPressed (VirtualButtonBehaviour vb)
	{
        string buttonName = vb.VirtualButtonName;
		
        if (buttonName == "videoplay_sob")
        {
			//Changes the state of the video play
			isVideoPlaying = !isVideoPlaying;
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

	//animates the 3D Saturn logo rotating around the y axis
	private void animateSaturn() {
		if(doSaturnAnimation) {
			saturnArena.transform.Rotate(new Vector3(0f, 0f, 20*rotationSpeed) * Time.deltaTime);
		}		
	}

	//starts or pausing the video after the state of isVideoPlaying
	private void videoPlaySOB() {
		if(isVideoPlaying) {
			player.Play();
			pauseIcon.SetActive(false);
		}else if(!isVideoPlaying){
			player.Pause();
			pauseIcon.SetActive(true);
		}
	}

	
}
