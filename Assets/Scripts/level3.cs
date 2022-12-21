using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class level3 : MonoBehaviour
{
    public GameObject[] fruits;
    public GameObject[] small_fruits;
    public int[] fruit_count;
    public float height = 20.0f;
    public GameObject water;
    public float TideSpeed = 2.0f;
    public GameObject[] boards;
    public GameObject b_control;
    public GameObject QuestionSymbol;
    public GameObject QuestionCam;
    public GameObject[] boardAns;
    
    public TextMeshProUGUI Instruct;

    private string[] ops = {"+", "-", "*"};
    private float timestamp_start = 0.0f;
    private int num_fruit_generate;
    private int num_correct_board;
    private string question;
    private int answer;
    private int[] wrong_answer = new int[2];
    private int[] wrong_board_idx;

    void Random_drop_fruit()
    {
        // Define the range of the random position
        Vector2 pos = Random.insideUnitCircle;

        // Generate a random x, y, and z position within the defined range
        float x = pos.x * 20.0f;
        float y = height;
        float z = pos.y * 20.0f;

        // Create a Vector3 to store the generated position
        Vector3 randomPosition = new Vector3(x, y, z);

        // Define the bounds of the area you want the object to stay within
        Vector3 minBounds = new Vector3(-20, 20, -20);
        Vector3 maxBounds = new Vector3(20, 50, 20);

        // Use the Clamp function to constrain the object's position to within the defined bounds
        Vector3 clampedPosition = new Vector3(
        Mathf.Clamp(randomPosition.x, minBounds.x, maxBounds.x),
        Mathf.Clamp(randomPosition.y, minBounds.y, maxBounds.y),
        Mathf.Clamp(randomPosition.z, minBounds.z, maxBounds.z)
        );

        // Update the fruit's position with the clamped position
        int fruits_num = fruits.Length;
        int cur_fruit = Random.Range(0, fruits_num);
        GameObject fruit = Instantiate(fruits[cur_fruit]);
        fruit.name = fruits[cur_fruit].name;
        fruit.transform.position = clampedPosition; 

        //random rotate fruit
        float angle = Random.Range(0, 360);
        fruits[cur_fruit].transform.Rotate(0, angle, 0);
        fruit_count[cur_fruit] ++;
    }

    //Destroy all fruits
    void Destroy_fruits()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            if (o.tag == "fruit")
                Destroy(o);
    }

    //Reset fruit count to 0
    void Reset_count()
    {
        for(int i = 0; i < fruit_count.Length; i++)
            fruit_count[i] = 0;
    }

    public void SetLevel()
    {
        Reset_count();
        timestamp_start = Time.time;
        num_fruit_generate = 12;
        num_correct_board = 2;
        wrong_board_idx = new int[6-num_correct_board];
        for (int i = 0; i < num_fruit_generate; i++) 
        {
            Random_drop_fruit();
        }
    }

    //disable all tiles
    public void DisableTiles()
    {
        foreach (GameObject board in boards)
            board.SetActive(false);
    }

    //disable all wrong tiles
    public void DisableWrongTiles()
    {
        foreach (int idx in wrong_board_idx)
            boards[idx].SetActive(false);
    }

    public void EnableAllTiles()
    {
        foreach (GameObject board in boards)
        {
            board.SetActive(true);
            // b_control.SetActive(true);
        }  
    }

    public void SetTileNum(bool b)
    {
        foreach (GameObject num in boardAns)
            num.SetActive(b);
    }

    public void SetQuestion()
    {
        List<int> boardidxs = new List<int>() {0, 1, 2, 3, 4, 5};
        //generate correct answer board
        for (int i = 0; i < num_correct_board; i++) 
        {
            int index = Random.Range(0, boardidxs.Count);
            //do correct board assign
            boardAns[boardidxs[index]].GetComponent<TextMeshPro>().text = answer.ToString();
            boardidxs.RemoveAt(index);
        }
        //generate wrong answer board
        for (int i = 0; i < 6 - num_correct_board; i++) 
        {
            int index = Random.Range(0, boardidxs.Count);
            wrong_board_idx[i] = boardidxs[index];
            Debug.Log(wrong_board_idx[i]);
            boardAns[boardidxs[index]].GetComponent<TextMeshPro>().text = wrong_answer[Random.Range(0, 2)].ToString();
            boardidxs.RemoveAt(index);
        }
    }


    void OnEnable()
    {
        StartCoroutine(Tide_change(-14f));
        StartCoroutine(Tide_initialize(-20f));
        StartCoroutine(TextInstruction());
        StartCoroutine(GameInstruction());
        SetLevel();
        

        //random select 2 fruit and operator to generate answer
        int[] selectF = {0, 1, 2, 3, 4, 5};
        int[] pickF = new int[2];
        int index = Random.Range(0, selectF.Length);
        pickF[0] = index;
        GameObject fruit = Instantiate(small_fruits[index]);
        fruit.name = small_fruits[index].name;
        fruit.transform.position = new Vector3(45, -10, 6);
        while (true)
        {
            int i2 = Random.Range(0, selectF.Length);
            if (i2 != index) 
            {
                pickF[1] = i2;
                GameObject fruit2 = Instantiate(small_fruits[i2]);
                fruit2.name = small_fruits[i2].name;
                fruit2.transform.position = new Vector3(45, -10, 2);
                break;
            }
        }
        string op = ops[Random.Range(0, ops.Length)];
        switch (op)
        {
            case "+":
                answer = fruit_count[pickF[0]] + fruit_count[pickF[1]];
                QuestionSymbol.GetComponent<TextMeshPro>().text = "+";
                break;
            case "-":
                answer = fruit_count[pickF[0]] - fruit_count[pickF[1]];
                QuestionSymbol.GetComponent<TextMeshPro>().text = "-";
                break;
            case "*":
                answer = fruit_count[pickF[0]] - fruit_count[pickF[1]];
                QuestionSymbol.GetComponent<TextMeshPro>().text = "X";
                break;
            default:
                Debug.Log("unknown operator");
                break;
        }
        for (int i = 0; i < wrong_answer.Length; i++)
        {
            while (true)
            {
                int temp = Random.Range(answer - 5, answer + 10);
                if (temp != answer)
                {
                    wrong_answer[i] = Random.Range(answer - 5, answer + 10);
                    break;
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    { 

    }

    IEnumerator Tide_change(float end)
    {
        yield return new WaitForSeconds(25);
        float x = water.transform.position.x;
        float z = water.transform.position.z;
        while (water.transform.position.y <= -14.1f)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, new Vector3(x, end, z), 1f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Tide_initialize(float end)
    {
        yield return new WaitForSeconds(42);
        float x = water.transform.position.x;
        float z = water.transform.position.z;
        while (water.transform.position.y >= -19.8f)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, new Vector3(x, end, z), 1f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator TextInstruction()
    {
        Instruct.text = "Count and Memorize fruit sum!";
        yield return new WaitForSeconds(17);
        Instruct.text = "Stand on the board!";
        yield return new WaitForSeconds(11);
        QuestionCam.transform.Rotate(90, 0, 0);
        SetTileNum(true);
        Instruct.text = "Pick the correct answer!";
        yield return new WaitForSeconds(8);
        Instruct.text = "Correct!";
        yield return new WaitForSeconds(7);
        
        QuestionSymbol.GetComponent<TextMeshPro>().text = "";
        QuestionCam.transform.Rotate(-90, 0, 0);
        Instruct.text = "Wait for next round...";
    }

    IEnumerator GameInstruction()
    {
        yield return new WaitForSeconds(17);
        EnableAllTiles();
        yield return new WaitForSeconds(11);
        SetQuestion();
        yield return new WaitForSeconds(8);
        DisableWrongTiles();
        yield return new WaitForSeconds(8);
        SetTileNum(false);
        DisableTiles();
        Destroy_fruits();
    }
}



		// fruits[0].transform.RotateAround (Vector3.up, Time.deltaTime);
		// fruits[1].transform.RotateAround (Vector3.up, -Time.deltaTime);
		// fruits[2].transform.RotateAround (Vector3.up, Time.deltaTime);
		// fruits[3].transform.RotateAround (Vector3.up, -Time.deltaTime);
		// fruits[4].transform.RotateAround (Vector3.up, Time.deltaTime);
		// fruits[5].transform.RotateAround (Vector3.up, -Time.deltaTime);
		// fruits[6].transform.RotateAround (Vector3.up, Time.deltaTime);
		// fruits[7].transform.RotateAround (Vector3.up, -Time.deltaTime);
		// fruits[8].transform.RotateAround (Vector3.up, Time.deltaTime);
		// fruits[9].transform.RotateAround (Vector3.up, -Time.deltaTime);


        // Destroy_fruits();
        // StartCoroutine(Tide_change(-14f));
        // SetLevel();

        // //random select 2 fruit and operator to generate answer
        // int[] selectF = {0, 1, 2};
        // int[] pickF = new int[2];
        // int index = Random.Range(0, selectF.Length);
        // pickF[0] = index;
        // while (true)
        // {
        //     int i2 = Random.Range(0, selectF.Length);
        //     if (i2 != index) 
        //     {
        //         pickF[1] = i2;
        //         break;
        //     }
        // }
        // string op = ops[Random.Range(0, ops.Length)];
        // switch (op)
        // {
        //     case "+":
        //         answer = fruit_count[pickF[0]] + fruit_count[pickF[1]];
        //         break;
        //     case "-":
        //         answer = fruit_count[pickF[0]] - fruit_count[pickF[1]];
        //         break;
        //     default:
        //         Debug.Log("unknown operator");
        //         break;
        // }
        // for (int i = 0; i < wrong_answer.Length; i++)
        // {
        //     while (true)
        //     {
        //         int temp = Random.Range(answer - 5, answer + 10);
        //         if (temp != answer)
        //         {
        //             wrong_answer[i] = Random.Range(answer - 5, answer + 10);
        //             break;
        //         }
        //     }
        // }
        // Debug.Log(answer);
        // Debug.Log(wrong_answer[0]);