using UnityEngine;
using System.Collections;

public class TestWebCam : MonoBehaviour {

    public float checkTimer = 1;
    public Color32 darkTolerance = new Color32(40,5,5,0);
    public GameManager.SuccessType success;
    public float inactivityTimeout = 5;
    public bool hidden = true;
    public Color32 averageDelta = new Color32(10,10,10,0);
    public int averageCount = 3;
    public GameObject meshRenderer;

    private Color32[] _data;
    private float _elapsedTime = 0;
    private bool _initialized = false;
    private bool _hidden = false;
    private float _startTime = 0;
    private Color32 _lastAverage;
    private bool _firstAverage = false;
    private int _average = 0;

    public GameManager manager;
    public Ritual ritual;

    // Use this for initialization
    void Start () {
        _elapsedTime = 0;
    }
	
    void OnEnable()
    {
        _elapsedTime = 0;
        _firstAverage = false;
       _startTime = Time.time;
        meshRenderer.SetActive(true);
    }

	void Update () {
        if (!_initialized && WebCamManager.Texture() != null && WebCamManager.Texture().didUpdateThisFrame) {
            _data = new Color32[WebCamManager.Texture().width * WebCamManager.Texture().height];
            _initialized = true;
        } else {
            _elapsedTime += Time.deltaTime;
        
            if (WebCamManager.Texture() != null && WebCamManager.Texture().didUpdateThisFrame && _elapsedTime>checkTimer) {
                _elapsedTime = 0;
                WebCamManager.Texture().GetPixels32(_data);
                if (hidden) {
                    CheckHidden();                    
                } else {
                    CheckCameraMove();
                }
            }
        }
		if ((Time.time - _startTime) > inactivityTimeout)
		{
			if (WebCamManager.Texture() == null)
			{
				//Debug.Log("no camera, win instead of loosing");
				if (manager != null)
				{
					gameObject.SetActive(false);
					manager.Success(success);
				}
				else
					ritual.Success(success);

				return;
			}
			//Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
			if (ritual == null)
			{
				manager.Failed(GameManager.FailType.TooLong);
				gameObject.SetActive(false);
			}
			else
			{
				ritual.Failed(GameManager.FailType.TooLong);
			}
		}
	}
    
    private void CheckCameraMove()
	{
		Color32 averageColor = getAverage();
		if (_firstAverage)
		{
			int dr = Mathf.Abs(_lastAverage.r - averageColor.r);
			int dg = Mathf.Abs(_lastAverage.g - averageColor.g);
			int db = Mathf.Abs(_lastAverage.b - averageColor.b);
			//Debug.Log(averageColor);
			//Debug.Log("delta " + dr + " " + dg + " " + db);

			_lastAverage = averageColor;

			if (dr > averageDelta.r && dg > averageDelta.g && db > averageDelta.b)
				_average++;
			if (_average == averageCount )
			{
				//Debug.Log("OK: CameraMove");
				if (manager != null)
				{
					gameObject.SetActive(false);
					manager.Success(success);
				}
				else
					ritual.Success(success);
			}
		}
		else
		{
			_firstAverage = true;
			_lastAverage = averageColor;
		}
    }
    
    private void CheckHidden()
    {
        if (!_hidden && isWebcamHidden()) {
            _hidden = true;
            //Debug.Log("OK: Hidden");
            if (manager!=null) {
                gameObject.SetActive(false);
                manager.Success(success);
            }
            else
                ritual.Success(success);
        } else if (_hidden && !isWebcamHidden()) {
            _hidden = false;
            //Debug.Log("ok");
        }
    }
    
    private bool isWebcamHidden()
    {
       Color32 averageColor = getAverage();
         
       return (averageColor.r < darkTolerance.r && averageColor.g < darkTolerance.g && averageColor.b < darkTolerance.b) ? true : false;
    }
    
    private Color32 getAverage()
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
            
        return averageColor;
    }
}
