using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefab;

    private Vector2 spawnPosition;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float initialRepeatRate = 2f, repeatRate;


    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;

        StartCoroutine(SpawnBall(startDelay, initialRepeatRate));
    }

    IEnumerator SpawnBall(float startDelay, float initialRepeatRate)
    {
        yield return new WaitForSeconds(startDelay);

        while (!GameManager.Instance.gameOver)
        {
            int ballIndex = Random.Range(0, ballPrefab.Length);

            Instantiate(ballPrefab[ballIndex], spawnPosition, ballPrefab[ballIndex].transform.rotation);

            // TODO: increase spawn frequency with time (maybe based on score?)
            repeatRate = initialRepeatRate;

            yield return new WaitForSeconds(repeatRate);
        }

    }
}
