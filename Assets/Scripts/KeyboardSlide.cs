using UnityEngine;
using System.Collections;

public class KeyboardSlide : MonoBehaviour {

    public GameManager manager;
    public KeyCode startLetter;
    public KeyCode endLetter;
    public int letterCount = 3;
    public float timeout = 5;
    public GameManager.SuccessType success;

    private float _startTime = 0;
    private string _letters;
    
    void OnEnable() {
       _startTime = Time.time;
       _letters = "";
    }
	
	// Update is called once per frame
	void Update () {
	   if(Input.anyKeyDown) {
           _letters += Input.inputString.ToUpper();
           Debug.Log(Input.inputString);
           Debug.Log(startLetter.ToString().ToUpper());
           if ( _letters[0].ToString()==startLetter.ToString() && 
                _letters[_letters.Length-1].ToString()==endLetter.ToString() && 
                _letters.Length>letterCount) {
                Debug.Log("OK: " + _letters);
                gameObject.SetActive(false);
                manager.Success(success);
           }
       }

       if ((Time.time - _startTime) > timeout) {
           Debug.Log("timeout FAILED (" + (Time.time - _startTime) + ")");
           manager.Failed(GameManager.FailType.TooLong);
           gameObject.SetActive(false);
       }
	}
}
