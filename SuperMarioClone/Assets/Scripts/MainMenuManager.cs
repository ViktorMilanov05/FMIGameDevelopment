using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.NewGame();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
