using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public GameObject levelM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider C)
    {

        if (C.gameObject.tag == "robot")
        {
            Destroy(C.gameObject);
        }
        if (C.gameObject.tag == "Player")
        {
            levelM.GetComponent<levelManager>().is_die = true;
        }
    }



}
