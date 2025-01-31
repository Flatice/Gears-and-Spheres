using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct BallSelection
    {
        public GameObject[] ballPrefab;
        public int[] weights;

        public BallSelection(GameObject[] ballPrefab, int[] weights)
        {
            this.weights = weights;
            this.ballPrefab = ballPrefab;
        }

        public void Validate()
        {
            if (Array.Exists(weights, w => w < 0))
                Debug.LogError("Weight can't be negative");
        }
    }
    public BallSelection ballSelection;

    private Vector2 spawnPosition;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float initialRepeatRate = 2f, repeatRate;

    private void OnValidate()
    {
        ballSelection.Validate();
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        
        for (int j = 1; j <= (int)ballSelection.weights.Sum(); j++)
        {
            BallToSpawnTest(j);
        }

        StartCoroutine(SpawnBall(startDelay, initialRepeatRate));
    }

    IEnumerator SpawnBall(float startDelay, float initialRepeatRate)
    {
        yield return new WaitForSeconds(startDelay);
        
        while (!GameManager.Instance.gameOver)
        {
            while (GameManager.Instance.isPaused)
                yield return null;

            GameObject ballToSpawn = BallToSpawn();  // ABSTRACTION
            if (ballToSpawn == null) Debug.LogError("Something went wrong while selecting a ball to spawn");

            Instantiate(ballToSpawn, spawnPosition, ballToSpawn.transform.rotation);

            // TODO: increase spawn frequency with time (maybe based on score?)
            repeatRate = initialRepeatRate;

            yield return new WaitForSeconds(repeatRate);
        }

    }

    private GameObject BallToSpawn()
    {
        int randomSelect = Random.Range(1, ballSelection.weights.Sum() + 1);

        int cumSum = 0;
        for (int i = 0; i <= ballSelection.ballPrefab.Length; i++)
        {
            cumSum += ballSelection.weights[i];

            if (randomSelect <= cumSum)
                return ballSelection.ballPrefab[i];
        }

        return null;
    }

    private void BallToSpawnTest(int j)
    {
        int randomSelect = j;

        int cumSum = 0;
        for (int i = 0; i <= ballSelection.ballPrefab.Length; i++)
        {
            cumSum += ballSelection.weights[i];

            if (randomSelect <= cumSum)
            {
                Debug.Log($"r = {randomSelect}, ball = {ballSelection.ballPrefab[i].name}, weight = {ballSelection.weights[i]}");
                return;
            }            
        }

        Debug.Log("Something went wrong while selecting a ball to spawn. r = " + randomSelect);
    }
}
