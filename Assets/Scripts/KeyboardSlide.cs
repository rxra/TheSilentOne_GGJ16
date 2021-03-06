﻿using UnityEngine;
using System.Collections;

public class KeyboardSlide : MonoBehaviour {

    public GameManager manager;
    public Ritual ritual;
    public KeyCode startLetter;
    public KeyCode endLetter;
    public int letterCount = 3;
    public float timeout = 5;
    public GameManager.SuccessType success;

    private float _startTime = 0;
    private string _letters;
    
    void OnEnable() {
       _startTime = Time.time;
       _letters = "";
    }
	
	// Update is called once per frame
	void Update ()
	{
	   if(Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
           _letters += Input.inputString.ToUpper();
           //Debug.Log(Input.inputString);
           if ( _letters[0].ToString()==startLetter.ToString() && 
                _letters[_letters.Length-1].ToString()==endLetter.ToString() && 
                _letters.Length>=letterCount)	
			{
                //Debug.Log("OK: " + _letters);
                if (manager!=null)
				{
                    gameObject.SetActive(false);
                    manager.Success(success);
                }
                else
				{
                    ritual.Success(success);
                }
           }
		}
	   
		if ((Time.time - _startTime) > timeout)
		{
			//Debug.Log("timeout FAILED (" + (Time.time - _startTime) + ") " + _letters);
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
