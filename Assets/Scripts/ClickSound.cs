using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource; 

    public void Clicsound()
    {
        audioSource.Play();
    }
}
