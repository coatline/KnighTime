using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour {

    public Sprite image;
    public string name;
    public bool equipped = false;

    public Sprite Image
    {
        get { return image; }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}
