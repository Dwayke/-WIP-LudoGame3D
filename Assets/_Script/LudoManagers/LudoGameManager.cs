using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class LudoGameManager : MonoBehaviour
{
    #region VARS
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rollForce = 5f;
    [SerializeField] private float _torqueForce = 500f;

    private GameObject _currentDice;
    private Rigidbody _diceRigidbody;
    private bool _isRolling = false;
    private bool _resultDetermined = false;

    private readonly Dictionary<Vector3, int> _diceFaceDirections = new()
    {
        { Vector3.up, 2 },
        { Vector3.down, 5 },
        { Vector3.right, 4 },
        { Vector3.left, 3 },
        { Vector3.forward, 1 },
        { Vector3.back, 6 }
    };

    [HideInInspector]public bool isGameStarted = false;
    public event Action OnGameStarted;
    public event Action<int> OnDiceRollComplete;
    #endregion
    #region ENGINE
    #endregion
    #region MEMBER METHODS
    public void RollTheDice()
    {
        if (_isRolling) return;
        if (_currentDice != null) Destroy(_currentDice);
        _currentDice = Instantiate(_dicePrefab, _spawnPoint.position, Random.rotation);
        if (!_currentDice.TryGetComponent<Rigidbody>(out _diceRigidbody))
        {
            Debug.LogError("Dice prefab must have a Rigidbody component!");
            return;
        }

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized;
        _diceRigidbody.AddForce(randomDirection * _rollForce, ForceMode.Impulse);
        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        _diceRigidbody.AddTorque(randomTorque * _torqueForce);

        _isRolling = true;
        _resultDetermined = false;
        StartCoroutine(CheckDiceStop());
    }
    #endregion
    #region LOCAL METHODS
    private IEnumerator CheckDiceStop()
    {
        yield return new WaitForSeconds(0.5f);

        while (_isRolling)
        {
            if (_diceRigidbody.linearVelocity.magnitude < 0.01f && _diceRigidbody.angularVelocity.magnitude < 0.01f)
            {
                yield return new WaitForSeconds(0.5f);

                if (_diceRigidbody.linearVelocity.magnitude < 0.01f && _diceRigidbody.angularVelocity.magnitude < 0.01f)
                {
                    _isRolling = false;
                    DetermineResult();
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DetermineResult()
    {
        if (_resultDetermined || _currentDice == null) return;
        _resultDetermined = true;

        float bestAlignment = -1f;
        Vector3 bestDirection = Vector3.zero;
        foreach (Vector3 faceDirection in _diceFaceDirections.Keys)
        {
            Vector3 worldFaceDirection = _currentDice.transform.TransformDirection(faceDirection);
            float alignment = Vector3.Dot(worldFaceDirection, Vector3.up);

            if (alignment > bestAlignment)
            {
                bestAlignment = alignment;
                bestDirection = faceDirection;
            }
        }

        int result = _diceFaceDirections[bestDirection];
        TryStartGame(result);
        Debug.Log("Dice Result: " + result);

        OnDiceRollComplete.Invoke(result);
    }
    private void TryStartGame(int result)
    {
        if (result == 6)
        {
            isGameStarted = true;
            OnGameStarted.Invoke();
            Debug.Log("Game Started!");
        }
    }
    #endregion
}
