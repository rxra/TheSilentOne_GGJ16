using UnityEngine;
using System.Collections;

public class TestMicrophone : MonoBehaviour {

    public AudioSource audio;
    
    private string _deviceName;
    private AudioClip _clip;
    
	// Use this for initialization
	void Start () {
        foreach(var dmic in  Microphone.devices) {
            _deviceName = dmic;
            _clip = Microphone.Start(_deviceName, true, 10, 44100);
            break;            
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (_clip!=null && Input.anyKey) {
            Microphone.End(_deviceName);
            audio.clip = _clip;
            audio.Play();
        }
	}
}
