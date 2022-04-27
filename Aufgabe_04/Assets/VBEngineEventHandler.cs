using System.Collections;
using UnityEngine;
using Vuforia;

public class VBEngineEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject engine;

	void Start ()
	{
		// Register Virtual Button Events
		VirtualButtonBehaviour[] vbArr = GetComponentsInChildren<VirtualButtonBehaviour> ();
		foreach (VirtualButtonBehaviour vb in vbArr) {
			vb.RegisterEventHandler (this);
		}
	}



    public void OnButtonPressed (VirtualButtonBehaviour vb)
	{
        string buttonName = vb.VirtualButtonName;
        Debug.Log("OnButtonPressed: " + buttonName);
        if (buttonName == "explosion")
        {
            toggleExplosion(engine.transform);
            exploded = !exploded;
        }
        else if (buttonName == "grow")
        {
            scaleUp();
        }
        else if (buttonName == "shrink")
        {
            scaleDown();
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

    public float scaleFactor = 1.25f;
    
    // Scales up model by scaleFactor
    private void scaleUp(){
        engine.transform.localScale *= scaleFactor;
        Debug.Log("Scale Up");
    }

    // Scales down model by 1/scaleFactor
    private void scaleDown(){
        engine.transform.localScale *= 1/scaleFactor;
        Debug.Log("Scale Down");
    }

    public float explosionForce = 0.05f;

    private bool exploded = false;

    // exploded == true: moves all children of parent by 0.05 * dist(parent, child)
    // exploded == false: moves all children of parent by -0.05 * dist(parent, child)
    private void toggleExplosion(Transform parent)
    {
        Debug.Log("Explosion");
        if(!exploded){
            explosionForce *= -1;
        }
        foreach(Transform child in parent.transform){
            //move child object
            child.position = explosionForce * getDirection(parent.position, child.position);
            toggleExplosion(child);
        }
    }

    private Vector3 getDirection(Vector3 fromV, Vector3 toV)
    {
        Vector3 v = new Vector3(0, 0, 0);
        v = toV - fromV;
        return v.normalized;
    }
}
