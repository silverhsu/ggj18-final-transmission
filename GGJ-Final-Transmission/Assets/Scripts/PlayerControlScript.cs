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
    public GameObject playerExplosionPrefab = null;
    public GameObject playerSprite = null;

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
        if (!dead)
        {
            inputVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            inputVelocity.y = Input.GetAxis("Vertical") * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity = Vector3.SmoothDamp(velocity, inputVelocity, ref accel, 0.1f);
        rb.velocity = velocity;

        //rb.velocity = inputVelocity;
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

            StartCoroutine(AnimateDeath());
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
            playerExplosionPrefab, transform.position, Quaternion.identity
        );
        GameObject.Destroy(explosion, 2.5f);

        // Slow time and wait for a few seconds
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.25f);
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        Time.timeScale = 1.0f;

        // Destroy the player's ship
        GameObject.Destroy(this.gameObject);
    }
}
