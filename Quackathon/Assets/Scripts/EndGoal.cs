using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGoal : MonoBehaviour {

    private GameObject endScreen;
    private Text endText;

    void Awake()
    {
        Time.timeScale = 1;
        endScreen = GameObject.Find("win");
        endText = GameObject.Find("winTxt").GetComponent<Text>();
        endScreen.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
