using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Librarian : MonoBehaviour {

    public Vector2 velocity;
    public Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 current = body.position;
        Vector2 target = new Vector2(5, 5);

        body.position = Vector2.MoveTowards(current,target,0.1f);
    }

}
