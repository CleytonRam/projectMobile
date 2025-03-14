using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public new Animation animation;

    public AnimationClip run;
    public AnimationClip idle;

    private void Update()
    {
        //if ( (Input.GetKeyDown(KeyCode.A)))
        //{
        //    PlayAnimation(run);          
        //}
        //else if ((Input.GetKeyDown(KeyCode.S)))
        //{
        //    PlayAnimation(idle);      
        //}
    }

    private void PlayAnimation(AnimationClip c)
    {
        //animation.clip = c;
        animation.CrossFade(c.name);
    }
}
