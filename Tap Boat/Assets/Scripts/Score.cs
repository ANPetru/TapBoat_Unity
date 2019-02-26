using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public static float SCORE = 1;
    public static float SCOREVELOCITY = 0.1f; 

    private GameManager gm;
    private Text textScore;
    private float score;

	void Start () {
        textScore = GetComponent<Text>();
        gm = GameManager.instance;
        Restart();

    }


    private IEnumerator AddScore()
    {
        while (!gm.gameOver) {
            score += SCORE;
            textScore.text = "" + score;
            yield return new WaitForSeconds(SCOREVELOCITY);
        }

        
    }

    public void Restart()
    {
        gm.SetHighScore(score);
        score = 0;
        StartCoroutine(AddScore());
    }
}
