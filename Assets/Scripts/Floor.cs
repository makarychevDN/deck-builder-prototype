using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private List<Encounter> encounters;

    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TMP_Text endGameScreenText;
    [SerializeField] private GameObject victoryEncounterScreen;
    [SerializeField] private Button restartButton;

    [SerializeField] private Encounter currentEncounter;
    [SerializeField] private int currentEncounterIndex;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        playerInput.OnLose.AddListener(() => ShowEndGameScreen("Поражение"));

        currentEncounterIndex = -1;
        StartNextEncounter();
    }

    private void StartNextEncounter()
    {
        if(currentEncounter != null)
        {
            currentEncounter.Uninit();
            currentEncounter.gameObject.SetActive(false);
            currentEncounter.EnemyInput.OnLose.RemoveListener(FinishEncounter);
        }

        currentEncounterIndex++;

        if(currentEncounterIndex >= encounters.Count)
        {
            ShowEndGameScreen("Победа!");
            return;
        }

        currentEncounter = encounters[currentEncounterIndex];

        currentEncounter.gameObject.SetActive(true);
        currentEncounter.Init(playerInput);
        currentEncounter.EnemyInput.OnLose.AddListener(FinishEncounter);
    }

    private void ShowEndGameScreen(string screenText)
    {
        endGameScreen.SetActive(true);
        endGameScreenText.text = screenText;
    }

    private async void FinishEncounter()
    {
        victoryEncounterScreen.SetActive(true);
        await Task.Delay(3000);
        victoryEncounterScreen.SetActive(false);
        StartNextEncounter();
    }

    private void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            playerInput.SetAllCardsToDrawPile();
        }
    }
}
