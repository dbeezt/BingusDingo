using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Librarian : MonoBehaviour {

    AIPath aipath;
    GameObject chair;
    GameObject player;
    bool chase;

    float running = 150f;
    float walking = 75f;

	void Start () {
        this.aipath = this.GetComponent<AIPath>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.chair = GameObject.FindGameObjectWithTag("Chair");

        this.chase = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            this.chase = !this.chase;
        }
        if(this.chase)
        {
            this.aipath.target = this.player.transform;
            this.aipath.speed = this.running;
        } else
        {
            this.aipath.target = this.chair.transform;
            this.aipath.speed = this.walking;
        }
	}
}
