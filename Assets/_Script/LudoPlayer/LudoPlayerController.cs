using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Linq;

public class LudoPlayerController : MonoBehaviour
{
    #region VARS
    public Camera currentCamera;
    private LudoControls _controls;
    #endregion
    #region ENGINE
    private void OnEnable()
    {
        _controls = new LudoControls();
        _controls.Gameplay.Click.performed += OnClick;
        _controls.Gameplay.Enable();
    }
    void Start()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.main;
        }
    }
    private void OnDisable()
    {
        _controls.Gameplay.Click.performed -= OnClick;
        _controls.Gameplay.Disable();
    }
    #endregion
    #region LOCAL METHODS
    private void OnClick(InputAction.CallbackContext context)
    {
        if (LudoManagers.Instance.GameStateManager.gameState != EGameState.DiskSelection) return;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit info, 100))
        {
            if (info.collider.gameObject.TryGetComponent<BaseDisk>(out BaseDisk disk))
            {
                if (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6)
                {
                    if (disk.color == LudoManagers.Instance.TurnManager.currentTurn&&disk.diskState != EDiskState.Home)
                    {
                        Debug.Log(disk);
                        disk.diskState = EDiskState.Free;
                        LudoManagers.Instance.BoardManager.MoveDisk(disk);
                        LudoManagers.Instance.GameStateManager.gameState = EGameState.DiskMotion;
                    }
                    else
                    {
                        Debug.Log("Select a disk of yours");
                    }
                }
                else
                {
                    if (disk.color == LudoManagers.Instance.TurnManager.currentTurn && disk.diskState == EDiskState.Free && disk.pathIndex + LudoManagers.Instance.TurnManager.lastRolls[^1] <= 57)
                    {
                        Debug.Log(disk);
                        disk.diskState = EDiskState.Free;
                        LudoManagers.Instance.BoardManager.MoveDisk(disk);
                        LudoManagers.Instance.GameStateManager.gameState = EGameState.DiskMotion;
                    }
                    else if(disk.color != LudoManagers.Instance.TurnManager.currentTurn)
                    {
                        Debug.Log("Select a disk of yours");
                    }
                    else if(disk.diskState != EDiskState.Free)
                    {
                        Debug.Log("Select a free disk");
                    }
                    else if (disk.pathIndex + LudoManagers.Instance.TurnManager.lastRolls[^1] > 57)
                    {
                        Debug.Log("Roll value is too large, Select a valid disk");
                    }
                }

            }
            else
            {
                Debug.Log("Select a disk");
            }
        }
    }
    #endregion
}
