using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AnimationEvents : MonoBehaviour
{
    public void IsSat()
    {
        transform.GetComponent<Animator>().SetBool("isSat", true);
    }

    public void ExitTable()
    {
        transform.GetComponent<Animator>().SetBool("isSat", false);
        
    }
}
