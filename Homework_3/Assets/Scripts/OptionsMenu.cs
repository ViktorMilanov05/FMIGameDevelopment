using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject optionsPanel;

    public void Back()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
