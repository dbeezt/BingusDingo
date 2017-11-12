using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public float time = 0f;

    private Text timerText;

    void Awake()
    {
        timerText = GetComponent<Text>();    
    }

	// Update is called once per frame
	void FixedUpdate () {
        time += Time.deltaTime;
        timerText.text = time.ToString("00.00");
    }
}
