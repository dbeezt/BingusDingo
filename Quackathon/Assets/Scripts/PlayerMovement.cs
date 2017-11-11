using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float rotationSpeed, moveSpeed;
    public float rotate, push;

    private Rigidbody2D body2D;
    private VoiceInput[] inputs;

    private void Awake () {
        body2D = GetComponent<Rigidbody2D>();
        inputs = GetComponents<VoiceInput>();
    }

    void Start () {
		
	}
	
	void FixedUpdate () {
        rotate= inputs[0].reMapVal;
        push = inputs[1].reMapVal;

        if (push != 0 && rotate != 0) {
            body2D.AddRelativeForce(new Vector2(push * moveSpeed, 0), ForceMode2D.Force);
        } else if (rotate != 0) {
            body2D.AddTorque(rotate * moveSpeed);
        }
        else if (push != 0) {
            body2D.AddTorque(-push * moveSpeed);
        }
    }
}
