using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraMode : MonoBehaviour
{
    [SerializeField] private GameObject _head;
    [SerializeField] private MWHeadMovement _headMovement;
    [SerializeField] private GameObject _sniperCamera;

    private CameraMode _currentMode = CameraMode.Default;

    private void OnEnable()
    {
        InputManager.SwitchCameraModeAction += Switch;
    }

    private void OnDisable()
    {
        InputManager.SwitchCameraModeAction -= Switch;
    }

    private void Switch()
    {
        if (_currentMode == CameraMode.Default)
        {
            _head.SetActive(false);
            _headMovement.enabled = false;
            _sniperCamera.SetActive(true);
            _currentMode = CameraMode.Sniper;
        }
        else
        {
            _sniperCamera.SetActive(false);
            _headMovement.enabled = true;
            _head.SetActive(true);
            _currentMode = CameraMode.Default;
        }
    }
}

public enum CameraMode
{
    Default,
    Sniper
}
