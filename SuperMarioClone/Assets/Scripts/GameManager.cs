using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Level { get; private set; }
    public int Lives { get; private set; }
    public int Coins {  get; private set; }

    public event Action<int> OnLivesChanged;
    public event Action<int> OnCoinsChanged;
    public event Action<int> OnLevelChanged;

    private int maxLevel = 2;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        NewGame();   
    }
    void NewGame()
    {
        Lives = 3;
        Coins = 0;

        OnLivesChanged?.Invoke(Lives);
        OnCoinsChanged?.Invoke(Coins);
        LoadLevel(1);
    }
    void LoadLevel(int level)
    {
        Level = level;
        OnLevelChanged?.Invoke(level);
        SceneManager.LoadScene($"{level}");
    }

    public void ResetLevel(float delay = 0)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    private void ResetLevel()
    {
        Lives--;

        OnLivesChanged?.Invoke(Lives);

        if (Lives > 0)
        {
            LoadLevel(Level);
        }
        else
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        NewGame();
    }
    public void NextLevel()
    {
        if (Level == maxLevel)
        {
            NewGame();
        }
        else
        {
            LoadLevel(Level + 1);
        }
    }

    public void AddCoin()
    {
        Coins++;
        OnCoinsChanged?.Invoke(Coins);
    }

    public void AddLife()
    {
        Lives++;
        OnLivesChanged?.Invoke(Lives);
    }
}
