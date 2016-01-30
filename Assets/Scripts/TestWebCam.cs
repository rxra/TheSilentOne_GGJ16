using UnityEngine;
using System.Collections;

public class TestWebCam : MonoBehaviour {

    public Renderer rendererForCamera;
    public float checkTimer = 1;
    public Color32 darkTolerance = new Color32(40,5,5,0);
    public GameManager.SuccessType success;

    private string _deviceName;
    private Color32[] _data;
    private WebCamTexture _webcam;
    private float _elapsedTime = 0;
    private bool _initialized = false;
    private bool _hidden = false;

    public GameManager manager;

    // Use this for initialization
    void Start () {
        foreach(var wcam in WebCamTexture.devices) {
            _deviceName = wcam.name;
            _webcam = new WebCamTexture(_deviceName, 128, 72);
            rendererForCamera.material.mainTexture = _webcam;
            _webcam.Play();
            _elapsedTime = 0;
            break;            
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!_initialized && _webcam.didUpdateThisFrame) {
            _data = new Color32[_webcam.width * _webcam.height];
            _initialized = true;
        } else {
            _elapsedTime += Time.deltaTime;
        
            if (_webcam.didUpdateThisFrame && _elapsedTime>checkTimer) {
                _elapsedTime = 0;
                _webcam.GetPixels32(_data);
                if (!_hidden && isWebcamHidden()) {
                    _hidden = true;
                    Debug.Log("OK: Hidden");
                    gameObject.SetActive(false);
                    manager.Success(success);
                } else if (_hidden && !isWebcamHidden()) {
                    _hidden = false;
                    Debug.Log("ok");
                }
            }
        }
    }
    
    private bool isWebcamHidden()
    {
        long sumR = 0;
        long sumG = 0;
        long sumB = 0;
        foreach(var c in _data) {
            sumR += c.r;
            sumG += c.g;
            sumB += c.b;
        }
        
        Color32 averageColor = new Color32(
            (byte)(sumR / _data.Length),
            (byte)(sumG / _data.Length),
            (byte)(sumB / _data.Length),
            0);
       
       //Debug.Log(averageColor);
         
       return (averageColor.r < darkTolerance.r && averageColor.g < darkTolerance.g && averageColor.b < darkTolerance.b) ? true : false;
    }
}
