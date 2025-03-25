using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using UnityEngine.UI;
using TMPro;



public class ItemManager : Singleton <ItemManager> { 


  
    public SOInt coins;
   
   
    private void Start() {
        Reset();

    }

    private void Reset()  {
        coins.value = 0;
       
        UpdateUI();
    }

    public void AddCoins(int amount = 1) {
        coins.value += amount;

        UpdateUI();
    }



    private void UpdateUI() {
       //coinsText.text = coins.ToString();

        //UIInGameManager.Instance.UpdateTextCoins(coins.value.ToString());
    }
}
