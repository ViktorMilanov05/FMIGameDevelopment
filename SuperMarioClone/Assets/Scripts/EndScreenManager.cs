using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public void PlayAgain()
    {
        GameManager.Instance.NewGame();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
