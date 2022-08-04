using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDetector : MonoBehaviour {

    public Button transitionButton;

	void Start () {
		
	}
	
	void Update () {
        if (transform.childCount == 0)
        {
            transitionButton.gameObject.SetActive(true);
        }
	}
}
