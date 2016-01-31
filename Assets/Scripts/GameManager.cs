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
        Default,
        Success1,
        Success2,
        Success3,
        Success4,
        Success5
    }
    
    public string[] triggers;
    private Animator _anim;
    private int _step = 0;
    public AudioClip winSFX;
    public AudioClip gameOverSFX;
    public AudioClip failSFX;
    public Character character;

    public void CharacterToc()
    {
        character.SetTrigger("Toc");    
    }
    
    public void CharacterKeyboard()
    {
        character.SetTrigger("SlideLetters");    
    }
    
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
            switch(type) {
                case SuccessType.Success1:
                    character.SetTrigger("Smiley1");
                    break;
                case SuccessType.Success2:
                    character.SetTrigger("Smiley2");
                    break;
                case SuccessType.Success3:
                    character.SetTrigger("Smiley3");
                    break;
                case SuccessType.Success4:
                    character.SetTrigger("Smiley4");
                    break;
                case SuccessType.Success5:
                    character.SetTrigger("Smiley5");
                    break;
                case SuccessType.Default:
                    StartCoroutine(WaitAndSetTrigger(winSFX.length));
                    break;
            }
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

    public void NextStepAfterAnimEnd()
    {
        _anim.SetTrigger(triggers[_step]);
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
