using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    void OnMouseDown()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
