using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverSound : MonoBehaviour, IPointerEnterHandler {

    public void OnPointerEnter( PointerEventData ped) {
        AkSoundEngine.PostEvent("Hover", this.gameObject);
    }
}
