using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public float timerScoreSec;
    public float timerScoreMin;

    public float minSpawnTimeBullet;
    public float maxSpawnTimeBullet;
    public int countdownBeforeGettingHarder = 3;

    private float timerToSpawnBullet;

    public TextMeshProUGUI timerText;

    public Vector2 screenBounds;

    public GameObject panelDeath;

    public bool isStarted;
    public GameObject start, command;
    public GameObject Bullet1, Bullet2;

    public static GameManager Instance;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Music");
        screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SpawnBullet(Random.Range(1, 5), Bullet1);
    }

    // Update is called once per frame
    void Update()
    {
        timerScoreSec += Time.deltaTime;
        string scoreSec = "";
        if (timerScoreSec <= 9.5)
        {
            scoreSec = $"0{timerScoreSec.ToString("F0")}";
        }
        else
        {
            scoreSec = $"{timerScoreSec.ToString("F0")}";
        }

        if (timerScoreSec >= 59.5)
        {
            timerScoreMin++;
            timerScoreSec = 0;
        }

        timerText.text = $"{timerScoreMin} : {scoreSec}";


        timerToSpawnBullet -= Time.deltaTime;
        if (timerToSpawnBullet <= 0)
        {
            int whichBullet = Random.Range(0, 2);
            if (whichBullet == 0)
            {
                SpawnBullet(Random.Range(1, 5), Bullet1);
            }
            else
            {
                SpawnBullet(Random.Range(1, 5), Bullet2);
            }
        }

        timerText.text = $"{timerScoreMin} : {scoreSec}";

        if (!isStarted)
        {
            Time.timeScale = 0f;
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                start.SetActive(false);
                command.SetActive(false);
                isStarted = true;
            }
        }
    }

    public void SpawnBullet(int side, GameObject whichBullet)
    {
        timerToSpawnBullet = Random.Range(minSpawnTimeBullet, maxSpawnTimeBullet);
        countdownBeforeGettingHarder--;
        if (countdownBeforeGettingHarder <= 0)
        {
            countdownBeforeGettingHarder = 3;
            minSpawnTimeBullet -= .1f;
            maxSpawnTimeBullet -= .1f;
        }
        GameObject goInstatiated;
        switch (side)
        {
            case 1:
                goInstatiated = Instantiate(whichBullet,
                    new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 0), Quaternion.identity);
                goInstatiated.GetComponent<IPooledObject>().OnObjectSpawn(Vector3.down);
                break;
            case 2:
                goInstatiated = Instantiate(whichBullet,
                    new Vector3(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y), 0), Quaternion.identity);
                goInstatiated.GetComponent<IPooledObject>().OnObjectSpawn(Vector3.left);
                break;
            case 3:
                goInstatiated = Instantiate(whichBullet,
                    new Vector3(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y, 0),
                    Quaternion.identity);
                goInstatiated.GetComponent<IPooledObject>().OnObjectSpawn(Vector3.up);
                break;
            case 4:
                goInstatiated = Instantiate(whichBullet,
                    new Vector3(-screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y), 0),
                    Quaternion.identity);
                goInstatiated.GetComponent<IPooledObject>().OnObjectSpawn(Vector3.right);
                break;
        }
    }

    public void DeathPlayer()
    {
        Time.timeScale = 0f;
        panelDeath.SetActive(true);
    }
}