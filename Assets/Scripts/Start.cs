using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Starts the main sceen of the game by collison with gameobject
 * author Jeffrey Lansford
 */
public class Start : MonoBehaviour
{
    // on trigger with controllers' collider, load the  main scene of the game
    private void OnTriggerEnter(Collider other) {
        // loads main scene 
        if (other.gameObject.CompareTag("Hands")) {
            print("collison with player");
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
