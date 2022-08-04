using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    AudioSource audio;
    public AudioClip buttonClick_one;
    public AudioClip buttonClick_two;
    public AudioClip buttonClick_three;

    bool waiting;
    int func;
    int ind;
    float timer = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ChangeSceneToIndex(int index)
    {
        ind = index;
        waiting = true;
        func = 0;

        var rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0: audio.PlayOneShot(buttonClick_one); break;
            case 1: audio.PlayOneShot(buttonClick_two); break;
            case 2: audio.PlayOneShot(buttonClick_three); break;
        }
    }
    public void ToNextScene()
    {
        var rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0: audio.PlayOneShot(buttonClick_one); break;
            case 1: audio.PlayOneShot(buttonClick_two); break;
            case 2: audio.PlayOneShot(buttonClick_three); break;
        }
        waiting = true;
        func = 1;
    }

    private void Update()
    {
        if (!waiting)
        {
            return;
        }

        if (waiting)
        {
            timer += Time.deltaTime;
        }
        if (timer >= .1f)
        {
            if (func == 0)
            {
                SceneManager.LoadScene(ind);
            }
            else if (func == 1)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex + 1);
            }
        }
    }
}
