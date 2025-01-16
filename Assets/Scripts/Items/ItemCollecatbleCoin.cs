using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class ItemCollecatbleCoin : ItemCollectableBase {

    public Collider2D collider;

    protected override void OnCollect() {

        
        base.OnCollect();
        ItemManager.Instance.AddCoins(_amount);
        collider.enabled = false;
    }
}
