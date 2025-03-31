using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [System.Serializable]
    public class BallSelection
    {
        public GameObject[] ballPrefab;
        public int[] weights1, weights2, weights3;
        public int[] scoreCutoff = new int[2];

        public void Validate()
        {
            List<string> errors = new List<string>();

            int[] weightsTotal = weights1.Concat(weights2).Concat(weights3).ToArray();
            if (Array.Exists(weightsTotal, w => w < 0))
                errors.Add("Weight can't be negative");

            int prefabCount = ballPrefab.Length;
            if (weights1.Length != prefabCount)
                errors.Add($"The number of weights in weights1 ({weights1.Length}) must be the same as the number of balls ({prefabCount})");
            if (weights2.Length != prefabCount)
                errors.Add($"The number of weights in weights2 ({weights2.Length}) must be the same as the number of balls ({prefabCount})");
            if (weights3.Length != prefabCount)
                errors.Add($"The number of weights in weights3 ({weights3.Length}) must be the same as the number of balls ({prefabCount})");
        
            if (errors.Count > 0)
                throw new ArgumentException("Error in initialization of balls weights\n" + string.Join("\n", errors));
        }
    }
    public BallSelection ballSelection;

    private Vector2 spawnPosition;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float initialRepeatRate = 2f, repeatRate;


    // Start is called before the first frame update
    void Start()
    {
        // Validate BallSelection to ensure teh correct initializaion of weights
        try
        {
            ballSelection.Validate();
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.Message);
            enabled = false;  // disabling the script prevents further execution and unexpected errors
            return;
        }

        spawnPosition = transform.position;

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
        // change the probability of spawning special balls when the score increases to add difficulty
        int[] weights;
        if (GameManager.Instance.score >= ballSelection.scoreCutoff[1]) weights = ballSelection.weights3;
        else if (GameManager.Instance.score >= ballSelection.scoreCutoff[0]) weights = ballSelection.weights2;
        else weights = ballSelection.weights1;

        int randomSelect = Random.Range(1, weights.Sum() + 1);

        int cumSum = 0;
        for (int i = 0; i < ballSelection.ballPrefab.Length; i++)
        {
            cumSum += weights[i];

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
            cumSum += ballSelection.weights1[i];

            if (randomSelect <= cumSum)
            {
                Debug.Log($"r = {randomSelect}, ball = {ballSelection.ballPrefab[i].name}, weight = {ballSelection.weights1[i]}");
                return;
            }            
        }

        Debug.Log("Something went wrong while selecting a ball to spawn. r = " + randomSelect);
    }
}
