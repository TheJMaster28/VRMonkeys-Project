using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* goes back to StartScreen to signify the end of the game when collides with gameobject tagged "EndGame"
 * author: Jeffrey Lansford
 */
public class EndGame : MonoBehaviour
{
    // needs to have the Timer gameobject with the script Timer.cs
    public GameObject timer;
    private Timer time;
    public float wait;
    public Text text;
    private bool doEndGame;

    // gets the timer class from gameObject timer
    // and sets boolean to false to not run Endgame mechanics
    private void Start() {
        time = timer.GetComponent<Timer>();
        doEndGame = false;
    }

    // decremtes timer when we want to do end game stuff
    private void Update() {

        // delays the scene load by value of wait seconds 
        if (doEndGame) {
            wait -= Time.deltaTime;
        }
        // when delay is over, load StartScreen
        if (wait < 0) {
            print("End Game");
            // load start screen to signify end of game
            SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // ends game if object is tagged "EndGame"
        if (other.gameObject.CompareTag("Hands")) {
            // writes You win in the middle of the players VR screen  
            text.rectTransform.localPosition = new Vector3(0, 0, 0);
            text.text = "You Win";
            text.alignment = TextAnchor.MiddleCenter;
            // allows timer for delay of loading scene and stops the main timer to prevent conflict of loads
            doEndGame = true;
            time.stopTimer = true;

        }
        
    }
}
