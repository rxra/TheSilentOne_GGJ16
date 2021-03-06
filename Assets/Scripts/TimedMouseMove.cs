﻿using UnityEngine;
using System.Collections;

public class TimedMouseMove : MonoBehaviour {

    public GameManager manager;
    public Ritual ritual;
    public float distance = 200;
    public float duration = 5;
    public GameManager.SuccessType success;
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
	
    void OnEnable()
    { 
	   _lasPosition = Input.mousePosition;
	   _started = false;
       _startTime = Time.time;
       _elapsedTime = 0;
       _length = 0;
    }

	// Update is called once per frame
	void Update()
	{
		if (!_started && Vector3.Distance(_lasPosition, Input.mousePosition) > 1)
		{
			_started = true;
		}

		if (_started)
		{
			_elapsedTime += Time.deltaTime;
			_length += Vector3.Distance(_lasPosition, Input.mousePosition);
			_lasPosition = Input.mousePosition;

			//Debug.Log(_length);

			if (_length >= distance)
			{
				//Debug.Log("OK: " + _length + " " + _elapsedTime);
				if (manager != null)
				{
					gameObject.SetActive(false);
					manager.Success(success);
				}
				else
					ritual.Success(success);
			}/* else if (_elapsedTime>duration) {
               Debug.Log("FAILED: " + _length + " " + _elapsedTime);
                if (manager!=null) {
                    gameObject.SetActive(false);
                    manager.Failed(GameManager.FailType.Error);
                } else
                    ritual.Failed(GameManager.FailType.Error);
           }*/
		}
		else if ((Time.time - _startTime) > inactivityTimeout)
		{
			//Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
			_started = false;
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
}
