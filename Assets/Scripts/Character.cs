using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public CameraMasking masking;
    public GameObject leftFinger;
    public GameObject rightFinger;
    
    private Animator _anim;
    
    public void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);    
    }

    public void StartDrawingLeft()
    {
        masking.StartMask(leftFinger);    
    }
    
    public void StartDrawingRight()
    {
        masking.StartMask(rightFinger);    
    }
    
    public void StopDrawing()
    {
        masking.StopMask();    
    }
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
