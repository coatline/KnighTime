using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{

    public GameObject player;

    bool dead = false;
    float deathTimer = 0;
    AudioSource audio;
    public AudioClip step;
    public AudioClip die;
    public AudioClip die_two;
    public AudioClip die_three;
    public AudioClip sword;
    public Arrow arrowPrefab;
    Collider2D col;
    SpriteRenderer sr;

    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (deathTimer > 1)
        {
            Destroy(gameObject);
        }
        if (dead)
        {
            deathTimer += Time.deltaTime;
            return;
        }

        var direction = (player.transform.position - transform.position).normalized;

        RaycastHit2D cast = Physics2D.Raycast(transform.position, direction, 100, arrowPrefab.gameObject.layer | gameObject.layer);

        if (cast.collider != null && cast.transform.CompareTag("Player"))
        {
            Attack();
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
    }

    float timer = 0;
    float timeTilStepSound = .3f;

    void Attack()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Random.Range(1.5f, 2f) * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= timeTilStepSound)
        {
            timer = 0;
            audio.PlayOneShot(step);
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
                    Collider2D[] hi = transform.GetComponentsInChildren<Collider2D>();

                    for (int i = hi.Length - 1; i >= 0; i--)
                    {
                        hi[i].enabled = false;
                    }
                    for (int j = 0; j < transform.childCount; j++)
                    {
                        var child = transform.GetChild(j);
                        child.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
                    }
                    var rand = Random.Range(0, 3);

                    switch (rand)
                    {
                        case 0 : audio.PlayOneShot(die); break;
                        case 1 : audio.PlayOneShot(die_two); break;
                        case 2 : audio.PlayOneShot(die_three); break;
                    }

                    audio.PlayOneShot(die);
                    dead = true;
                    deathTimer = 0;
                    sr.enabled = false;
                    col.enabled = false;
                }
            }


            if (script.name == "Sword" && script.equipped)
            {
                dead = true;
                deathTimer = 0;
                col.enabled = false;
                sr.enabled = false;

                Collider2D[] hi = transform.GetComponentsInChildren<Collider2D>();

                for (int i = hi.Length - 1; i >= 0; i--)
                {
                    hi[i].enabled = false;
                }
                    for (int j = 0; j < transform.childCount; j++)
                    {
                        var child = transform.GetChild(j);
                        child.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 361));
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
        }
    }
}
