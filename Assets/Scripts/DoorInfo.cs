using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * allows for Doors to be open differnt ways based on enums that Unity editor can fill out
 * author: Jeffrey Lansford
 */
public class DoorInfo : MonoBehaviour
{
    // doors open left and right
    public enum Open {
        Right,
        Left
    }

    // some doors center of the model is not at the center of the door like the outhouse door
    public enum Center {
        Center,
        NotCenter
    }
    // allows Unity editor to set the values and KeyOpen.cs have access to this variables  
    public Open whichWay;
    public Center origin;

}
