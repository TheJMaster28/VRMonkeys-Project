using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/* allows the player to teleport around the world
 * author: Jeffrey Lansford, Kitt Phi, Ivan Nieto
 */
public class Teleporter : MonoBehaviour
{
    // needs gameObject to use a pointer for player
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.5f;
    private SteamVR_Behaviour_Pose m_Pose = null;

    /*
     * creates the pointer and gets the pose of the VR actions
     */
    private void Awake() {
        m_Pointer = Instantiate(m_Pointer, (new Vector3(0, 0, 0)), (new Quaternion()));
        m_Pointer.SetActive(false);
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    /*
     * checks when the teleport button is pressed then telepoet to location of pointer when released
     */
    private void Update() {
        // when teleport action if pressed 
        // update the pointer's postion and set active if the raycast hit some collider
        if (m_TeleportAction.GetState(m_Pose.inputSource)) {
            print("Teleport down");
            m_HasPosition = UpdatePointer();
            m_Pointer.SetActive(m_HasPosition);
        }
        // when released
        // teleport if it hit something 
        if (m_TeleportAction.GetStateUp(m_Pose.inputSource)) {
            TryTeleport();
            m_Pointer.SetActive(false);
        }
        
    }

    /*
     * checks if player can teleport
     */
    private void TryTeleport() {
            
            //checking if teleprot in progress
            if (!m_HasPosition || m_IsTeleporting)
                return;
            // gets orgin and headpostion of the VR object
            Transform camaraRig = SteamVR_Render.Top().origin;
            Vector3 headPosition = SteamVR_Render.Top().head.position;
            // sets up Vectors for transforming to pointer postion
            Vector3 groundPosition = new Vector3(headPosition.x, camaraRig.position.y, headPosition.z);
            Vector3 translateVector = m_Pointer.transform.position - groundPosition;
            // starts a coroutines of MoveRig
            StartCoroutine(MoveRig(camaraRig, translateVector));
    }

    /*
     * transforms postion to new positon while having fading to not disorient player
     */
    private IEnumerator MoveRig(Transform camaraRig, Vector3 translation) {
        // teleport is in progress
        m_IsTeleporting = true;
        // does fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);
        yield return new WaitForSeconds(m_FadeTime);
        // transform player to new positon
        camaraRig.position += translation;
        // fade out of balck
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);
        // no longer teleporting
        m_IsTeleporting = false;
    }

    /*
     * checks if raycast hits collider and updates pointer position
     */
    private bool UpdatePointer() {
        // makes ray cast
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // if it hit something
        if (Physics.Raycast(ray, out hit) ) {
            // move pointer to that position 
            m_Pointer.transform.position = hit.point;
            print("Hit point: " + hit.point);
            return true;
        }
        return false;
    }




}
