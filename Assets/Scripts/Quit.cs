using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Quits game when controllers collide with gameobject
 * author: Jeffrey Lansford
 */
public class Quit : MonoBehaviour
{
    // Quits game with gameobjects tagged "Hands"
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Hands") ) {
            print("Quiting Game");
            // only works when project is built
            Application.Quit();
        }
    }
    
}
