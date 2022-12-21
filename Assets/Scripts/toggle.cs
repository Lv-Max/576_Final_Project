using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggle : MonoBehaviour
{
    
    public void change_toggle_icon()
    {
        if (AudioListener.volume == 0)
        {
            GameObject.Find("Checkmark").GetComponent<Image>().sprite = Resources.Load<Sprite>("mute");
        }
        else
        {
            GameObject.Find("Checkmark").GetComponent<Image>().sprite = Resources.Load<Sprite>("unmute");
        }
    }
        
    
}

