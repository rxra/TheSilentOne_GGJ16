using UnityEngine;
using System.Collections;

public class Ritual : MonoBehaviour {

    public GameManager manager;
    public int successCount = 1;
    //public float timeout = 20;
    public MonoBehaviour[] checkers; 
    public AudioSource sound;
    public Material energy;
    
    private float _startTime;
    private int _success = 0;
    private bool _energyStarted = false;
    private float _energyElapsedTime = 0;
	public bool _listeningHasStarted = false;
    
    public void Success(GameManager.SuccessType success)
    {
        if (_success==0)
        {
            //Debug.Log("PLAY sound");
            energy.SetFloat("_Cutoff", 1.0f);
            _energyElapsedTime = 0;
            sound.Play();
            _energyStarted = true;
        }
        
        _success++;
        manager.heart.SetTrigger("success");
        if (_success==successCount) {
            _energyStarted = false;
            energy.SetFloat("_Cutoff", 1.0f);
            //Debug.Log("stop sound");
            sound.Stop();
            //Debug.Log("Ritul suceeded");
            manager.Success(success);
           gameObject.SetActive(false);
        } else {
            checkers[_success-1].enabled = false;
            checkers[_success].enabled = true;
        }
    }

    public void Failed(GameManager.FailType fail)
    {
        //Debug.Log("stop sound");
        sound.Stop();
        _energyStarted = false;
        energy.SetFloat("_Cutoff", 1.0f);
        manager.Failed(fail);
        gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void OnEnable ()
	{
		foreach (var go in checkers)
		{
			go.enabled = false;
		}
		_listeningHasStarted = false;
		manager.currentRitual = GetComponent<Ritual>();
		_energyElapsedTime = 0;
		energy.SetFloat("_CutOff", 1.0f);
		_success = 0;
	}
	
	// Update is called once per frame
	void Update () {
	  // if ((Time.time - _startTime) > timeout && _listeningHasStarted)
   //     {
			//Debug.Log("stop sound");
			//sound.Stop();
			//_energyStarted = false;
			//energy.SetFloat("_CutOff", 1.0f);
   //         Debug.Log("inactivity FAILED (" + (Time.time - _startTime) + ")");
			//manager.Failed(GameManager.FailType.TooLong);
			//gameObject.SetActive(false);
   //    
       if (_energyStarted) {
           _energyElapsedTime += Time.deltaTime;
			float cutoff = 1.0f - Mathf.Lerp(0.0f, 1.0f, _energyElapsedTime / 30.0f);
		   energy.SetFloat("_Cutoff", cutoff);
			if (cutoff <= 0)
				manager.EndGame();
       }
	}

    public void StartListening()
    {
        foreach (var go in checkers)
        {
            go.enabled = false;
        }
		_listeningHasStarted = true;
		_startTime = Time.time;
        checkers[0].enabled = true;
    }
}
