using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfTrapLeft : MonoBehaviour {

    GameObject trap;
    public float speed = 10f;
    public Vector2 moveRange;
    public float timer = 5f;
    public float timerWait = 0f;

    private float position;
    private Rigidbody2D rb2d;
    private bool moveLeft = true;

	void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    // Use this for initialization
	void Start () {
        trap = GetComponent<GameObject>();
        updateVelocity(-speed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        position = transform.localPosition.x;

        if (moveLeft && position <= moveRange.y)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                moveLeft = false;
                updateVelocity(speed);
                timer = 5f;
            }

            
        }
        else if (!moveLeft && position >= moveRange.x)
        {
            // wait for x seconds

            
                moveLeft = true;
                updateVelocity(-speed);
            
        }
    }

    //-300 -> -426


    private void updateVelocity(float velocity)
    {
        rb2d.velocity = new Vector2(velocity, 0);
    }
}
