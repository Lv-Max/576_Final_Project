using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{
    public GameObject[] boardsL;
    public GameObject[] boardsR;

    private float y;
    // Start is called before the first frame update
    void Start()
    {
        y = boardsL[0].transform.position.y;
        StartCoroutine(boardmove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator boardmove()
    {
        while (true)
        {
            foreach (GameObject board in boardsL)
            {
                float x = board.transform.position.x;
                float z = board.transform.position.z;
                board.transform.position = Vector3.Lerp(board.transform.position, new Vector3(x, y, z+15.0f), 1f * Time.deltaTime);
            }
            foreach (GameObject board in boardsR)
            {
                float x = board.transform.position.x;
                float z = board.transform.position.z;
                board.transform.position = Vector3.Lerp(board.transform.position, new Vector3(x, y, z-15.0f), 1f * Time.deltaTime);
            }
            yield return null;
            foreach (GameObject board in boardsL)
            {
                float x = board.transform.position.x;
                float z = board.transform.position.z;
                board.transform.position = Vector3.Lerp(board.transform.position, new Vector3(x, y, z-15.0f), 1f * Time.deltaTime);
            }
            foreach (GameObject board in boardsR)
            {
                float x = board.transform.position.x;
                float z = board.transform.position.z;
                board.transform.position = Vector3.Lerp(board.transform.position, new Vector3(x, y, z+15.0f), 1f * Time.deltaTime);
            }
            yield return null;
        }
    }
}
