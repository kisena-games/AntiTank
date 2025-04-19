using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraMode : MonoBehaviour
{
    [SerializeField] private GameObject _sniperCamera;

    public static CameraMode CurrentMode { get; private set; }
    public static Action SwitchToDefaultModeAction;

    private void OnEnable()
    {
        InputManager.SwitchCameraModeAction += Switch;
    }

    private void OnDisable()
    {
        InputManager.SwitchCameraModeAction -= Switch;
    }

    private void Awake()
    {
        CurrentMode = CameraMode.Default;
    }

    private void Switch()
    {
        if (CurrentMode == CameraMode.Default)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            
            _sniperCamera.SetActive(true);
            CurrentMode = CameraMode.Sniper;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _sniperCamera.SetActive(false);
            CurrentMode = CameraMode.Default;

            SwitchToDefaultModeAction?.Invoke();
        }
    }
}

public enum CameraMode
{
    Default,
    Sniper
}
