using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.Equals("Customer"))
        {
            anim.enabled = true;
            anim.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Customer"))
        {
            anim.SetBool("isOpen", false);
        }
    }
}
