using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{

    //https://tunetank.com/track/3308-extreme

    public static int doubleDamageCount;
    public static int grenadesCount;
    public static int minesCount;
    public static int score;

    TextMeshProUGUI doubleDamageText;
    TextMeshProUGUI grenadesText;
    TextMeshProUGUI minesText;
    TextMeshProUGUI scoreText;

    [SerializeField] public GameObject granade;
    [SerializeField] public GameObject mine;
    GameObject gameplayUI;
    GameObject menuUI;
    GameObject enemySpawner1;
    GameObject enemySpawner2;
    GameObject startTextButton;
    GameObject restartTextButton;
    public GameObject[] items;
    public GameObject[] weaponsAmmo;

    PlayerControls playerControlsScript;
    PlayerAnimation playerAnimationScript;

    [SerializeField] public int akBullets;
    [SerializeField] public int minigunBullets;
    [SerializeField] public int shotgunBullets;
    [SerializeField] public int sniperBullets;

    [HideInInspector] public int itemIndex = 0;

    private void Start()
    {
        score = 0;
        doubleDamageCount = 0;
        grenadesCount = 0;
        minesCount = 0;
        Time.timeScale = 0;
        playerControlsScript = GameObject.Find("Helmet").GetComponent<PlayerControls>();
        playerAnimationScript = GameObject.Find("Helmet").GetComponent<PlayerAnimation>();
        startTextButton = GameObject.Find("Start Text Button");
        restartTextButton = GameObject.Find("Restart Text Button");
        menuUI = GameObject.Find("Menu UI");
        gameplayUI = GameObject.Find("Gameplay UI");
        enemySpawner1 = GameObject.Find("Enemy Spawner 1");
        enemySpawner2 = GameObject.Find("Enemy Spawner 2");
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        doubleDamageText = GameObject.Find("Double Damage Text").GetComponent<TextMeshProUGUI>();
        grenadesText = GameObject.Find("Grenade Text").GetComponent<TextMeshProUGUI>();
        minesText = GameObject.Find("Mines Text").GetComponent<TextMeshProUGUI>();
        restartTextButton.SetActive(false);
        gameplayUI.SetActive(false);
        enemySpawner1.SetActive(false);
        enemySpawner2.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        doubleDamageText.text = "Double Damage = " + doubleDamageCount;
        grenadesText.text = "Grenades = " + grenadesCount;
        minesText.text = "Mines = " + minesCount;
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        PlayerControls.theFirstStart = true;
        PlayerControls.isPaused = false;
        enemySpawner1.SetActive(true);
        enemySpawner2.SetActive(true);
        playerControlsScript.enabled = true;
        playerAnimationScript.enabled = true;
        gameplayUI.SetActive(true);
        restartTextButton.SetActive(true);
        menuUI.SetActive(false);
        Destroy(startTextButton);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
