using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isStarted;
    private bool gameOver;
    private GameObject m_ShipPrefab;
    private GameObject m_UFOPrefab;
    private GameObject m_AsteroidBigPrefab;
    private PlayerController m_PlayerController;
    private Text m_GameScore;
    private Text m_WaveNumber;
    public List<Image> Lives;
    [SerializeField] private int score;
    private int livesNumber;
    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyNumber;
    [SerializeField] private int enemyPlusPerWave = 1;
    public GameObject MainMenu;
    public GameObject EndGameMenu;
    public GameObject GameUI;


    public int Score { get => score; set => score = value; }
    public Text GameScore { get => m_GameScore != null ? m_GameScore : m_GameScore = GameObject.Find("GameUI/ScoreText").GetComponent<Text>(); }
    public Text WaveNumber { get => m_WaveNumber != null ? m_WaveNumber : m_WaveNumber = GameObject.Find("GameUI/WaveNumberText").GetComponent<Text>(); }
    public GameObject UFOPrefab { get => m_UFOPrefab != null ? m_UFOPrefab : m_UFOPrefab = Resources.Load<GameObject>("UFO"); }
    public GameObject AsteroidBigPrefab { get => m_AsteroidBigPrefab != null ? m_AsteroidBigPrefab : m_AsteroidBigPrefab = Resources.Load<GameObject>("AsteroidLarge"); }
    public GameObject ShipPrefab { get => m_ShipPrefab != null ? m_ShipPrefab : m_ShipPrefab = Resources.Load<GameObject>("Player"); }
    public PlayerController PlayerController { get => m_PlayerController != null ? m_PlayerController : m_PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); }

    private void OnEnable()
    {
        MainMenu.SetActive(true);
        EndGameMenu.SetActive(false);
        GameUI.SetActive(false);
    }

    private void Start()
    {
        StartNewWave();
    }

    private void Update()
    {
        
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1 && !gameOver)
        {
            wave++;
            StartNewWave();
            PlayerController.IsInvincible = true;
        }
        GameMenuContrl();
        GetGameInfo();
    }

    private void GameMenuContrl()
    {
        if (!isStarted && Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
            MainMenu.SetActive(false);
            GameUI.SetActive(true);
            Instantiate(ShipPrefab, new Vector3(0, 0, 0), transform.rotation);
        }

        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
    }

    private void GetGameInfo()
    {
        if (isStarted)
        {


            GameScore.text = $"{Score}";
            WaveNumber.text = $"{wave}";


            livesNumber = (int)PlayerController.Hp;

            for (int i = 0; i < Lives.Count; i++)
            {
                Lives[i].color = i < livesNumber ? Lives[i].color = Color.white : Lives[i].color = Color.black;
            }

            if (livesNumber <= 0)
            {
                EndGame();
            }
        }
    }

    private void StartNewWave()
    {

        enemyNumber = wave + enemyPlusPerWave;
        var asteroidsNumber = Random.Range(1, enemyNumber - 1);
        var ufoNumber = enemyNumber - asteroidsNumber;
        for (int i = 0; i < asteroidsNumber; i++)
        {
            Instantiate(AsteroidBigPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0), transform.rotation);
        }
        for (int i = 0; i < ufoNumber; i++)
        {
            Instantiate(UFOPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-7, 7), 0), transform.rotation);
        }
    }
    private void EndGame()
    {
        EndGameMenu.SetActive(true);
        gameOver = true;
    }

    private void Reset()
    {
        EndGameMenu.SetActive(false);
        gameOver = false;
        wave = 1;
        score = 0;
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(item);
        }
        StartNewWave();
        Instantiate(ShipPrefab, new Vector3(0, 0, 0), transform.rotation);

    }

}
