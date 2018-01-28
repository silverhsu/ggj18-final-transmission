using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTextBackScript : MonoBehaviour {

    private Vector3 startScale;

    private Vector3 targetScale;



	// Use this for initialization
	void Start () {
        startScale = this.transform.localScale;
        this.transform.localScale = new Vector3(startScale.x, 0f, startScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.localScale != targetScale)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, 6f * Time.unscaledDeltaTime);
        }
	}

    public void fadeInBack()
    {
        targetScale = startScale;
    }


    public void fadeOutBlack()
    {
        targetScale = new Vector3(startScale.x, 0f, startScale.z);
    }
}
