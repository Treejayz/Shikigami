using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour {

	public void Yell(string s) {
        AkSoundEngine.PostEvent(s, transform.parent.gameObject);
    }
}
