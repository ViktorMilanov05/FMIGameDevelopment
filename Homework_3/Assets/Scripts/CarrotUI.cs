using System;
using UnityEngine;
using UnityEngine.UI;

public class CarrotUI : MonoBehaviour
{
    [SerializeField]
    private Image[] carrots;

    public static Action OnAllCarrotsCollected;

    private int collectedCarrots = 0;

    private Color emptyColor = new Color(1f, 1f, 1f, 0.59f);
    private Color fullColor = new Color(1f, 1f, 1f, 1f);

    void OnEnable()
    {
        CarrotPickup.OnCarrotCollected += AddCarrot;    
    }
    void OnDisable()
    {
        CarrotPickup.OnCarrotCollected -= AddCarrot;    
    }

    void Start()
    {
        for (int i = 0; i < carrots.Length; i++)
        {
            carrots[i].color = emptyColor;
        }
    }

    public void AddCarrot()
    {
        if (collectedCarrots >= carrots.Length) { return; }
        carrots[collectedCarrots].color = fullColor;
        collectedCarrots++;

        if(collectedCarrots >= carrots.Length)
        {
            OnAllCarrotsCollected?.Invoke();
        }
    }
}
