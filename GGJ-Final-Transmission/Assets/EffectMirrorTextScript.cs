using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMirrorTextScript : MonoBehaviour {

    private TextMesh parentTxtMesh;
    private TextMesh txtMesh;

	// Use this for initialization
	void Start () {
        txtMesh = this.GetComponent<TextMesh>();
        parentTxtMesh = this.transform.parent.GetComponent<TextMesh>();
        updateSettings();
	}


    // Update is called once per frame

    void Update () {
		if(txtMesh.text.GetHashCode() != parentTxtMesh.text.GetHashCode())
        {
            updateSettings();
        }
	}

    void updateSettings()
    {
        txtMesh.text = parentTxtMesh.text;
        txtMesh.alignment = parentTxtMesh.alignment;
        txtMesh.fontSize = parentTxtMesh.fontSize;
        txtMesh.anchor = parentTxtMesh.anchor;
        txtMesh.characterSize = parentTxtMesh.characterSize;
    }
}
