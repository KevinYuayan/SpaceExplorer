using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// File Name: GameController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Controller that controls the game
/// Revision History:
/// </summary>
public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject enemy_1;
    public GameObject enemy_2, orb;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    public GameObject highScore;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    // private instance variables

    private int _numberOfEnemies_1;
    private List<GameObject> _enemies_1;

    private int _numberOfEnemies_2;
    private List<GameObject> _enemies_2;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    // public properties

    public int NumberOfEnemies_1
    {
        get
        {
            return _numberOfEnemies_1;
        }
        set
        {
            for (int enemyNum = _numberOfEnemies_1; enemyNum < value; enemyNum++)
            {
                _enemies_1.Add(Instantiate(enemy_1));
            }
            _numberOfEnemies_1 = value;
        }
    }
    public int NumberOfEnemies_2
    {
        get
        {
            return _numberOfEnemies_2;
        }
        set
        {
            for (int enemyNum = _numberOfEnemies_2; enemyNum < value; enemyNum++)
            {
                _enemies_2.Add(Instantiate(enemy_2));
            }
            _numberOfEnemies_2 = value;
        }
    }
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            if(_lives < 1)
            {
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
        }
    }
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            if (_score != 0)
            {
                if (_score % 200 == 0)
                {
                    NumberOfEnemies_1 += 1;
                }
                if (_score % 500 == 0)
                {
                    NumberOfEnemies_2 += 1;
                }
            }

            if (highScore.GetComponent<HighScore>().score < _score)
            {
                highScore.GetComponent<HighScore>().score = _score;
            }
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        highScore = GameObject.Find("HighScore");

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
    }


    private void SceneConfiguration()
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                highScoreLabel.enabled = false;
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                break;
            case "Main":
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;
        }

        Lives = 5;
        Score = 0;

        // creates an empty container (list) of type GameObject
        _enemies_1 = new List<GameObject>();
        _enemies_2 = new List<GameObject>();

        Instantiate(orb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Event Handlers
    public void OnStartButtonClick()
    {
        DontDestroyOnLoad(highScore);
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
