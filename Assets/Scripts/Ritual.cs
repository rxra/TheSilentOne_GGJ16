﻿using UnityEngine;
using System.Collections;

public class Ritual : MonoBehaviour {

    public GameManager manager;
    public int successCount = 1;
    public float timeout = 20;
    
    private float _startTime;
    private int _success = 0;
    
    public void Success(GameManager.SuccessType success)
    {
        _success++;
        if (_success==successCount) {
            Debug.Log("Ritul suceeded");
            manager.Success(success);
           gameObject.SetActive(false);
        }
    }

    public void Failed(GameManager.FailType fail)
    {
        manager.Failed(fail);
        gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void OnEnable () {
	   _startTime = Time.time;
       _success = 0;
	}
	
	// Update is called once per frame
	void Update () {
	   if ((Time.time - _startTime) > timeout) {
           Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
           manager.Failed(GameManager.FailType.TooLong);
           gameObject.SetActive(false);
       }
	}
}