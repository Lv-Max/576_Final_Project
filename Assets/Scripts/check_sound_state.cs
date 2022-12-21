using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class check_sound_state : MonoBehaviour
{
    
    void Start()
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
