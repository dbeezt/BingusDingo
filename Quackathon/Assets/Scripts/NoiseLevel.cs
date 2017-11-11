using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseLevel : MonoBehaviour {
    public int micID;
    public GameObject player;

    private VoiceInput input;
    private Slider slider;


    private void Awake()
    {
        input = GameObject.Find("Player").GetComponents<VoiceInput>()[micID];
        slider = GetComponent<Slider>();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //slider.value = Mathf.Lerp(slider.value, input.reMapVal, 0.25f);
        slider.value = input.reMapVal;	
	}
}
