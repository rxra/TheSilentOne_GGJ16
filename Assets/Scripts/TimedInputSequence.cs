﻿using UnityEngine;
using System.Collections;

public class TimedInputSequence : MonoBehaviour {

    public enum Type
    {
        Any,
        Keyboard,
        Mouse,
        MouseLeft,
        MouseRight
    }
    
    public GameManager manager;
    public Ritual ritual;
    public Type type = Type.Any;
    public float epsilon = 0.2f;
    public float[] steps;
    public GameManager.SuccessType success;
    public float inactivityTimeout = 5;
    
    private bool _started = false;
    private int _step = 0;
    private float _elapsedTimeStep = 0;
    private float _totalTime = 0;
    private float _totalElapsedTime = 0;
    private float _startTime = 0;
    
	// Use this for initialization
	void Start () {
	   _started = false;
       foreach(var step in steps) {
           _totalTime += step;
           _totalTime += epsilon;
       }
       _startTime = Time.time;
	}

    void OnEnable() {
	   _started = false;
       _startTime = Time.time;
       _elapsedTimeStep = 0;
        _totalElapsedTime = 0;
    }
    
	// Update is called once per frame
	void Update () {
        
	   if(!_started && IsInput()) {
           _started = true;
           _elapsedTimeStep = 0;
           _step = 0;
       } else if (_started) {
           _elapsedTimeStep += Time.deltaTime;
           _totalElapsedTime += Time.deltaTime;
           if (IsInput()) {
               if (Mathf.Abs(steps[_step]-_elapsedTimeStep) < epsilon) {
                   //Debug.Log("step " + _step + " OK (" + _elapsedTimeStep + " " + steps[_step] + ")");
                   _step++;
                   if (_step==steps.Length) {
                       _started = false;
                       if (manager!=null) {
                            manager.Success(success);
                           gameObject.SetActive(false);
                       } else
                            ritual.Success(success);
                   } else {
                       _elapsedTimeStep = 0;
                   }
               } else {
                    /*Debug.Log("step " + _step + " FAILED (" + _elapsedTimeStep + " " + steps[_step] + ")");
                    _started = false;
                    if (manager!=null) {
                         manager.Failed(GameManager.FailType.Error);
                        gameObject.SetActive(false);
                    } else
                         ritual.Failed(GameManager.FailType.Error);*/
                }
            }
           
           if (_totalElapsedTime > (_totalTime + epsilon)) {
                /*Debug.Log("TOTAL TIME FAILED (" + _totalElapsedTime + " " + _totalTime + ")");               
                _started = false;
                if (manager!=null) {
                     manager.Failed(GameManager.FailType.TooLong);
                    gameObject.SetActive(false);
                } else
                     ritual.Failed(GameManager.FailType.TooLong);*/
            }
        } if ((Time.time - _startTime) > inactivityTimeout) {
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
    
    private bool IsInput()
    {
        switch(type) {
        case Type.Any:
            return Input.anyKeyDown;
        case Type.Mouse:
            return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
        case Type.MouseLeft:
            return Input.GetMouseButtonDown(0);
        case Type.MouseRight:
            return Input.GetMouseButtonDown(1);
        }
        return false;
    }
}
