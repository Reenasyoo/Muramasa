using Muramasa.Movement;
using Muramasa.Utilities;
using UnityEngine;

public class AxisInput : MonoBehaviour
{
    #region Properties
    private static float HorizontalInput => Input.GetAxis(GLOBALS.HORIZONTAL_STRING);
    private static float VerticalInput => Input.GetAxis(GLOBALS.VERTICAL_STRING);

    #endregion

    #region Fields

    private IInputVector _inputVector = null;
    private static Vector2 GetAxisInput() => new Vector2(HorizontalInput, VerticalInput);
    
    #endregion
    
    private void Awake()
    {
        _inputVector = GetComponent<IInputVector>();
    }
    
    private void Update()
    {
        _inputVector?.GetInputVector(GetAxisInput());
    }
}
