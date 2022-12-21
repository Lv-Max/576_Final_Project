using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelManager : MonoBehaviour
{
    public bool is_die = false;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public int Num_remain = 7;

    public TextMeshProUGUI Instruct;
    public TextMeshProUGUI remain;
    public TextMeshProUGUI point;

    private float timeStart = 0.0f;
    private float timePass = 0.0f;
    //start level
    void startLevel(GameObject lv)
    {

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        lv.SetActive(true);
    }





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Game());
    }

    // Update is called once per frame
    void Update()
    {
        timePass = Time.time - timeStart;
        remain.text = Num_remain.ToString()+ "/7";
        point.text = Mathf.Floor((Mathf.Max(timePass - 6, 0))*50).ToString() + "pts";
        if (timePass <= 6)
        {
            Instruct.text = Mathf.Floor((6-timePass)).ToString() + "!";
        }
        //check if player is died

    }

    IEnumerator Game()
    {
        yield return new WaitForSeconds(6);
        for (int i = 0; i < 2; i++)
        {
            startLevel(level1);
            yield return new WaitForSeconds(60);
        }
        startLevel(level2);
        yield return new WaitForSeconds(60);
        while (!is_die)
        {
            startLevel(level3);
            yield return new WaitForSeconds(60);
        }
    }
}
