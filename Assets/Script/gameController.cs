using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float punteggioTimer;
    float spawnTimer;
    public float spawnRate = 1.5f;
    public float spawnRateDecrement = 0.1f; // Decremento della spawn rate dopo ogni ciclo
    public float minSpawnRate = 0.5f; // Limite minimo per la spawn rate

    public GameObject ostacolo;
    public GameObject nemico1;
    public GameObject nemico2;
    public GameObject nemico3;
    public static bool gameover;

    public static int ostacoli_passati;
    public static int nemiciUccisi;

    private int cicloCount;

    private enum GameState
    {
        SpawningObstacles,
        SpawningEnemiesType1,
        SpawningEnemiesType2,
        SpawningEnemyType3
    }

    private GameState currentState;

    // Variabili per i nemici
    private int enemyRound;
    private int enemiesPerRound;
    private int totalEnemiesInRound;

    // Start is called before the first frame update
    void Start()
    {
        gameover = false;

        ostacoli_passati = 0;
        nemiciUccisi = 0;
        cicloCount = 0;

        currentState = GameState.SpawningObstacles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            switch (currentState)
            {
                case GameState.SpawningObstacles:
                    generaOstacoli();
                    punteggioTimer += Time.deltaTime;
                    if (punteggioTimer > 0.1f)
                    {
                        punteggioTimer = 0;
                        Punti.valorePunti++;
                    }
                    if (ostacoli_passati >= 15)
                    {
                        currentState = GameState.SpawningEnemiesType1;
                        enemyRound = 1;
                        enemiesPerRound = 1;
                        nemiciUccisi = 0;
                        totalEnemiesInRound = 0;
                    }
                    break;

                case GameState.SpawningEnemiesType1:
                    gestisciNemici(nemico1, 3, 2, GameState.SpawningEnemiesType2);
                    break;

                case GameState.SpawningEnemiesType2:
                    gestisciNemici(nemico2, 2, 1, GameState.SpawningEnemyType3);
                    break;

                case GameState.SpawningEnemyType3:
                    gestisciNemicoFinale();
                    break;
            }
        }
    }

    void generaOstacoli()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer -= spawnRate;
            Vector2 spawnPosOstacolo = new Vector2(4.25f, Random.Range(-2f, 3f));
            Instantiate(ostacolo, spawnPosOstacolo, Quaternion.identity);
            ostacoli_passati++;
        }
    }

    void gestisciNemici(GameObject nemico, int maxRounds, int incrementPerRound, GameState nextState)
    {
        if (enemyRound <= maxRounds)
        {
            if (totalEnemiesInRound == 0)
            {
                generaNemici(nemico, enemiesPerRound);
                totalEnemiesInRound = enemiesPerRound;
            }
            else if (nemiciUccisi >= totalEnemiesInRound)
            {
                enemiesPerRound += incrementPerRound;
                enemyRound++;
                nemiciUccisi = 0;
                totalEnemiesInRound = 0;
            }
        }
        else
        {
            // Passa allo stato successivo
            enemyRound = 1;
            enemiesPerRound = 1;
            nemiciUccisi = 0;
            totalEnemiesInRound = 0;
            currentState = nextState;
        }
    }

    void gestisciNemicoFinale()
    {
        if (enemyRound == 1)
        {
            enemiesPerRound = 1;
            generaNemici(nemico3, enemiesPerRound);
            totalEnemiesInRound = enemiesPerRound;
            enemyRound++;
        }
        else if (nemiciUccisi >= totalEnemiesInRound)
        {
            currentState = GameState.SpawningObstacles;
            ostacoli_passati = 0;
            nemiciUccisi = 0;
            cicloCount++;
            // Aumenta la velocità di spawn degli ostacoli
            spawnRate = Mathf.Max(minSpawnRate, spawnRate - spawnRateDecrement);
        }
    }

    void generaNemici(GameObject nemico, int quanti)
    {
        for (int i = 0; i < quanti; i++)
        {
            Vector2 spawnPosNemico = new Vector2(10f, Random.Range(-1.5f, 3.5f));
            Instantiate(nemico, spawnPosNemico, Quaternion.identity);
        }
    }
}
