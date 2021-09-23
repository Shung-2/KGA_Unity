using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public int pipeNum = 5; // 파이프 개수
    public float spawnRate = 1.5f; // 파이프 생성 시간
    public float pipeMin = 0.85f;  // 파이프 랜덤 생성 위치
    public float pipeMax = 2.0f;
    Vector2 objectPoolPosition = new Vector2(-3, 0);
    GameObject[] pipes;
    int currentPipe = 0;
    float lastSpawnTime;

    void Start()
    {
        pipes = new GameObject[pipeNum];

        for (int i = 0; i < pipeNum; ++i)
        {
            pipes[i] = Instantiate(pipePrefab, objectPoolPosition, Quaternion.identity);
            // Instantiate == new 동적할당과 비슷한 개념.
        }
    }

    void Update()
    {
        lastSpawnTime += Time.deltaTime;

        if (lastSpawnTime >= spawnRate)
        {
            lastSpawnTime = 0;

            float spawnYpos = Random.Range(pipeMin, pipeMax);
            float spawnXpos = 2.0f;

            pipes[currentPipe].transform.position = new Vector2(spawnXpos, spawnYpos);

            currentPipe++;
            if (currentPipe >= pipeNum) currentPipe = 0;
        }
    }
}
