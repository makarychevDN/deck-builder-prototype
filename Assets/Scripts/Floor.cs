using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        playerInput.OnLose.AddListener(ShowDeathScreen);
    }

    private void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    private void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
