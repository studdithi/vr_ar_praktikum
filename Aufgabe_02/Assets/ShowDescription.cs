using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDescription : MonoBehaviour
{
    public TextMesh textMesh;
    public string buildingName;

    public void ShowName(){
        textMesh.text = buildingName;
        Debug.Log("ShowName");
    }
    public void HideName(){
        textMesh.text = "";
        Debug.Log("Hide Name");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
