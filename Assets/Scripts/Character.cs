using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public CameraMasking masking;
    public GameObject leftFinger;
    public GameObject rightFinger;
    public AudioClip[] drawingSounds;
    
    private Animator _anim;
    private int soundIndex = 0;
    
    public void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);    
    }

    public void StartDrawingLeft()
    {
        masking.big = false;
        masking.StartMask(leftFinger);
        GetComponent<AudioSource>().PlayOneShot(drawingSounds[soundIndex]);
    }
    
    public void StartDrawingMegaLeft()
    {
        masking.big = true;
        masking.StartMask(leftFinger);
        GetComponent<AudioSource>().PlayOneShot(drawingSounds[soundIndex]);
    }
    
    public void StartDrawingRight()
    {
        masking.big = false;
        masking.StartMask(rightFinger);
        GetComponent<AudioSource>().PlayOneShot(drawingSounds[soundIndex]);
    }
    
    public void StopDrawing()
    {
        masking.StopMask();
        GetComponent<AudioSource>().Stop();
        if (soundIndex >= drawingSounds.Length - 1)
            soundIndex = 0;
        else
            soundIndex++;
    }
    
	// Use this for initialization
	void Start () {
	   _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
