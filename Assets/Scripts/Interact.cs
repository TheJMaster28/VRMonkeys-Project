using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * script that allows object to be interactable by the VIVE contollers
 * author: Jeffrey Lansford
 */
// gameobject requires ridgebody
[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour {

    // hides variable from inspector
    [HideInInspector]
    // keeps track of active hand that gameobject is being held by
    public ViveInput m_ActiveHand = null;

}
