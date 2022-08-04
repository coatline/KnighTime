using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public bool isEnemyArrow;
    public bool shot = false;
    AudioSource audio;
    public AudioClip arrowHit;
    public AudioClip arrowHit_two;
    Collider2D col;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();

        if (!isEnemyArrow)
        {
            transform.rotation = Quaternion.Euler(0, 0, (Random.Range(0, 361f)));
        }
    }

    bool dead;
    float deadtimer;

    private void Update()
    {
        if (dead && !audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || (collision.gameObject.CompareTag("Enemy") && !isEnemyArrow) && shot && !dead)
        {
            var rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0: audio.PlayOneShot(arrowHit); break;
                case 1: audio.PlayOneShot(arrowHit_two); break;
            }
            dead = true;
            col.enabled = false;
            sr.enabled = false;
        }
    }
}
