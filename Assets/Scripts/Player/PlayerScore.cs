using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    [HideInInspector]
    public int score = 0;
    public Text playerScoreText;
    public Text panelScoreText;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	public void UpdateScore (int damage) {

        score += damage;
        playerScoreText.text = score.ToString();
        panelScoreText.text = score.ToString();
    }
}
