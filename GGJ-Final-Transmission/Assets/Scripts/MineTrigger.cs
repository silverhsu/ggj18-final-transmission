using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
    public Mine mine = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;
        if (tag == "Player")
        {
            mine.TriggerMine(other.transform.position);
        }
    }
}
