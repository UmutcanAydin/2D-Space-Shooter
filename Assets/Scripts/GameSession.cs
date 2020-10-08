using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int health;

    private void Awake()
    {
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        health = FindObjectOfType<Player>().getHealth();
    }

    public int getScore()
    {
        return score;
    }

    public int getHealth()
    {
        return health;
    }

    public void AddToScore(int value)
    {
        score += value;
    }

    public void DecreaseHealth(int value)
    {
        health -= value;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
