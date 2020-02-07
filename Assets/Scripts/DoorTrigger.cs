using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Door Opening Trigger In Unity - Beginner Friendly Tutorial/ Guide
//https://www.youtube.com/watch?time_continue=46&v=6vj_Ie9i-Ak
public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    private bool isOpended = false;
    private bool dontOpenagain = true;
    void OnTriggerEnter(Collider col)
    {

        if ( col.gameObject.CompareTag("Hands") )
            isOpended = true;

        if (isOpended && dontOpenagain)
        {

            dontOpenagain = false;
            door.transform.position += new Vector3(-.5f, 0, -0.5f);
            door.transform.Rotate(Vector3.up, 90);
        }

        
    }
}
