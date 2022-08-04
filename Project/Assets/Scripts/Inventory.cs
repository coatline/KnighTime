using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject currentWeapon = null;
    public Image[] itemHolders;
    public Sprite holderSprite;
    public int numItemsInInventory;
    public GameObject disabledPickupablesHolder;
    public int arrows;

    private void Awake()
    {
        disabledPickupablesHolder = new GameObject("disabled items");
    }

    public void EquipWeapon(GameObject weapon, string name)
    {
        currentWeapon = weapon;
        currentWeapon.transform.position = transform.position + new Vector3(.4f, 0, 0);
    }

    public void UnEquipWeapon()
    {
        currentWeapon.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), 0, 0);
        currentWeapon = null;
    }

    private void Update()
    {
        if (currentWeapon != null)
        {
            MoveWeapon();
        }
    }

    public void MoveWeapon()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector3 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        //currentWeapon.transform.position = transform.position + new Vector3(.4f, 0);
        //currentWeapon.transform.up = dir;

        currentWeapon.transform.LookAt(mousePos, -Vector3.forward);

        if (currentWeapon.GetComponent<PickupAble>().name == "Bow")
        {
            currentWeapon.transform.rotation *= Quaternion.AngleAxis(-90, Vector3.forward);
            currentWeapon.transform.position = transform.position + new Vector3(.4f, 0);
        }
        else if (currentWeapon.GetComponent<PickupAble>().name == "Sword")
        {
            Vector3 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            if (Input.mousePosition.x >= Screen.width / 2)
            {
                currentWeapon.transform.position = transform.position + new Vector3(.4f, 0);
            }
            else if(Input.mousePosition.x < Screen.width / 2)
            {
                currentWeapon.transform.position = transform.position - new Vector3(.4f, 0);
            }

            currentWeapon.transform.up = dir;
        }

        

    }

    public void AddItemToSlot(GameObject item, Sprite uiSprite)
    {
        item.transform.SetParent(disabledPickupablesHolder.transform);

        item.transform.rotation = Quaternion.identity;

        for (int i = 0; i < itemHolders.Length; i++)
        {
            var itemHolderScript = itemHolders[i].GetComponent<ItemHolder>();


            if (itemHolderScript.mySprite == null)
            {
                //print("Added " + item.name + " to inventory");
                if (item.GetComponent<PickupAble>().name == "Arrow")
                {
                    arrows++;
                }
                numItemsInInventory++;
                itemHolderScript.ChangeSprite(uiSprite, true);
                break;
            }
        }
    }

    public void RemoveItemFromSlot(Transform playerTransform, bool dropIt = true)
    {

        for (int i = itemHolders.Length - 1; i >= 0; i--)
        {
            var itemHolderScript = itemHolders[i].GetComponent<ItemHolder>();

            if (itemHolderScript.mySprite != null)
            {
                numItemsInInventory--;
                itemHolderScript.ChangeSprite(holderSprite, false);

                if (dropIt)
                {
                    var child = disabledPickupablesHolder.transform.GetChild(disabledPickupablesHolder.transform.childCount - 1);
                    if (child.GetComponent<PickupAble>().name == "Arrow")
                    {
                        arrows--;
                    }
                    child.SetParent(null);
                    var randPos = Random.Range(0, 2);
                    int xx = -1;
                    int xy = 1;

                    child.GetComponent<Arrow>().shot = false;

                    child.transform.position = transform.position + new Vector3(Random.Range(xx, xy), Random.Range(-1, 2), 0);
                    child.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
                    child.gameObject.SetActive(true);
                }

                break;
            }
        }

    }

    public void ShootBow()
    {
        for (int i = itemHolders.Length - 1; i >= 0; i--)
        {
            var itemHolderScript = itemHolders[i].GetComponent<ItemHolder>();

            if (disabledPickupablesHolder.transform.childCount == 0)
            {
                break;
            }

            var child = disabledPickupablesHolder.transform.GetChild(disabledPickupablesHolder.transform.childCount - 1);

            var pickupAbleScript = child.GetComponent<PickupAble>();

            if (pickupAbleScript.name == "Arrow")
            {
                if (itemHolderScript.mySprite != null)
                {
                    itemHolderScript.ChangeSprite(holderSprite, false);
                    numItemsInInventory--;
                    arrows--;
                    child.SetParent(null);
                    Vector3 mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    Vector3 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

                    dir += new Vector3(Random.Range(-.1f, .2f), Random.Range(-.3f, .4f), 0);

                    child.transform.up = dir;
                    child.transform.position = transform.position + transform.forward;

                    child.gameObject.SetActive(true);
                    var rb = child.GetComponent<Rigidbody2D>();
                    rb.AddForce(dir * 100);

                    child.GetComponent<Arrow>().shot = true;

                    break;
                }
            }
        }
    }
}
