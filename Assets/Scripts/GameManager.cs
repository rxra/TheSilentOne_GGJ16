using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum FailType
    {
        Error
        ,TooLong
    }

    public enum SuccessType
    {
        Success1
    }
    
    public string[] triggers;
    private Animator _anim;
    private int _step = 0;
    public AudioClip winSFX;
    public AudioClip gameOverSFX;
    public AudioClip failSFX;
    public Character character;

    public void Success(SuccessType type)
    {
        Debug.Log("Success: " + type);
        _anim.SetTrigger("SequenceFinished");
        _step++;
        if (_step>=triggers.Length) {
            Debug.Log("finished");
            GetComponent<AudioSource>().PlayOneShot(gameOverSFX);
        }
        else if (_step<triggers.Length)
        {
            GetComponent<AudioSource>().PlayOneShot(winSFX);
            StartCoroutine(WaitAndSetTrigger(winSFX.length));
        }
    }
    
    public void Failed(FailType type)
    {
        Debug.Log("Failed: " + type);
        if (_step<triggers.Length)
        {
            switch (type)
            {
                case FailType.Error:
                    GetComponent<AudioSource>().PlayOneShot(failSFX);
                    StartCoroutine(WaitAndSetTrigger(failSFX.length));
                    break;

                case FailType.TooLong:
                    _anim.SetTrigger(triggers[_step]);
                    break;
            }
        }
    }

    public void ForceNextStep()
    {
        Debug.Log("Entract finished");
        _step++;
        if (_step >= triggers.Length)
        {
            Debug.Log("finished");
        }
        else if (_step < triggers.Length)
        {
            _anim.SetTrigger(triggers[_step]);
        }
    }
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
       if (triggers.Length>0) {
           _anim.SetTrigger(triggers[_step]);           
       }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitAndSetTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _anim.SetTrigger(triggers[_step]);
    }
}
