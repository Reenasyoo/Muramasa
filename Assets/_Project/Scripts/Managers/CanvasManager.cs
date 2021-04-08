using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Systems;
using Muramasa.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<MonoBehaviour>
{
    [Header("Canvases")]
    [SerializeField] private Canvas _dialogCanvas = null;
    [SerializeField] private Canvas _gameCanvas = null;
    [SerializeField] private Slider _playerHealthSlider;

    [TextArea]
    [SerializeField] private string firstDialogText = "";
    
    [TextArea]
    [SerializeField] private string secondDialogText = "";
    
    
    public delegate void GenericCallbackFunction();

    public delegate void OnPlayerHealthChangeDelegate(int health);
    public static event OnPlayerHealthChangeDelegate OnPlayerHealthChange;
    public static void ChangeHealth(int health)
    {
        Debug.Log("cc");
        OnPlayerHealthChange?.Invoke(health);
    }

    private void Awake()
    {
        _dialogCanvas.gameObject.SetActive(false);
        SetHealth(100);
    }

    private void Start()
    {
        OnPlayerHealthChange += this.SetHealth;
    }


    private void SetHealth(int health)
    {
        _playerHealthSlider.value = health / 100f;
    }
}
