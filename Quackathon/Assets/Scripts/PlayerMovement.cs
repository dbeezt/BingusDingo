using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float move1, move2;

    private Rigidbody2D body2D;
    private VoiceInput[] inputs;

    private void Awake () {
        body2D = GetComponent<Rigidbody2D>();
        inputs = GetComponents<VoiceInput>();
    }

    void Start () {
		
	}
	
	void FixedUpdate () {
        move1 = inputs[0].reMapVal;
        move2 = inputs[1].reMapVal;

        if (move1 != 0) {
            // var up = new Vector2(transform.up.x, transform.up.y);
            body2D.AddTorque(move1 * moveSpeed);
        }
        if (move2 != 0) {
            body2D.AddTorque(-move2 * moveSpeed);
        }

        if (move1 != 0 && move2 != 0) {
            body2D.AddRelativeForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
        }
    }
}
