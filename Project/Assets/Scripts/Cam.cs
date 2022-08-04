using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public bool follow;
    public Transform target;

    void LateUpdate()
    {
        if (follow)
        {
            transform.position = target.position - new Vector3(0, 0, 10);
        }
        else
        {
            transform.Translate(0, .01f, 0);
        }
    }
}
