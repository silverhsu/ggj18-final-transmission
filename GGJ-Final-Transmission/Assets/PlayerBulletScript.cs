using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

    private Collider2D bulletCol;
    private Rigidbody2D rigid;

    private float moveSpeed = 9f;

	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody2D>();
        bulletCol = this.GetComponent<Collider2D>();
        rigid.velocity = new Vector2(0f, moveSpeed);
        Destroy(this.gameObject, 4f);
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position += (this.transform.up * moveSpeed * Time.deltaTime);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Bullet collided");
        bulletCol.enabled = false;
        Destroy(this.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Bullet particle col");
        bulletCol.enabled = false;
        Destroy(this.gameObject);
    }
}
