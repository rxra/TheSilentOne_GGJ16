using UnityEngine;
using System.Collections;

public class WebCamManager : MonoBehaviour {

    public Renderer rendererForCamera;
    private string _deviceName;
    private static WebCamTexture _webcam;

    public static WebCamTexture Texture()
    {
        return _webcam;    
    }
    
	// Use this for initialization
	void Start () {
        foreach (var wcam in WebCamTexture.devices)
        {
            _deviceName = wcam.name;
            _webcam = new WebCamTexture(_deviceName, 128, 72);
            rendererForCamera.material.mainTexture = _webcam;
            _webcam.Play();
            break;
        }
	}
	
    void Destroy()
    {
        _webcam.Stop();
        _webcam = null;    
    }
    
}
