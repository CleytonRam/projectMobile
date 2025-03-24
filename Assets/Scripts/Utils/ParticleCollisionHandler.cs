using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    private ParticleSystem particleSystemHand;

     void Start()
    {
        particleSystemHand = GetComponent<ParticleSystem>();
        
        var collision = particleSystemHand.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.mode = ParticleSystemCollisionMode.Collision3D;
        collision.dampen = 0.0f;
        collision.bounce = 0.7f;
        collision.lifetimeLoss = 0.0f;

        collision.collidesWith = LayerMask.GetMask("Default");
    }

}
