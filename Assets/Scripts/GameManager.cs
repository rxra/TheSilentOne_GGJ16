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
    public AudioClip failSFX;

    public void Success(SuccessType type)
    {
        Debug.Log("Success: " + type);
        _step++;
        if (_step>=triggers.Length) {
            Debug.Log("finished");
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(winSFX);
            StartCoroutine(WaitAndSetTrigger(winSFX.length));
        }
    }
    
    public void Failed(FailType type)
    {
        Debug.Log("Failed: " + type);
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
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
       _anim.SetTrigger(triggers[_step]);
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
