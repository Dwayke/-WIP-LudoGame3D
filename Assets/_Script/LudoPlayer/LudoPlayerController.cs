using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;

public class LudoPlayerController : NetworkBehaviour
{
    #region VARS
    public Camera currentCamera;
    private LudoControls _controls;
    #endregion
    #region ENGINE
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!currentCamera)
        {
            currentCamera = Camera.main;
        }
        _controls = new LudoControls();
        _controls.Gameplay.Click.performed += OnClick;
        _controls.Gameplay.Enable();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        _controls.Gameplay.Click.performed -= OnClick;
        _controls.Gameplay.Disable();
    }
    #endregion
    #region LOCAL METHODS
    private void OnClick(InputAction.CallbackContext context)
    {
        if (LudoManagers.Instance.GameStateManager.gameState != EGameState.DiskSelection) return;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit info, 100,LayerMask.GetMask("Disk")))
        {
            if (info.collider.gameObject.TryGetComponent<BaseDisk>(out BaseDisk disk))
            {
                if (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6)
                {
                    if (disk.color == LudoManagers.Instance.TurnManager.currentTurn&&disk.diskState != EDiskState.Home)
                    {
                        Debug.Log(disk);
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
