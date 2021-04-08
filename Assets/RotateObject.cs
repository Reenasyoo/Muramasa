using DG.Tweening;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _rotationSpeed = 20f;

    private void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
