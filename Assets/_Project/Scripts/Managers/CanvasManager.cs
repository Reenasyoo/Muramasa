using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Systems;
using Muramasa.Utilities;
using UnityEngine;

public class CanvasManager : Singleton<MonoBehaviour>
{
    [Header("Canvases")]
    [SerializeField] private Canvas _dialogCanvas = null;
    [SerializeField] private Canvas _gameCanvas = null;

    private void Awake()
    {
        _dialogCanvas.gameObject.SetActive(false);
    }
}
