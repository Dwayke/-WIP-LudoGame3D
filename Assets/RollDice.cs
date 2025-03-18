using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float rollForce = 5f;
    [SerializeField] private float torqueForce = 500f;

    private GameObject currentDice;
    private Rigidbody diceRigidbody;
    private bool isRolling = false;
    private bool resultDetermined = false;

    // Dictionary to store face direction vectors and their corresponding values
    private readonly Dictionary<Vector3, int> diceFaceDirections = new()
    {
        { Vector3.up, 1 },
        { Vector3.down, 6 },
        { Vector3.right, 5 },
        { Vector3.left, 2 },
        { Vector3.forward, 3 },
        { Vector3.back, 4 }
    };

    public void RollTheDice()
    {
        // If a dice is already rolling, don't spawn another
        if (isRolling)
            return;

        // If there's an existing dice, destroy it
        if (currentDice != null)
            Destroy(currentDice);

        // Create new dice at spawn point
        currentDice = Instantiate(dicePrefab, spawnPoint.position, Random.rotation);
        
        if (!currentDice.TryGetComponent<Rigidbody>(out diceRigidbody))
        {
            Debug.LogError("Dice prefab must have a Rigidbody component!");
            return;
        }

        // Apply random force and torque to roll the dice
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized;
        diceRigidbody.AddForce(randomDirection * rollForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        diceRigidbody.AddTorque(randomTorque * torqueForce);

        isRolling = true;
        resultDetermined = false;

        // Start checking for when the dice stops
        StartCoroutine(CheckDiceStop());
    }

    private IEnumerator CheckDiceStop()
    {
        // Wait for the dice to settle
        yield return new WaitForSeconds(0.5f);

        // Check if the dice has stopped moving
        while (isRolling)
        {
            // If dice is moving very slowly or has stopped
            if (diceRigidbody.linearVelocity.magnitude < 0.01f && diceRigidbody.angularVelocity.magnitude < 0.01f)
            {
                // Wait a bit to confirm it's really stopped
                yield return new WaitForSeconds(0.5f);

                // Check again to make sure it's still stopped
                if (diceRigidbody.linearVelocity.magnitude < 0.01f && diceRigidbody.angularVelocity.magnitude < 0.01f)
                {
                    isRolling = false;
                    DetermineResult();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DetermineResult()
    {
        if (resultDetermined || currentDice == null)
            return;

        resultDetermined = true;

        // Find which face is pointing up (closest to world up)
        float bestAlignment = -1f;
        Vector3 bestDirection = Vector3.zero;

        foreach (Vector3 faceDirection in diceFaceDirections.Keys)
        {
            // Transform local face direction to world space
            Vector3 worldFaceDirection = currentDice.transform.TransformDirection(faceDirection);
            float alignment = Vector3.Dot(worldFaceDirection, Vector3.up);

            if (alignment > bestAlignment)
            {
                bestAlignment = alignment;
                bestDirection = faceDirection;
            }
        }

        // Get the value based on the face direction
        int result = diceFaceDirections[bestDirection];
        Debug.Log("Dice Result: " + result);

        // You can trigger events or update UI with the result here
        // For example:
        // OnDiceRollComplete.Invoke(result);
    }
}
