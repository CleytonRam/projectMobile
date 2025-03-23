using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollecatbleCoin> itens;
    
    
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollecatbleCoin>();
    }

    public void RegisterCoin(ItemCollecatbleCoin i) 
    {
        if (!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
            Debug.Log("Coin added to list");

        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    StartAnimations();
        //}
    }

    public void StartAnimations() 
    {
        StartCoroutine(ScalePieceByTime());
    }

    IEnumerator ScalePieceByTime()
    {
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;

        }
        Sort();

        yield return null;
        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort() 
    {
        itens = itens.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
