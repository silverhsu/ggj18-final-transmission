using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLargeExplosionScript : MonoBehaviour {

    public int frameNum = 1;

    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;
    public Sprite frame5;
    public Sprite frame6;
    public Sprite frame7;
    public Sprite frame8;
    public Sprite frame9;

    private SpriteRenderer sprRend;

    private float frameTimer = 0f;
    private float frameThreshold = 0.125f;

    // Use this for initialization
    void Start () {
        sprRend = this.GetComponent<SpriteRenderer>();
        updateFrame();

        this.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 340f));

        Destroy(this.gameObject, frameThreshold * 8f);
	}
	
	// Update is called once per frame
	void Update () {
        frameTimer += Time.deltaTime;

        if(frameTimer > frameThreshold)
        {
            frameTimer -= frameThreshold;
            frameNum++;

            updateFrame();
        }

	}


    private void updateFrame()
    {
        switch (frameNum)
        {
            case 1:
                sprRend.sprite = frame1;
                break;
            case 2:
                sprRend.sprite = frame2;
                break;
            case 3:
                sprRend.sprite = frame3;
                break;
            case 4:
                sprRend.sprite = frame4;
                break;
            case 5:
                sprRend.sprite = frame5;
                break;
            case 6:
                sprRend.sprite = frame6;
                break;
            case 7:
                sprRend.sprite = frame7;
                break;
            case 8:
                sprRend.sprite = frame8;
                break;
            case 9:
                sprRend.sprite = frame9;
                break;
            default:
                sprRend.sprite = frame1;
                break;
        }
    }
}
