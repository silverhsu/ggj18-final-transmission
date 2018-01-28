using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextScript : MonoBehaviour {

    public string displayString;
    public string contentString;

    public float textKeyTimer = 0f;
    private float textKeyThreshold = 0.05f;

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

    // Use this for initialization
    void Start () {

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

        isTriggered = true;
        //displayString = bodyText;
        beginningString = "It is over for us.\nThis is " + NameGenerator.getNewName() + "'s final transmission...\n";
        contentString += beginningString + bodyText + "\n............";
        charThreshold = contentString.Length;

        Debug.Log("content length:" + contentString.Length);
        Debug.Log("char:" + charThreshold);
    }
}
