using TMPro;
using UnityEngine;

public abstract class GameUIBase : MonoBehaviour
{
    protected TextMeshProUGUI label;
    private CameraMovement cameraMovement;

    protected virtual void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }
    protected virtual void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        cameraMovement.OnUndergroundChanged += OnUndergroundChanged;
        Subscribe();
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
    private void OnUndergroundChanged(bool isUndergound) 
    {
        label.color = isUndergound ? Color.white : Color.black;
    }
}
