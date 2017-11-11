using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float leftSpeed, rightSpeed;
    public float move1, move2;

    private Rigidbody2D body2D;

    private void Awake () {
        body2D = GetComponent<Rigidbody2D>();    
    }

    void Start () {
		
	}
	
	void FixedUpdate () {
        move1 = Input.GetAxis("move1");
        move2 = Input.GetAxis("move2");

        if (move1 != 0) {
            // var up = new Vector2(transform.up.x, transform.up.y);
            body2D.AddTorque(move1 * leftSpeed);
        }
        if (move2 != 0) {
            body2D.AddTorque(-move2 * rightSpeed);
        }

        if (move1 != 0 && move2 != 0) {
            body2D.AddRelativeForce(new Vector2(rightSpeed + leftSpeed, 0), ForceMode2D.Force);
        }
    }
}
