using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    private void Awake()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            takeLife();
        }
        else
        {
            resetGame();
        }
    }

    private void resetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void takeLife()
    {
        playerLives--;
    }
}
