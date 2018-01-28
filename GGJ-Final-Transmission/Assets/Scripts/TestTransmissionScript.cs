using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransmissionScript : MonoBehaviour {

    public TextMesh txtMesh;


    private string displayString = "";
    private string beginningString = "It is over for us.\nThis is our final transmission...";

    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()"
    private string messupString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()!@#$%^&*()!@#$%^&*()";
    private string currAddString = "";

    private bool isTypeable = false;

    private int stringIter = 0;

    private float keyTimer = 0f;
    private float keyThreshold = 0.06f;

    private int typeCount = 0;
    private int typeOverflowThreshold = 20;
    private int typeOverflowAbsThreshold = 40;

    public float typingTimer = 0f;
    private float typingThreshold = 12f;

    public AnimationCurve probabilityCurve;

    public SpriteRenderer glassLayer;

    private bool isTriggered = false;

    // Use this for initialization
    void Start () {
        displayString = "";
        beginningString = "It is over for us.\nThis is " + NameGenerator.getNewName() + "'s final transmission...";
        txtMesh.text = displayString;

	}

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (!isTypeable)
            {
                //keyTimer += Time.deltaTime;
                keyTimer += Time.unscaledDeltaTime;
                if (keyTimer > keyThreshold)
                {
                    keyTimer -= keyThreshold;

                    displayString += beginningString[stringIter];
                    stringIter++;
                    if (stringIter >= beginningString.Length)
                    {
                        displayString += "\n";
                        isTypeable = true;
                    }
                    //txtMesh.text = displayString;
                }
            }
            else
            {
                typingTimer += Time.unscaledDeltaTime;
                //typingTimer += Time.deltaTime;

                if (Input.inputString != "")
                {
                    
                    currAddString = "";
                    if (Random.Range(0f, 1f) < probabilityCurve.Evaluate(typingTimer / typingThreshold))
                    {
                        currAddString += messupString[Random.Range(0, messupString.Length)];
                        //displayString += messupString[Random.Range(0, messupString.Length)];
                        //typeCount += 1;
                    }
                    else
                    {
                        currAddString = Input.inputString;
                        //displayString += Input.inputString;
                        //typeCount += Input.inputString.Length;
                    }

                    displayString += currAddString;
                    typeCount += currAddString.Length;

                    if (typeCount > typeOverflowAbsThreshold)
                    {
                        displayString += "\n";
                        typeCount = 0;
                    }
                    else if (typeCount > typeOverflowThreshold && currAddString.Contains(" "))
                    {
                        displayString += "\n";
                        typeCount = 0;
                    }

                    //Debug.Log("Input string is not empty!");
                }
                else
                {
                    //Debug.Log("Input string is empty");
                }
            }

            if (displayString.GetHashCode() != txtMesh.text.GetHashCode())
            {
                txtMesh.text = displayString + "_";
            }


        }
    }

    public void triggerTransmission()
    {
        if (!glassLayer.gameObject.activeSelf)
        {
            glassLayer.gameObject.SetActive(true);
        }
            isTriggered = true;
    }
}
