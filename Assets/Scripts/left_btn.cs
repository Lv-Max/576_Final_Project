using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class left_btn : MonoBehaviour
{
    
    public void change_insrtuction()
    {
        string stage = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text.Split("#")[1];

        if(stage == "1")
        {
            return;
        }else if (stage == "2")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #1";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Remember number each  fruit droped from the sky!";
            GameObject.Find("GamePlay").GetComponent<Image>().sprite = Resources.Load<Sprite>("Stage1");
        }
        else if (stage == "3")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #2";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Stay on tile and find solution to the question on screen!";
            GameObject.Find("GamePlay").GetComponent<Image>().sprite = Resources.Load<Sprite>("Stage2");
        }
        else if (stage == "4")
        {
            GameObject.Find("Stage").GetComponent<TextMeshProUGUI>().text = "Stage #3";
            GameObject.Find("Instruction").GetComponent<TextMeshProUGUI>().text = "Choose the tile with the right answer!";
            GameObject.Find("GamePlay").GetComponent<Image>().sprite = Resources.Load<Sprite>("Stage3");
        }

    }
}
