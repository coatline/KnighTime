using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reference : MonoBehaviour
{
    public Sprite holderSprite;
    public Inventory inventory;
    public GameObject itemHolderHolder;
    public Image[] itemHolders;

    private void Awake()
    {
        inventory.itemHolders = itemHolders;
        inventory.holderSprite = holderSprite;
    }

}