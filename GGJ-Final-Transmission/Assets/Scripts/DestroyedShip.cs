using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedShip : MonoBehaviour {

    public Rigidbody2D rb;
    
	void Start ()
    {
        rb.rotation = Random.Range(0.0f, 360.0f);
        rb.angularVelocity = Random.Range(1, 5) * 30.0f;
        rb.velocity = new Vector2(0.0f, -1.0f);

    }
}
