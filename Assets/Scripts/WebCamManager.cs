using UnityEngine;
using System.Collections;

public class WebCamManager : MonoBehaviour {

    public GameObject[] toActivate;
    public Renderer rendererForCamera;
    private string _deviceName;
    private static WebCamTexture _webcam;
    private bool _initialized = false;

    public static WebCamTexture Texture()
    {
        return _webcam;    
    }
    
    void Awake()
    {
        foreach(var go in toActivate)
        {
            go.SetActive(false);
        }
    }
    
	// Use this for initialization
	void Start()
    {
        foreach (var wcam in WebCamTexture.devices)
        {
            _deviceName = wcam.name;
            _webcam = new WebCamTexture(_deviceName, 128, 72);
            rendererForCamera.material.mainTexture = _webcam;
            _webcam.Play();
            break;
        }
		if (WebCamTexture.devices.Length == 0 || 
			(WebCamTexture.devices.Length== 1 && WebCamTexture.devices[0].name.Contains("Virtual")))
		{
			Debug.Log("no webcam");
			_webcam = null;
		}
	}
	
    void Destroy()
    {
        _webcam.Stop();
        _webcam = null;    
    }
    
    void Update()
    {
        if ((!_initialized && WebCamManager.Texture() == null)
			|| !_initialized && WebCamManager.Texture().didUpdateThisFrame) {
            _initialized = true;
            foreach(var go in toActivate)
            {
                go.SetActive(true);
            }
        }
    }
    
}
