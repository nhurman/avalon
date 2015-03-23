/* VRScreenWarningAnimation
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiddleVR_Unity3D;

public class VRScreenWarningAnimation : MonoBehaviour {

    private GameObject m_NodeToWatch;
    private float      m_NearDistance = 0.01f;

    void Update ()
    {
        if (m_NodeToWatch == null)
        {
            return;
        }

        // Set near distance
        this.GetComponent<Renderer>().material.SetFloat("_NearDistance", m_NearDistance);

        // Halo position
        Vector3 nodePosition = m_NodeToWatch.transform.position;
        this.GetComponent<Renderer>().material.SetVector("_HeadPosition", new Vector4(nodePosition.x, nodePosition.y, nodePosition.z, 1.0f));

        // Make texture slide
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.08f*Time.time%1.0f));

        // Make texture blink
        float bright = Mathf.Clamp( 1.5f-(Time.time%1.0f), 0.0f, 1.0f );
        this.GetComponent<Renderer>().material.SetFloat("_Brightness", bright);
    }

    public void SetNodeToWatch(GameObject iNode)
    {
        m_NodeToWatch = iNode;
    }

    public void SetNearDistance(float iNearDistance)
    {
        m_NearDistance = iNearDistance;
    }
}
