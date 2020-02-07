using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Opens the door with the tag Doortag
 * author: Jeffrey Lansford
 */
public class KeyOpen : MonoBehaviour
{
    // tag of door key can open
    public string Doortag;
    // allows door to be open only once
    private bool dontOpenagain = true;
    // each door gameOject needs to have DoorInfo class with the enums filled out
    private DoorInfo info;
    // when key collides with door, open depending on info of door
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag(Doortag) && dontOpenagain) {
            print("collion with door");
            dontOpenagain = false;
            // get class DoorInfo
            info = col.gameObject.GetComponent<DoorInfo>();
            // opens if door  open right and the center of the door object is at the center of the model
            if (info.whichWay == DoorInfo.Open.Right && info.origin == DoorInfo.Center.Center) {
                col.gameObject.transform.position += new Vector3(-.5f, 0, -0.5f);
                col.gameObject.transform.Rotate(Vector3.up, 90);
            }
            // opens if it opens left and the center of the door is not at the center of the model
            // the prefab door of the outhouse has it center on the left side of the door
            else if ( info.whichWay == DoorInfo.Open.Left && info.origin == DoorInfo.Center.NotCenter) {
                col.gameObject.transform.Rotate(Vector3.up, -90);
            }

            // opens if door open lefst and the center of the door object is at the center of the model
            else if (info.whichWay == DoorInfo.Open.Left && info.origin == DoorInfo.Center.Center) {
                col.gameObject.transform.position += new Vector3(.5f, 0, 0.5f);
                col.gameObject.transform.Rotate(Vector3.up, -90);
            }
        }
    }

    
}
