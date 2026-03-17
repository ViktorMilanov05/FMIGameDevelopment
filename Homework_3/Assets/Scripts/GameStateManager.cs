using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject losePanel;

    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += LoseGame;
        CarrotUI.OnAllCarrotsCollected += WinGame;
    }
    void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= LoseGame;
        CarrotUI.OnAllCarrotsCollected -= WinGame;
    }
    public void WinGame()
    {
        winPanel.SetActive(true);
    }
    public void LoseGame()
    {
        losePanel.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("ProcedurialGeneratedLevels");
    }
}
