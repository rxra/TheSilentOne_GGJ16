using UnityEngine;
using System.Collections;

public class Ritual : MonoBehaviour {

    public GameManager manager;
    public int successCount = 1;
    public float timeout = 20;
    public MonoBehaviour[] checkers; 
    public AudioSource sound;
    
    private float _startTime;
    private int _success = 0;
    
    public void Success(GameManager.SuccessType success)
    {
        if (_success==0)
        {
            Debug.Log("PLAY sound");
            sound.Play();
        }
        
        _success++;
        if (_success==successCount) {
            Debug.Log("stop sound");
            sound.Stop();
            Debug.Log("Ritul suceeded");
            manager.Success(success);
           gameObject.SetActive(false);
        } else {
            checkers[_success-1].enabled = false;
            checkers[_success].enabled = true;
        }
    }

    public void Failed(GameManager.FailType fail)
    {
        Debug.Log("stop sound");
        sound.Stop();
        manager.Failed(fail);
        gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void OnEnable () {
	   _startTime = Time.time;
       _success = 0;
       foreach(var go in checkers) {
           go.enabled = false;
       }
       checkers[0].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	   if ((Time.time - _startTime) > timeout) {
        Debug.Log("stop sound");
           sound.Stop();
           Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
           manager.Failed(GameManager.FailType.TooLong);
           gameObject.SetActive(false);
       }
	}
}
