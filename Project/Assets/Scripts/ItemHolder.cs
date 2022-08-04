using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour {

    public Sprite mySprite = null;
    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    public void ChangeSprite(Sprite newSprite, bool hasItemNow)
    {
        myImage.sprite = newSprite;
        if (hasItemNow)
        {
            mySprite = newSprite;
        }
        else
        {
            mySprite = null;
        }
    }

}
