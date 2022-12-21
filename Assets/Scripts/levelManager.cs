using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.AI;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public bool is_die = false;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject losePop;
    public GameObject winPop;
    public GameObject player;
    public GameObject PauseMenu;

    public AudioSource bgm;
    public AudioClip whistle;
    public AudioClip tick;

    public float score = 0.0f;
    public int Num_remain = 7;

    public TextMeshProUGUI Instruct;
    public TextMeshProUGUI remain;
    public TextMeshProUGUI point;


    private float timeStart = 0.0f;
    private float timePass = 0.0f;
    private float SoundTime = 0.0f;
    private bool has_whistled = false;
    // private float roundTimer = 0.0f;
    //start level
    void startLevel(GameObject lv)
    {

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        lv.SetActive(true);
    }

    public void Pause()
    {
        player.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time. timeScale = 0;
    }

    public void Resume()
    {
        player.GetComponent<UnityEngine.InputSystem.PlayerInput>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time. timeScale = 1;
        PauseMenu.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        // foreach(var component in player.GetComponents<Component>())
        // {
        //     Debug.Log(component);
        // }
        Resume();
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
        StartCoroutine(Game());
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            PauseMenu.SetActive(true);
        }

        timePass = Time.time - timeStart;
        if (!is_die | Num_remain == 1)
        {
            score = Mathf.Floor((Mathf.Max(timePass - 6, 0))*35);
            remain.text = Mathf.Max(Num_remain, 0).ToString()+ "/7";
            point.text = score.ToString() + "pts";
        }

        if (timePass <= 6)
        {
            Instruct.text = Mathf.Floor((6-timePass)).ToString() + "!";
            //play tick sound
            if (Time.time - SoundTime > 1.0f)
            {
                SoundTime = Time.time;
                AudioSource.PlayClipAtPoint(tick, player.transform.position, 1);
            }
        }
        else if (!has_whistled)
        {
            has_whistled = true;
            AudioSource.PlayClipAtPoint(whistle, player.transform.position, 1);
        }
        //check if player is died
        if (is_die) 
            StartCoroutine(lose());
        else if (Num_remain == 1)
            StartCoroutine(win());


    }

    IEnumerator Game()
    {
        yield return new WaitForSeconds(6);
        for (int i = 0; i < 2; i++)
        {
            startLevel(level1);
            yield return new WaitForSeconds(50);
        }
        startLevel(level2);
        yield return new WaitForSeconds(50);
        while (!is_die)
        {
            startLevel(level3);
            yield return new WaitForSeconds(50);
        }
    }

    IEnumerator win() 
    {
        Instruct.gameObject.SetActive(false);
        winPop.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString() + "pts";
        winPop.SetActive(true);
        yield return new WaitForSeconds(1f);
        Pause();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator lose() 
    {
        Instruct.gameObject.SetActive(false);
        losePop.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString() + "pts";
        losePop.SetActive(true);
        yield return new WaitForSeconds(1f);
        Pause();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
