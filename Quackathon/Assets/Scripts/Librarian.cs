using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum States
{
    at_chair,
    chasing,
    at_player,
    returning
}

public class Librarian : MonoBehaviour
{
    AIPath aipath;
    GameObject chair;
    GameObject player;
    bool canChase;
    int catches = 0;
    public bool reached = false;
    public States state = States.returning;
    public float threshold = 2f;
    public float volume_weight = 1.2f;
    public float distance_weight = 0.5f;
    public float distance_from_sound = 5f;

    private GameObject soundLocation;
    private GameObject endScreen;
    private Text endText;
    private textZoom warning;
    private Text warningText;

    float running = 150f;
    float walking = 75f;

    void Awake()
    {
        var warningGO = GameObject.Find("warningTxt");
        warning = warningGO.GetComponent<textZoom>();
        warningText = warningGO.GetComponent<Text>();
        soundLocation = new GameObject();
        soundLocation.AddComponent<Transform>();
        this.aipath = this.GetComponent<AIPath>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(this.player.GetComponents<VoiceInput>()[0].ToString());
        Debug.Log(this.player.GetComponents<VoiceInput>()[1].ToString());
        this.chair = GameObject.FindGameObjectWithTag("Chair");
        endScreen = GameObject.Find("lose");
        endText = GameObject.Find("loseTxt").GetComponent<Text>();
        endScreen.SetActive(false);
    }

    void Start()
    {
        this.canChase = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.reached = this.aipath.TargetReached;
        if ((Input.GetKeyDown(KeyCode.Space) || activated()) && canChase)
        {
            state = States.chasing;
            soundLocation.transform.position = player.transform.position;
            this.aipath.target = this.soundLocation.transform;
            this.aipath.speed = this.running + (this.catches * 25f);
        }

        switch (state)
        {
            case States.at_chair:
                Debug.Log("Reached Chair");
                this.aipath.target = this.player.transform;
                this.aipath.speed = 0;
                break;
            case States.at_player:
                Debug.Log("Reached Player");
                StartCoroutine(waitThenReturn());
                break;
            case States.chasing:
                if (this.aipath.TargetReached)
                {
                    state = States.at_player;
                }
                break;
            case States.returning:
                this.aipath.target = this.chair.transform;
                this.aipath.speed = this.walking;
                if (this.aipath.TargetReached)
                {
                    state = States.at_chair;
                }
                break;
        }
    }

    private bool activated()
    {
        float volume = this.player.GetComponents<VoiceInput>()[0].reMapVal + this.player.GetComponents<VoiceInput>()[1].reMapVal;
        volume /= 2;
        float distance = (this.transform.position - this.player.transform.position).magnitude;
        float decision = (volume_weight * volume) / (distance_weight * distance);
        decision *= 1000f;
        Debug.Log("Decision: " + decision + "\t Threshold: " + threshold);
        return decision >= threshold;
    }

    IEnumerator waitThenReturn()
    {
        this.aipath.target = this.chair.transform;
        this.aipath.speed = this.walking;
        this.canChase = false;
        yield return new WaitForSeconds(2);
        this.canChase = true;
        state = States.returning;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player" && activated()) || (collision.gameObject.tag == "Player" && Vector2.Distance(collision.gameObject.transform.position,soundLocation.transform.position) < distance_from_sound))
        {
            if (catches == 2)
            {
                endScreen.SetActive(true);
                Time.timeScale = 0;
                warningText.text = "";
            }
            warningText.text += "I";
            warning.active = true;
            catches++;
        }
    }
}
