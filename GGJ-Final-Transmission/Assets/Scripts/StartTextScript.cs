﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextScript : MonoBehaviour {

    public string displayString;
    public string contentString;

    public float textKeyTimer = 0f;
    private float textKeyThreshold = 0.04f;

    private bool isTriggered;

    private TextMesh txtMesh;

    private string beginningString;

    private int charCount = -1;
    private int charThreshold;

    private bool isReachedEndOfMessage = false;

    public float fadeOutTimer = 0f;
    private float fadeOutThreshold = 1.0f;

    private Color startColor;
    private Color clearColor;

    public AnimationCurve xScaleCurve;
    public AnimationCurve yScaleCurve;

    private Vector3 startScale;

    private EffectTextBackScript textBack;

    // Use this for initialization
    void Start () {

        textBack = GameObject.FindObjectOfType<EffectTextBackScript>();

        txtMesh = this.GetComponent<TextMesh>();
        txtMesh.text = "";

        startColor = txtMesh.color;
        clearColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        startScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            textKeyTimer += Time.unscaledDeltaTime;
            if( textKeyTimer > textKeyThreshold && !isReachedEndOfMessage)
            {
                textKeyTimer -= textKeyThreshold;
                charCount++;

                if (!isReachedEndOfMessage && charCount >= charThreshold)
                {
                    isReachedEndOfMessage = true;
                }
                else
                {
                    displayString += contentString[charCount];
                }

            }

            if (isReachedEndOfMessage)
            {
                fadeOutTimer += Time.unscaledDeltaTime;

                this.transform.localScale = new Vector3(startScale.x * xScaleCurve.Evaluate(fadeOutTimer/ fadeOutThreshold), startScale.y * yScaleCurve.Evaluate(fadeOutTimer / fadeOutThreshold), startScale.z);
                if(fadeOutTimer > fadeOutThreshold)
                {
                    isReachedEndOfMessage = false;
                    isTriggered = false;
                    this.transform.localScale = new Vector3(0f, 0f, 0f);
                    textBack.fadeOutBlack();
                }
            }
        }

        if(txtMesh.text.GetHashCode() != displayString.GetHashCode())
        {
            txtMesh.text = displayString;
        }
	}

    public void showMessage(string bodyText)
    {
        Debug.Log("show start message with body: " + bodyText);
        textBack.fadeInBack();
        this.transform.localScale = startScale;
        fadeOutTimer = 0f;
        textKeyTimer = 0f;
        charCount = -1;
        isTriggered = true;
        displayString = "";
        contentString = "";
        txtMesh.text = "";
        //displayString = bodyText;
        beginningString = "It is over for us.\nThis is " + NameGenerator.getNewName() + "'s final transmission...\n";
        contentString += beginningString + bodyText + "\n...............end";
        charThreshold = contentString.Length;

        Debug.Log("content length:" + contentString.Length);
        Debug.Log("char:" + charThreshold);
    }
}
