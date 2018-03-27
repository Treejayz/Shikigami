using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendRTPC : MonoBehaviour {

    public string RTPC;
    public Slider mainSlider;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey(RTPC))
        {
            mainSlider.value = PlayerPrefs.GetFloat(RTPC);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ValueChanged () {
        PlayerPrefs.SetFloat(RTPC, mainSlider.value);
        AkSoundEngine.SetRTPCValue(RTPC, mainSlider.value, null);
    }
}
