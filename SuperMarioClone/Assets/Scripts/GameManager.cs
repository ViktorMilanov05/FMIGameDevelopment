using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Level { get; private set; }
    public int Lives { get; private set; }
    void Awake()
    {
        if(Instance != null)
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

        LoadLevel(1);
    }
    void LoadLevel(int level)
    {
        Level = level;
        SceneManager.LoadScene($"{level}");
    }

    public void ResetLevel(float delay = 0)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    private void ResetLevel()
    {
        Lives--;

        if(Lives > 0)
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
        //to do: make game over screen
        NewGame();
    }

    //if I make another levels
    public void NextLevel()
    {
        LoadLevel(Level + 1);
    }
}
