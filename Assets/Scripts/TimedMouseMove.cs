using UnityEngine;
using System.Collections;

public class TimedMouseMove : MonoBehaviour {

    public GameManager manager;
    public float distance = 200;
    public float duration = 5;
    public GameManager.SuccessType success;
    public GameManager.FailType fail;
    public float inactivityTimeout = 5;
    
    private Vector3 _lasPosition;
    private float _length = 0;
    private float _elapsedTime = 0;
    private bool _started = false;
    private float _startTime = 0;
    
	// Use this for initialization
	void Start () {
	   _lasPosition = Input.mousePosition;
       _length = 0;
       _startTime = Time.time;
	}
	
    void OnEnable() {
	   _lasPosition = Input.mousePosition;
	   _started = false;
       _startTime = Time.time;
       _elapsedTime = 0;
       _length = 0;
    }

	// Update is called once per frame
	void Update () {
        if (!_started && Vector3.Distance(_lasPosition, Input.mousePosition)>1) {
            _started = true;
        }
        
        if (_started) {
            _elapsedTime += Time.deltaTime;
    	   _length += Vector3.Distance(_lasPosition, Input.mousePosition);
           _lasPosition = Input.mousePosition;
           
           Debug.Log(_length);
           
           if (_length>=distance) {
               Debug.Log("OK: " + _length + " " + _elapsedTime);
               manager.Success(success);
               gameObject.SetActive(false);
           } else if (_elapsedTime>duration) {
               Debug.Log("FAILED: " + _length + " " + _elapsedTime);
               manager.Failed(fail);
               gameObject.SetActive(false);
           }
       } else if ((Time.time - _startTime) > inactivityTimeout) {
           Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
           _started = false;
           manager.Failed(fail);
           gameObject.SetActive(false);
       }
	}
}
