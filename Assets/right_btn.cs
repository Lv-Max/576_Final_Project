using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class right_btn : MonoBehaviour
{

    public void change_insrtuction()
    {
        //Debug.Log(GameObject.Find("Stage"));


        //foreach (var component in GameObject.Find("Stage").GetComponents<Component>())
        //{
        //    Debug.Log(component);
        //}
        string stage = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text.Split("#")[1];
        if (stage == "4")
        {
            return;
        }
        //else if (stage == "2")
        //{
        //    GameObject.Find("Stage").GetComponent<TextMeshPro>().text = "Stage #1";
        //    GameObject.Find("Instruction").GetComponent<TextMeshPro>().text = "Remember number each  fruit droped from the sky!";
        //}
        else if (stage == "1")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #2";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Stay on tile and find solution to the question on screen!";
        }
        else if (stage == "2")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #3";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Choose the tile with the right answer!";
        }
        else if (stage == "3")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #4";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Do not fall into water!";
        }

    }
}
