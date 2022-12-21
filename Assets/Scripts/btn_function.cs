using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btn_function : MonoBehaviour
{
    public void to_tutorial_page()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void to_gameplay()
    {
        SceneManager.LoadScene("Test");
    }
    public void exit()
    {
        Application.Quit();
    }
    public void return_to_main()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void mute()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            GameObject.Find("Checkmark").GetComponent<Image>().sprite = Resources.Load<Sprite>("unmute");
        }
        else
        {
            AudioListener.volume = 0;
            GameObject.Find("Checkmark").GetComponent<Image>().sprite = Resources.Load<Sprite>("mute");
        }
    }
    public void resume_game()
    {
        Time.timeScale = 1;
    }
}
