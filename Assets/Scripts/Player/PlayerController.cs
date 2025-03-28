using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Automove")]
    public float speed = 1f;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEnd = "EndLine";

    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;
    
    [Header("PowerUps")]
    public bool invincible = false;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    public GameObject endScreen;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("VFX")]
    public ParticleSystem vfxDeath;


    [SerializeField] private BounceHelper _bounceHelper;
    public Ease ease = Ease.OutBack;
    public float playerScaleDuration = .5f;

   


    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
        ScaleAtStart();
    }


    public void ScaleAtStart() 
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, playerScaleDuration).SetEase(ease);
    }
    public void Bounce()
    {
        if (_bounceHelper != null)
        {
            _bounceHelper.Bounce();
        }     
    }



    void Update()
    {
        if(!_canRun) return;


        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy) 
        {
            if (!invincible) 
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.DEAD); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckEnd) 
        {
            if (!invincible)
            {
                EndGame();
            }
        }
    }
    private void MoveBack(Transform t) 
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }
    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
        if (vfxDeath != null)
        {
            vfxDeath.Play();
        }
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
       uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b = true) 
    {
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease) 
    {
        /* var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p; */

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration) 
    {
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/
        transform.DOMoveY(_startPosition.y, 0.2f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }

    
    #endregion
}
