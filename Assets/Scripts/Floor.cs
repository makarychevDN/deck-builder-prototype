using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject victoryEncounterScreen;
    [SerializeField] private Button restartButton;
    [SerializeField] private List<Encounter> encounters;
    [SerializeField] private Encounter currentEncounter;
    [SerializeField] private int currentEncounterIndex;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        playerInput.OnLose.AddListener(ShowDeathScreen);

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
            return;
        }

        currentEncounter = encounters[currentEncounterIndex];

        currentEncounter.gameObject.SetActive(true);
        currentEncounter.Init(playerInput);
        currentEncounter.EnemyInput.OnLose.AddListener(FinishEncounter);
    }

    private void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    private void FinishEncounter()
    {
        playerInput.SetAllCardsToDrawPile();
        victoryEncounterScreen.SetActive(true);
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
