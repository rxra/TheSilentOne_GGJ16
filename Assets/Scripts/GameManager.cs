using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum FailType
    {
        Fail1    
    }

    public enum SuccessType
    {
        Success1    
    }
    
    public string[] triggers;
    private Animator _anim;
    private int _step = 0;
    
    public void Success(SuccessType type)
    {
        Debug.Log("Success: " + type);
        _step++;
        if (_step>=triggers.Length) {
            Debug.Log("finished");
        } else {
           _anim.SetTrigger(triggers[_step]);            
        }
    }
    
    public void Failed(FailType type)
    {
        Debug.Log("Failed: " + type);
        _anim.SetTrigger(triggers[_step]);            
    }
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
       _anim.SetTrigger(triggers[_step]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
