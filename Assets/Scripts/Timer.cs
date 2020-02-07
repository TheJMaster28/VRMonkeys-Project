using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /*
     * impleamts a timer into game and have it go to start screen when it runs out
     * author: Jeffrey Lansford
     */
    [Tooltip("Maxium timer is allowed before game is over, measured in seconds")]
    public float maxTime;
    private float timer;
    public Text timerText;
    public bool stopTimer;
    private float wait;
    // Update is called once per frame

    private void Start() {
        wait = 5;
    }
    void Update() {

        if (!stopTimer) {
            // subtracts 1 for every second
            maxTime -= Time.deltaTime;
            // writes it to UI for player to see
            timerText.text = ((int)maxTime).ToString();

            // loads Start screen when timer runs out
            if (maxTime < 0) {
                timerText.rectTransform.localPosition = new Vector3(0, 0, 0);
                timerText.text = "You Lose\nTry Again";
                timerText.alignment = TextAnchor.MiddleCenter;
                if ( wait < 0 )
                    SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
                wait -= Time.deltaTime;
            }
        }
    }
}
