using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ItemCollecatbleCoin : ItemCollectableBase
{

    public Collider colider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;

    [Header("Coin Particles")]
    public ParticleSystem coinParticles;

    public void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }
    protected override void OnCollect()
    {


        base.OnCollect();
        colider.enabled = false;
        collect = true;
        PlayerController.Instance.Bounce();
        PlayCollectParticles();
    }

    protected override void Collect()
    {
        OnCollect();
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {

                Destroy(gameObject);
            }
        }
    }

    private void PlayCollectParticles() 
    {
        if (coinParticles != null)
        {
            ParticleSystem particle = Instantiate(coinParticles, transform.position, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);
        }
    }


}
