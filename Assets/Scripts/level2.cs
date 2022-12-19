using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class level2 : MonoBehaviour
{
    public GameObject[] fruits;
    public int[] fruit_count;
    public float height = 20.0f;
    public GameObject water;
    public float TideSpeed = 2.0f;
    public GameObject[] boards;
    public GameObject b_control;
    


    internal bool drop = false;
    internal bool end = false;

    private string[] ops = {"+", "-"};
    private float timestamp_start = 0.0f;
    private int num_fruit_generate;
    private int num_correct_board;
    private string question;
    private int answer;
    private int[] wrong_answer = new int[2];

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
        StartCoroutine(Tide_initialize(-20f));
        Reset_count();
        timestamp_start = Time.time;
        num_fruit_generate = 15;
        num_correct_board = 2;
        for (int i = 0; i < num_fruit_generate; i++) 
        {
            Random_drop_fruit();
        }
        drop = true;
    }

    public void DisableTiles()
    {
        foreach (GameObject board in boards)
            board.SetActive(false);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        DisableTiles();
        Destroy_fruits();
        StartCoroutine(Tide_change(-14f));
        SetLevel();

        //random select 2 fruit and operator to generate answer
        int[] selectF = {0, 1, 2, 3};
        int[] pickF = new int[2];
        int index = Random.Range(0, selectF.Length);
        pickF[0] = index;
        while (true)
        {
            int i2 = Random.Range(0, selectF.Length);
            if (i2 != index) 
            {
                pickF[1] = i2;
                break;
            }
        }
        string op = ops[Random.Range(0, ops.Length)];
        switch (op)
        {
            case "+":
                answer = fruit_count[pickF[0]] + fruit_count[pickF[1]];
                break;
            case "-":
                answer = fruit_count[pickF[0]] - fruit_count[pickF[1]];
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
        Debug.Log(answer);
        Debug.Log(wrong_answer[1]);
    }

    // Update is called once per frame
    void Update()
    {
        //bpard appears after time
        if (Time.time - timestamp_start > 9.0f)
        {
            foreach (GameObject board in boards)
            {
                board.SetActive(true);
                // b_control.SetActive(true);
            }
        }
        
    }

    IEnumerator Tide_change(float end)
    {
        yield return new WaitForSeconds(10.0f);
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
        float x = water.transform.position.x;
        float z = water.transform.position.z;
        while (water.transform.position.y >= -19.8f)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, new Vector3(x, end, z), 1f * Time.deltaTime);
            yield return null;
        }
    }


    IEnumerator WaitForSeconds(float delay)
    {
        yield return new WaitForSeconds(5);
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