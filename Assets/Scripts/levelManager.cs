using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public bool is_die = false;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;


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

        //check if player is died
    }

    IEnumerator Game()
    {
        for (int i = 0; i < 2; i++)
        {
            startLevel(level1);
            yield return new WaitForSeconds(20);
        }
        for (int i = 0; i < 2; i++)
        {
            startLevel(level2);
            yield return new WaitForSeconds(20);
        }
        while (!is_die)
        {
            startLevel(level3);
            yield return new WaitForSeconds(20);
        }
    }
}
