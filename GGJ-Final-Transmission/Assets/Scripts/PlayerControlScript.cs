using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {

    public Vector3 inputVec;
    private float moveSpeed = 5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        inputVec = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVec += new Vector3(0f, 1f, 0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVec += new Vector3(0f, -1f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVec += new Vector3(-1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVec += new Vector3(1f, 0f, 0f);
        }

        this.transform.position += (inputVec * Time.deltaTime * moveSpeed);


	}
}
