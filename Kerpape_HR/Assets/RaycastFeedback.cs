using UnityEngine;
using System.Collections;

public class RaycastFeedback : MonoBehaviour
{

    public Color enterColor;
    public Color exitColor;

    //void OnTriggerEnter(Collider coll)
    //{

    //    MeshRenderer renderer = coll.gameObject.GetComponent<MeshRenderer>();
    //    if(renderer != null)
    //        renderer.material.color = enterColor;
    //}


    void OnTriggerExit(Collider coll)
    {

        MeshRenderer renderer = coll.gameObject.GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.material.color = exitColor;
    }

    void OnTriggerStay(Collider coll)
    {
        MeshRenderer renderer = coll.gameObject.GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.material.color = enterColor;
    }
}
