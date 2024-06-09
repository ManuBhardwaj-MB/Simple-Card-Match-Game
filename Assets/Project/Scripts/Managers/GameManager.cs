using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LevelHolder allLevels;
    [SerializeField] private bool debugLoggingIsOn;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject gameOverPanel;
    
    [SerializeField] private GameState currentGameState;
    private void Awake() => Instance ??= this;

    public void PlayNow()
    {
        SwitchPanel(GameState.Gameplay);
        GameController.Instance.LoadNewLevel(allLevels.GetLevel(SettingsManager.Instance.GetDifficultySettings()));
    }

    public void SwitchPanel(GameState state)
    {
        DisableAllPanels();
        switch (state)
        {
            case GameState.MainMenu:
                mainMenuPanel.gameObject.SetActive(true);
                break;
            case GameState.Gameplay:
                gamePlayPanel.gameObject.SetActive(true);
                break;
            case GameState.GameOver:
                gameOverPanel.gameObject.SetActive(true);
                break;
            case GameState.GameWon:
                gameWonPanel.gameObject.SetActive(true);
                break;
        }
    }
    
    private void DisableAllPanels()
    {
        mainMenuPanel.gameObject.SetActive(false);
        gamePlayPanel.gameObject.SetActive(false);
        gameWonPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
    }


    public void Log(string value)
    {
        if(!debugLoggingIsOn || !Debug.unityLogger.logEnabled) return;
        Debug.Log(value);
    }
}
