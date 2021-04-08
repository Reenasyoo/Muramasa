using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Systems;
using Muramasa.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<MonoBehaviour>
{
    [Header("Canvases")]
    [SerializeField] private Canvas _dialogCanvas = null;
    [SerializeField] private Canvas _gameCanvas = null;
    [SerializeField] private Slider _playerHealthSlider;
    [SerializeField] private TMP_Text _dialogText;

    [TextArea]
    [SerializeField] private string firstDialogText = "";
    
    [TextArea]
    [SerializeField] private string secondDialogText = "";
    [TextArea]
    [SerializeField] private string thirdDialogText = "";
    
    
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

    public void SetDialogText()
    {
        switch (GameManager.Instance.FirstPedInteractionCount)
        {
            case 0:
                _dialogText.text = firstDialogText;
                break;
            case 1 when GameManager.Instance.EnemyKilled:
                _dialogText.text = secondDialogText;
                break;
            default:
                _dialogText.text = thirdDialogText;
                break;
        }

        GameManager.Instance.FirstPedInteractionCount++;
    }
}
