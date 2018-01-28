using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 inputVelocity = Vector3.zero;
    private Vector3 accel = Vector3.zero;
    private Rigidbody2D rb = null;

    public int maxHealth = 1;
    public int health = 1;
    public bool dead = false;
    public Slider healthSlider = null;
    public GameObject explosionPrefab = null;
    public GameObject sprite = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        StartCoroutine(AnimateMovement());
    }

    private IEnumerator AnimateMovement()
    {
        while (!dead)
        {
            inputVelocity = Vector3.zero;
            inputVelocity.x = 0.0f;
            inputVelocity.y = -2.0f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            inputVelocity.x = Mathf.Clamp(Random.Range(-1.0f, 1.0f) * moveSpeed, -5, 5);
            inputVelocity.y = -1.0f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity = Vector3.SmoothDamp(velocity, inputVelocity, ref accel, 0.5f);
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        string tag = coll.gameObject.tag;
        Debug.Log(tag);
        if (tag == "Asteroid" || tag == "Player")
        {
            TakeDamage(health);
        }
        else if (tag == "Player")
        {
            TakeDamage(health);
        }else if(tag == "PlayerBullet")
        {
            TakeDamage(health);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        string tag = other.tag;
        Debug.Log(tag);
        if (tag == "Asteroid")
        {
            TakeDamage(health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (dead) { return; }

        health -= damage;
        if (health <= 0)
        {
            inputVelocity = Vector3.zero;
            health = 0;
            dead = true;

            StopAllCoroutines();
            StartCoroutine(AnimateDeath());
            this.GetComponent<Collider2D>().enabled = false;
        }
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    private IEnumerator AnimateDeath()
    {
        // Spawn an explosion
        var explosion = GameObject.Instantiate(
            explosionPrefab, transform.position, Quaternion.identity
        );
        GameObject.Destroy(explosion, 2.5f);

        // Wait for a few seconds
        Time.timeScale = 0.75f;
        yield return new WaitForSeconds(0.25f);
        sprite.SetActive(false);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.75f);

        // Destroy the enemy's ship
        GameObject.Destroy(this.gameObject);
    }
}
