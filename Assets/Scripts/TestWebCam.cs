using UnityEngine;
using System.Collections;

public class TestWebCam : MonoBehaviour {

    public Renderer renderer;
    
    private string _deviceName;
    
	// Use this for initialization
	void Start () {
        foreach(var wcam in WebCamTexture.devices) {
            _deviceName = wcam.name;
            WebCamTexture webcam = new WebCamTexture(_deviceName);
            renderer.material.mainTexture = webcam;
            webcam.Play();	
            break;            
        }

	}
	
	// Update is called once per frame
	void Update () {
	}
}
