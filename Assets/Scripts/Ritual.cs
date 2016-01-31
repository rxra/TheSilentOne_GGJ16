using UnityEngine;
using System.Collections;

public class Ritual : MonoBehaviour {

    public GameManager manager;
    public int successCount = 1;
    public float timeout = 20;
    public MonoBehaviour[] checkers; 
    public AudioSource sound;
    public Material energy;
    
    private float _startTime;
    private int _success = 0;
    private bool _energyStarted = false;
    private float _energyElapsedTime = 0;
    
    public void Success(GameManager.SuccessType success)
    {
        if (_success==0)
        {
            Debug.Log("PLAY sound");
            energy.SetFloat("_CutOff", 1.0f);
            _energyElapsedTime = 0;
            sound.Play();
            _energyStarted = true;
        }
        
        _success++;
        manager.hearth.SetTrigger("success");
        if (_success==successCount) {
            _energyStarted = false;
            energy.SetFloat("_CutOff", 1.0f);
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
        _energyStarted = false;
        energy.SetFloat("_CutOff", 1.0f);
        manager.Failed(fail);
        gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void OnEnable () {
	   _startTime = Time.time;
       _energyElapsedTime = 0;
       energy.SetFloat("_CutOff", 1.0f);
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
            _energyStarted = false;
            energy.SetFloat("_CutOff", 1.0f);
           Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
           manager.Failed(GameManager.FailType.TooLong);
           gameObject.SetActive(false);
       }
       
       if (_energyStarted) {
           _energyElapsedTime += Time.deltaTime;
           energy.SetFloat("_CutOff", 1.0f - Mathf.Lerp(0.0f, 1.0f, _energyElapsedTime / 40.0f));
       }
	}
}
