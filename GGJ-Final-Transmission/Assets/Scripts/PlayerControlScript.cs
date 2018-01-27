using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {

    private float moveSpeed = 5f;
    private Vector3 inputVelocity = Vector3.zero;
    private Vector3 accel = Vector3.zero;
    private Rigidbody2D rb = null;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
    {
        inputVelocity = Vector3.zero;
        inputVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        inputVelocity.y = Input.GetAxis("Vertical") * moveSpeed;
	}

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity = Vector3.SmoothDamp(velocity, inputVelocity, ref accel, 0.1f);
        rb.velocity = velocity;

        //rb.velocity = inputVelocity;
    }
}
