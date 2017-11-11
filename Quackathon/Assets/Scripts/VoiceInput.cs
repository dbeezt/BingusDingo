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
        float[] data = new float[512];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 512;
    }

    public float Remap()
    {
        return (loudness - lowerLimit) / (upperLimit - lowerLimit);
    }

    // Update is called once per frame
    void FixedUpdate () {
        loudness = Mathf.Lerp(loudness, (GetAveragedVolume() * sensitivity), 0.1f);
        if (loudness < lowerLimit)
        {
            playing = false;
            reMapVal = 0;
        } 
        else if (loudness >= upperLimit)
        {
            reMapVal = 1;
        }
        else
        {
            playing = true;
            reMapVal = Remap();
        }
    }
}
