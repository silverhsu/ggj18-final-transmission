using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTransmissionScript : MonoBehaviour {

    public TextMesh txtMesh;

    public SpriteMask glassMask;
    private Vector3 glassStartScale;
    private Vector3 glassTargetScale;

    private string displayString = "";
    private string beginningString = "It is over for us.\nThis is our final transmission...";
    private string queryString = "";

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
    private float typingThreshold = 8f;

    private bool isDoneTyping = false;

    public AnimationCurve probabilityCurve;

    public SpriteRenderer glassLayer;

    public LiesDatabase liesDB;

    private bool isTriggered = false;

    private EffectTextBackScript textBackScript;

    public SpriteRenderer whiteOutRend;

    public TextMesh detonationText;
    private Color startColorDetonate;

    // Use this for initialization
    void Start () {
        textBackScript = GameObject.FindObjectOfType<EffectTextBackScript>();
        glassStartScale = glassMask.transform.localScale;
        glassMask.transform.localScale = Vector3.zero;
        glassTargetScale = Vector3.zero;
        displayString = "";
        beginningString = "It is over for us.\nThis is " + NameGenerator.getNewName() + "'s final transmission...";
        txtMesh.text = displayString;
        startColorDetonate = detonationText.color;
        detonationText.color = Color.clear;
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
                    //queryString += beginningString[stringIter];

                    stringIter++;
                    if (stringIter >= beginningString.Length)
                    {
                        displayString += "\n";
                        //queryString += "\n";
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
                    queryString += currAddString;

                    typeCount += currAddString.Length;

                    if (typeCount > typeOverflowAbsThreshold)
                    {
                        displayString += "\n";
                        queryString += "\n";
                        typeCount = 0;
                    }
                    else if (typeCount > typeOverflowThreshold && currAddString.Contains(" "))
                    {
                        displayString += "\n";
                        queryString += "\n";
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


            if (!isDoneTyping && typingTimer > typingThreshold)
            {
                isDoneTyping = true;

                liesDB.InsertMessage(queryString);
                //
                textBackScript.fadeOutBlack();
                //SUBMIT
                Debug.Log(queryString);
                
                
            }

            detonationText.color = Color.Lerp(detonationText.color, startColorDetonate, Time.unscaledDeltaTime * 1f);
            detonationText.text = "Detonation in " + (typingThreshold - typingTimer).ToString("F3");
        }

        if(typingTimer > typingThreshold + 1.5f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(typingTimer > (typingThreshold * 0.8f))
        {
            whiteOutRend.color = Color.Lerp(whiteOutRend.color, Color.white, Time.unscaledDeltaTime * 2f);
        }

        glassMask.transform.localScale = Vector3.Lerp(glassMask.transform.localScale, glassTargetScale, 3f * Time.unscaledDeltaTime);
    }

    public void triggerTransmission()
    {
        textBackScript.fadeInBack();
        glassTargetScale = glassStartScale;
        /*
        if (!glassLayer.gameObject.activeSelf)
        {
            glassLayer.gameObject.SetActive(true);
        }
        */
            isTriggered = true;
    }
}
