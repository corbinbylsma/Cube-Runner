using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public GameObject settingsObject;
    public GameObject statsObject;

    public AudioClip youLose;

    static public float score = 0;        // Score gained every 1/4 second
    static public int bonus = 0;
    static public int lives = 5;        // Number of times you can get hit before you lose
    private float scoreTimer = .25f;

    GUIText textbox;
    GUIText statbox;

    private void Start()
    {
        textbox = settingsObject.GetComponent<GUIText>();
        statbox = statsObject.GetComponent<GUIText>();
        DontDestroyOnLoad(settingsObject);
        DontDestroyOnLoad(statsObject);
    }

    void Update () {
        scoreTimer -= Time.deltaTime;
		if (lives > 0 && scoreTimer <= 0)
        {
            score += 10 + bonus;
            scoreTimer = .25f;
        }
        textbox.text = "Score: " + score + "\nLives: " + lives;
        statbox.text = "Speed: " + PlayerMovement.speed / 2 + "\nJump: " + PlayerMovement.jumpVel * 5 + "\nTime: " + Mathf.Round(Time.time) + "s";
	}
}
