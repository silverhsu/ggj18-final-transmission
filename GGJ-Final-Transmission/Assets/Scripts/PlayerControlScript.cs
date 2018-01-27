using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    public const int ASTEROID_DAMAGE = 2;

    private float moveSpeed = 5f;
    private Vector3 inputVelocity = Vector3.zero;
    private Vector3 accel = Vector3.zero;
    private Rigidbody2D rb = null;

    public int maxHealth = 6;
    public int health = 6;
    public bool dead = false;
    public Slider healthSlider = null;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
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
        if (!dead)
        {
            Vector3 velocity = rb.velocity;
            velocity = Vector3.SmoothDamp(velocity, inputVelocity, ref accel, 0.1f);
            rb.velocity = velocity;

            //rb.velocity = inputVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        Debug.Log(tag);
        if (tag == "Asteroid")
        {
            TakeDamage(ASTEROID_DAMAGE);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        string tag = other.tag;
        Debug.Log(tag);
        if (tag == "Asteroid")
        {
            TakeDamage(ASTEROID_DAMAGE);
        }
    }

    public void TakeDamage(int damage)
    {
        if (dead) { return; }

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            dead = true;
        }
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }
}
