using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textZoom : MonoBehaviour {

    public float speed = 3f;
    public float timeOnScreen = 1f;
    public bool active = true;

    private Vector3 scale;

    void Start()
    {
        scale = new Vector3(0, 0, 0);    
    }

    // Update is called once per frame
    void Update () {
	    if (active)
        {
            active = false;
            StartCoroutine(grow());
        }    	
	}

    IEnumerator grow()
    {
        float timer = 0;
        while (transform.localScale.x < 1)
        {
            timer += Time.deltaTime;
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * speed;
            yield return null;
        }

        yield return new WaitForSeconds(timeOnScreen);

        while (transform.localScale.x > 0)
        {
            timer += Time.deltaTime;
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * speed;
            yield return null;
        }

        transform.localScale = new Vector3(0, 0, 0);
    }
}
