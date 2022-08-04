using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemy : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Player player;
    float timer;
    float shotRate = 2;
    AudioSource audio;
    public AudioClip die;
    public AudioClip die_two;
    public AudioClip die_three;
    public AudioClip sword;
    public AudioClip arrowshoot;
    public AudioClip arrowshoot_two;
    public AudioClip arrowshoot_three;
    SpriteRenderer sr;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        shotRate = Random.Range(1f, 2f);
    }

    void Update()
    {
        if (deathtimer >= 1)
        {
            Destroy(gameObject);
        }

        if (dead)
        {
            deathtimer += Time.deltaTime;
            return;
        }
        else
        {
            var direction = (player.transform.position - transform.position).normalized;

            RaycastHit2D cast = Physics2D.Raycast(transform.position, direction, 100, ~(1 << gameObject.layer | 1 << arrowPrefab.layer));
            if (cast.collider != null && cast.transform.CompareTag("Player"))
            {
                Shoot();
                Debug.DrawLine(transform.position, player.transform.position, Color.green);
            }
            else
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
        }

        timer += Time.deltaTime;
    }

    void Shoot()
    {
        if (timer < shotRate)
        {
            return;
        }

        timer = 0;
        shotRate = Random.Range(1f, 2f);

        var newArrow = Instantiate(arrowPrefab);
        newArrow.transform.position = transform.position;

        Vector3 playerPos = player.transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
        Vector3 dir = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);

        newArrow.transform.up = dir;
        newArrow.transform.position = transform.position;

        var rb = newArrow.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * 100);
        var script = newArrow.GetComponent<Arrow>();
        script.isEnemyArrow = true;
        script.shot = true;

        var r = Random.Range(0, 3);
        switch (r)
        {
            case 0: audio.PlayOneShot(arrowshoot_two); break;
            case 1: audio.PlayOneShot(arrowshoot); break;
            case 2: audio.PlayOneShot(arrowshoot_three); break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickupAble"))
        {
            var script = collision.gameObject.GetComponent<PickupAble>();

            if (script.name == "Arrow")
            {

                var otherScript = collision.gameObject.GetComponent<Arrow>();

                if (otherScript.shot && !otherScript.isEnemyArrow)
                {
                    for (int i = 0; i < Random.Range(1, 3); i++)
                    {
                        var newAr = Instantiate(arrowPrefab);
                        newAr.transform.position = transform.position + new Vector3(Random.Range(-.5f, .6f), Random.Range(-.5f, .6f));
                        newAr.transform.rotation = Quaternion.Euler(0, 0, (Random.Range(0, 361f)));

                    }
                    var rand = Random.Range(0, 3);

                    switch (rand)
                    {
                        case 0: audio.PlayOneShot(die); break;
                        case 1: audio.PlayOneShot(die_two); break;
                        case 2: audio.PlayOneShot(die_three); break;
                    }
                    dead = true;
                    deathtimer = 0;
                    sr.enabled = false;
                    col.enabled = false;
                }
            }

            else if (script.name == "Sword")
            {
                for (int i = 0; i < Random.Range(1, 3); i++)
                {
                    var newAr = Instantiate(arrowPrefab);
                    newAr.transform.position = transform.position + new Vector3(Random.Range(-.5f, .6f), Random.Range(-.5f, .6f));
                    newAr.transform.rotation = Quaternion.Euler(0, 0, (Random.Range(0, 361f)));

                }
                var rand = Random.Range(0, 3);

                switch (rand)
                {
                    case 0: audio.PlayOneShot(die); break;
                    case 1: audio.PlayOneShot(die_two); break;
                    case 2: audio.PlayOneShot(die_three); break;
                }
                audio.PlayOneShot(sword);
                dead = true;
                sr.enabled = false;
                col.enabled = false;
                //Destroy(gameObject);
            }
        }
    }

    bool dead = false;
    float deathtimer = 0;

}
