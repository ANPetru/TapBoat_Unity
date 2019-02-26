using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private const string HIGHSCORE = "highscore";


    public PathSpawner pathSpawner;
    public Score score;

    public static GameManager instance = null;
    public  bool  gameOver = false;
    public  bool spawnLeft = true;


    void Awake()
    {
        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void GameOver(PlayerMovement player)
    {
        gameOver = true;
        player.Respawn();
        Restart();
    }

    private void Restart()
    {
        spawnLeft = true;
        pathSpawner.Restart();
        score.Restart();
        gameOver = false;
    }

    public void SpawnPath()
    {
        pathSpawner.SpawnPath(true);
    }

    public float GetHighScore()
    {
        return PlayerPrefs.GetFloat(HIGHSCORE, 0);
    }

    public void SetHighScore(float newScore)
    {
        if (newScore > GetHighScore())
        {
            PlayerPrefs.SetFloat(HIGHSCORE, newScore);
        }
    }

    public int GetPathPosition(GameObject currentPath)
    {
        return pathSpawner.GetPathPosition(currentPath);
    }
}
