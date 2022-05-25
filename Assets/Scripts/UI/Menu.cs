using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private void OnEnable()
    {
        _spawner.LastEnemyDied += OpenWinPanel;
        _player.Die += OpenLosePanel;
    }

    private void OnDisable()
    {
        _spawner.LastEnemyDied -= OpenWinPanel;
        _player.Die -= OpenLosePanel;
    }

    public void OpenMenuPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenuPanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
    private void OpenWinPanel()
    {
        _winPanel.SetActive(true);
    }
    private void OpenLosePanel()
    {
        _losePanel.SetActive(false);
    }
}
