using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManage : MonoBehaviour
{
    [Header("Animation")]
    public AnimatorManager animatorManager;

    private void Awake()
    {
        StartAnimation();   
    }
    public void StartAnimation()
    {
         
        animatorManager.Play(AnimatorManager.AnimationType.IDLE);
    }
}
