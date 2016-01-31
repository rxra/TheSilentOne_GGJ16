using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public CameraMasking masking;
    public GameObject leftFinger;
    public GameObject rightFinger;
    public AudioClip drawingSound;
    
    private Animator _anim;
    
    public void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);    
    }

    public void StartDrawingLeft()
    {
        masking.StartMask(leftFinger);
        GetComponent<AudioSource>().PlayOneShot(drawingSound);
    }
    
    public void StartDrawingRight()
    {
        masking.StartMask(rightFinger);
        GetComponent<AudioSource>().PlayOneShot(drawingSound);
    }
    
    public void StopDrawing()
    {
        masking.StopMask();
        GetComponent<AudioSource>().Stop();
    }
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
