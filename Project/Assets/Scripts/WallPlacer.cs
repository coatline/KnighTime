using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour {

    //public GameObject wallPrefab;

	void Start () {

	}
	
	void Update () {
        //transform.position = Camera.main.transform.position + new Vector3(1f,6,10);		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
