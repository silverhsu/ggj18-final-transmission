using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    public const int ASTEROID_DAMAGE = 6;
    public const int ENEMY_BULLET_DAMAGE = 1;

    private float moveSpeed = 5f;
    private Vector3 prevPos = Vector3.zero;
    private Vector3 inputVelocity = Vector3.zero;
    private Vector3 accel = Vector3.zero;
    
    public int maxHealth = 6;
    public int health = 6;
    public bool dead = false;
    public Slider healthSlider = null;
    public GameObject playerExplosionPrefab = null;
    public GameObject playerSprite = null;
    public Rigidbody2D rb = null;

    public float fireWeaponTimer = 0f;
    public float fireWeaponThreshold = 0.25f;

    public GameObject playerBullet;

    private TestTransmissionScript transmissionScript;

    //public GameObject playerShipAnchor;
    //private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        transmissionScript = GameObject.FindObjectOfType<TestTransmissionScript>();

        //rb = GetComponent<Rigidbody2D>();
        prevPos = this.transform.position;

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
        if (!dead)
        {
            inputVelocity = Vector3.zero;
            inputVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            inputVelocity.y = Input.GetAxis("Vertical") * moveSpeed;

            // FIXME: listen for button instead of key
            fireWeaponTimer = Mathf.Max(fireWeaponTimer - Time.deltaTime, 0.0f);
            if (Input.GetKey(KeyCode.Space) && fireWeaponTimer <= 0)
            {
                FireWeapon();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Rotate ship as player moves left and right
            Vector3 curPos = this.transform.position;
            Vector3 tempDelta = prevPos - curPos;
            playerSprite.transform.rotation = Quaternion.Euler(0f, 0f, 180f * tempDelta.x);
            prevPos = curPos;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity = Vector3.SmoothDamp(velocity, inputVelocity, ref accel, 0.002f);
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
        else if (tag == "EnemyShip")
        {
            TakeDamage(health);
        }
        else if (tag == "EnemyBullet")
        {
            TakeDamage(ENEMY_BULLET_DAMAGE);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        string tag = other.tag;
        //Debug.Log(tag);
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
            inputVelocity = Vector3.zero;
            health = 0;
            dead = true;

            transmissionScript.triggerTransmission();
            

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
        //Time.timeScale = 0.25f;
        Time.timeScale = 0.05f;
        yield return new WaitForSeconds(0.95f);
        playerSprite.SetActive(false);
        yield return new WaitForSeconds(0.95f);
        Time.timeScale = 1.0f;

        // Destroy the player's ship
        GameObject.Destroy(this.gameObject);
    }

    private void FireWeapon()
    {
        Instantiate(playerBullet, this.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        fireWeaponTimer = fireWeaponThreshold;
    }
}
