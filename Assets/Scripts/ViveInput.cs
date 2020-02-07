using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
 * Srcipt to allow vive to interact with GameObjects
 * author: Jeffrey Lansford
 */
public class ViveInput : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction= null;
    public SteamVR_Behaviour_Pose m_Pose = null;
    public FixedJoint m_Joint = null;
    private Interact m_CurrrentInter = null;
    private List<Interact> m_ContactInter = new List<Interact>();

    /*
     * called when script is being loaded
     * sets Pose and Joint from Controller gameobject
     */
    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    /*
     * called after every frame
     * checks for state postions of grab action on controller and either picksup object or drops 
     */
    void Update() {
        // Down
        if ( m_GrabAction.GetStateDown(m_Pose.inputSource)) {
            print(m_Pose.inputSource + " trigger Down");
            Pickup();
        }

        //Up
        if ( m_GrabAction.GetStateUp(m_Pose.inputSource)) {
            print(m_Pose.inputSource + " trigger up");
            Drop();
        }
    }

    /*
     * checks tags with it enters a collider
     */
    private void OnTriggerEnter(Collider other) {
        // adds interactble object into list
        if (other.gameObject.CompareTag("Interactable")) {
            m_ContactInter.Add(other.gameObject.GetComponent<Interact>());
        }
        
        
    }
    
    /*
     * destorys object tagged "Destoryable" when trigger is down and is colliding with other gameobject
     */
    private void OnTriggerStay(Collider other) {
        // destorys tagged objects "Destroyable"
        if (other.gameObject.CompareTag("Destroyable") && m_GrabAction.GetState(m_Pose.inputSource)) {
            print("destroy");
            Destroy(other.gameObject);
        }
    }

    /*
     * removes interactable object from list
     */
    private void OnTriggerExit(Collider other) {
        // adds interactble object into list
        if (other.gameObject.CompareTag("Interactable"))
            m_ContactInter.Remove(other.gameObject.GetComponent<Interact>());
        
    }

    /*
     * attachs interactable object into controllers joint
     */
    public void Pickup() {

        // get nearest interactable
        m_CurrrentInter = GetNearInteract();


        // null check
        if (!m_CurrrentInter) return;

        // already held check
        if (m_CurrrentInter.m_ActiveHand)
            m_CurrrentInter.m_ActiveHand.Drop();

        // postion 
        m_CurrrentInter.transform.position = transform.position;

        // attach
        Rigidbody targetBody = m_CurrrentInter.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // set active hand
        m_CurrrentInter.m_ActiveHand = this;

    }

    /*
     * dettachs interactable onject from controllers joint
     */
    public void Drop() {
        // null check
        if (!m_CurrrentInter) return;

        // apply velocity
        Rigidbody targetBody = m_CurrrentInter.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // detach
        m_Joint.connectedBody = null;

        // clear
        m_CurrrentInter.m_ActiveHand = null;
        m_CurrrentInter = null;

    }

    /*
     * returns nearest interactable object from list for Pickup()
     */
    private Interact GetNearInteract() {
        // sets local variables
        Interact nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0;
        // goes through list and gets nearert interactble object
        foreach (Interact interactable in m_ContactInter) {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if ( distance < minDistance) {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}
