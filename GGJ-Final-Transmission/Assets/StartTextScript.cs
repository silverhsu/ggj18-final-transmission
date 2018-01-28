using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextScript : MonoBehaviour {

    public string contentString;

    public float textKeyTimer = 0f;
    private float textKeyThreshold = 0.05f;

    private bool isTriggered;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pullMessage()
    {
        isTriggered = true;
    }
}
