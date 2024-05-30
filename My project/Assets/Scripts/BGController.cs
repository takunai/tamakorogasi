using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector3 _relativePosition;

    void Start()
    {
        _mainCamera = Camera.main;
        _relativePosition = _mainCamera.transform.position - transform.position;
    }

    void Update()
    {
        transform.position = _mainCamera.transform.position - _relativePosition;
    }
}