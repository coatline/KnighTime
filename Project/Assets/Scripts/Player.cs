using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Inventory myInventory;
    public float speed;
    public float speedDecreasePerArrow;
    AudioSource audio;
    public AudioClip walk;
    public AudioClip pickup;
    public AudioClip pickup_two;
    public AudioClip arrowshoot;
    public AudioClip arrowshoot_two;
    public AudioClip arrowshoot_three;
    public AudioClip die;
    public AudioClip die_two;
    public AudioClip die_three;
    public AudioClip sword;
    public AudioClip drop;
    public AudioClip drop_two;
    bool dead = false;
    float deathTimer = 0;
    SpriteRenderer sr;
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        myInventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (deathTimer >= 1)
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.buildIndex);
        }
        if (dead)
        {
            deathTimer += Time.deltaTime;
            return;
        }

        Move();
        DropItem();
        FireWeapon();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy Sword"))
        {
            dead = true;
            sr.enabled = false;
            col.enabled = false;
            if (myInventory.currentWeapon != null)
            {
                myInventory.currentWeapon.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
                myInventory.currentWeapon = null;
            }
            var rand = Random.Range(0, 3);

            switch (rand)
            {
                case 0: audio.PlayOneShot(die); break;
                case 1: audio.PlayOneShot(die_two); break;
                case 2: audio.PlayOneShot(die_three); break;
            }
            audio.PlayOneShot(sword);
        }

        if (collider.gameObject.CompareTag("PickupAble"))
        {

            if (myInventory == null)
            {
                myInventory = GetComponent<Inventory>();
            }

            var pickupScript = collider.GetComponent<PickupAble>();

            var script = collider.gameObject.GetComponent<PickupAble>();

            if (script.name == "Arrow")
            {
                if (collider.gameObject.GetComponent<Arrow>().isEnemyArrow)
                {
                    dead = true;
                    var rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0: audio.PlayOneShot(die); break;
                        case 1: audio.PlayOneShot(die_two); break;
                        case 2: audio.PlayOneShot(die_three); break;
                    }
                    sr.enabled = false;
                    col.enabled = false;
                    if (myInventory.currentWeapon != null)
                    {
                        myInventory.currentWeapon.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
                        myInventory.currentWeapon = null;
                    }
                    return;
                }
            }

            if (myInventory.numItemsInInventory < myInventory.itemHolders.Length && (script.name != "Bow" && script.name != "Sword" && script.name != "Gun") && !collider.gameObject.GetComponent<Arrow>().shot)
            {
                audio.PlayOneShot(pickup);
                pickupScript.OnPickup();
                myInventory.AddItemToSlot(collider.gameObject, pickupScript.Image);
                speed -= speedDecreasePerArrow;
            }
            else if (myInventory.currentWeapon == null && (script.name == "Bow" || script.name != "Sword" || script.name != "Gun") && script.name != "Arrow")
            {
                audio.PlayOneShot(pickup_two);
                script.equipped = true;
                myInventory.EquipWeapon(collider.gameObject, script.name);
            }
        }
    }

    void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && myInventory.numItemsInInventory > 0)
        {
            audio.PlayOneShot(drop_two);
            myInventory.RemoveItemFromSlot(transform, true);
            speed += speedDecreasePerArrow;
        }
        if (Input.GetButtonDown("Fire2") && myInventory.currentWeapon != null)
        {
            audio.PlayOneShot(drop);
            myInventory.currentWeapon.GetComponent<PickupAble>().equipped = false;
            myInventory.UnEquipWeapon();
        }
    }

    void FireWeapon()
    {
        if (Input.GetButtonDown("Fire1") && myInventory.currentWeapon != null)
        {
            if (myInventory.currentWeapon.GetComponent<PickupAble>().name == "Bow")
            {
                if (myInventory.arrows > 0)
                {
                    var r = Random.Range(0, 3);
                    switch (r)
                    {
                        case 0: audio.PlayOneShot(arrowshoot_two); break;
                        case 1: audio.PlayOneShot(arrowshoot); break;
                        case 2: audio.PlayOneShot(arrowshoot_three); break;
                    }
                    speed += speedDecreasePerArrow;
                    myInventory.ShootBow();
                }
            }
        }
    }

    float timer = 0;
    float timeTilStepSound = .5f;

    void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (timer >= timeTilStepSound)
            {
                audio.PlayOneShot(walk);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (timer >= timeTilStepSound)
            {
                audio.PlayOneShot(walk);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            if (timer >= timeTilStepSound)
            {
                audio.PlayOneShot(walk);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            if (timer >= timeTilStepSound)
            {
                audio.PlayOneShot(walk);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
