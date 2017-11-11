using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoiceInput : MonoBehaviour {

    // Use this for initialization
    public AudioSource audioSource;
    public string micName;
    public float sensitivity;
    public float lowerLimit;
    public float upperLimit;
    public float reMapVal;
    public string[] devices;

    private float loudness = 0;
    private bool playing = false;
    void Start () {
        devices = Microphone.devices;
        audioSource.clip = Microphone.Start(micName, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public float Remap()
    {
        return (loudness - lowerLimit) / (upperLimit - lowerLimit);
    }

    // Update is called once per frame
    void FixedUpdate () {
        loudness = GetAveragedVolume() * sensitivity;
        if (loudness < lowerLimit) {
            playing = false;
            reMapVal = 0;
        } else {
            playing = true;
            reMapVal = Remap();
        }
        if (loudness >= upperLimit) {
            loudness =1.4F;
            reMapVal = Remap();
        }
    }
}
