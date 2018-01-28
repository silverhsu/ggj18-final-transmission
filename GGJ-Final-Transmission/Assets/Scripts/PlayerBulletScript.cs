using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

    public Collider2D collider = null;
    public Rigidbody2D rb = null;
    public float moveSpeed = 9f;
    
	void Start () {
        //rigid = this.GetComponent<Rigidbody2D>();
        //bulletCol = this.GetComponent<Collider2D>();
        rb.velocity = new Vector2(0f, moveSpeed);
        Destroy(this.gameObject, 4f);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Bullet collided");
        collider.enabled = false;
        Destroy(this.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Bullet particle col");
        collider.enabled = false;
        Destroy(this.gameObject);
    }
}
