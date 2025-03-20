using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class LudoPlayerController : MonoBehaviour
{
    #region VARS
    public Camera currentCamera;
    private LudoControls _controls;

    public event Action<BaseDisk> OnPieceSelected;
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
        if (LudoManagers.Instance.GameStateManager.gameState != EGameState.PieceSelection) return;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit info, 100))
        {
            if (info.collider.gameObject.TryGetComponent<BaseDisk>(out BaseDisk disk))
            {
                if (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6)
                {
                    if (disk.color == LudoManagers.Instance.TurnManager.currentTurn&&disk.pieceState != EPieceState.Home)
                    {
                        Debug.Log(disk);
                        OnPieceSelected.Invoke(disk);
                    }
                    else
                    {
                        Debug.Log("Select a piece of yours");
                    }
                }
                else
                {
                    if (disk.color == LudoManagers.Instance.TurnManager.currentTurn&& disk.pieceState == EPieceState.Free)
                    {
                        Debug.Log(disk);
                        OnPieceSelected.Invoke(disk);
                    }
                    else
                    {
                        Debug.Log("Select a piece of yours");
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
