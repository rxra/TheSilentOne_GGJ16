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
    public AudioClip gameOverSFX;
    public Character character;
    public Animator heart;
    public Ritual currentRitual;

    public void CharacterToc()
    {
        character.SetTrigger("Toc");    
    }
    
    public void CharacterKeyboard()
    {
        character.SetTrigger("SlideLetters");    
    }

    public void CharacterSlideLeft()
    {
        character.SetTrigger("SlideLeft");
    }
    public void CharacterSlideRight()
    {
        character.SetTrigger("SlideRight");
    }
    public void MistOff()
    {
        character.SetTrigger("MistOff");
    }
    public void MoveMouse()
    {
        character.SetTrigger("MoveMouse");
    }
    public void MoveCam()
    {
        character.SetTrigger("MoveCam");
    }
    public void HideCam()
    {
        character.SetTrigger("HideCam");
    }

    public void StartRitual()
    {
        currentRitual.StartListening();
    }

    public void Success(SuccessType type)
    {
        //Debug.Log("Success: " + type);
        _anim.SetTrigger("SequenceFinished");
        heart.SetTrigger("success");
        _step++;
        if (_step>=triggers.Length) {
            //Debug.Log("finished");
            GetComponent<AudioSource>().PlayOneShot(gameOverSFX);
        }
        else if (_step<triggers.Length)
        {
            //GetComponent<AudioSource>().PlayOneShot(winSFX);
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
                    _anim.ResetTrigger("SequenceFinished");
                    _anim.SetTrigger(triggers[_step]);
                    break;
            }
        }
    }
    
    public void Failed(FailType type)
    {
        //Debug.Log("Failed: " + type);
        if (_step<triggers.Length)
        {
            switch (type)
            {
                case FailType.Error:
                    //Debug.Log("Player has made an error");
                    //_anim.SetTrigger(triggers[_step]);
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
        //Debug.Log("Force Next Step");
        _step++;
        if (_step >= triggers.Length)
        {
            //Debug.Log("finished");
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
	
    IEnumerator WaitAndSetTrigger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _anim.SetTrigger(triggers[_step]);
    }
}
