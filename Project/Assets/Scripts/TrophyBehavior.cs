using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyBehavior : MonoBehaviour
{

    public Button mainMenuButton;
    public GameObject ps;
    RectTransform myTrans;
    float timer = 0;

    void Start()
    {
        myTrans = GetComponent<RectTransform>();
    }

    int state;
    int blah;

    void Update()
    {
        if (state == 0)
        {
            myTrans.Translate(2f, 0, 0);
            if (myTrans.anchoredPosition.x >= 0)
            {
                state = 1;
            }
        }
        else if (state == 1)
        {
            myTrans.Translate(0, 1f, 0);
            if (myTrans.anchoredPosition.y >= 30)
            {
                state = 2;
            }
        }
        else if (state == 2)
        {
            myTrans.Translate(0, -1f, 0);

            if (myTrans.anchoredPosition.y <= 0)
            {
                blah++;
                if (blah == 2)
                {
                    state = 3;
                }
                else
                {
                    state = 1;
                }
            }
        }
        else if (state == 3)
        {
            myTrans.Translate(0, -1f, 0);
            if (myTrans.anchoredPosition.y <= -150)
            {
                mainMenuButton.gameObject.SetActive(true);
                state = 4;
                ps.SetActive(true);
                var par = ps.GetComponent<ParticleSystem>();
                par.Play();
            }
        }
        else if(state == 4)
        {
            timer += Time.deltaTime;
            if(timer >= 2)
            {
                state = 5;
            }
        }
        else if (state == 5)
        {
            myTrans.Translate(0, -1f, 0);
            if(myTrans.anchoredPosition.y <= -400)
            {
                Destroy(gameObject);
            }
        }
    }
}
