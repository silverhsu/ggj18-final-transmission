using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public SpriteRenderer renderer = null;
    public Rigidbody2D rb = null;
    public Sprite[] asteroidSprites = new Sprite[6];
    public float moveSpeed = 5;

    private Vector2 targetPos = Vector3.zero;
    private bool triggered = false;
    
    // Use this for initialization
    void Start()
    {
        renderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        rb.angularVelocity = Random.Range(-30.0f, 30.0f);
        rb.velocity = Vector2.down * Random.Range(3.0f, 4.0f);
    }

    public void TriggerMine(Vector2 pos)
    {
        if (!triggered)
        {
            triggered = true;
            targetPos = pos;

            Vector2 dir = (pos - rb.position + Random.insideUnitCircle).normalized;
            rb.velocity = dir * moveSpeed;
        }
    }
}
