using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighPassInMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AkSoundEngine.SetRTPCValue("MenuUp", 0.0f, null);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
