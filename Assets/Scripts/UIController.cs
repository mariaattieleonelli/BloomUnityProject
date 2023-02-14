using UnityEngine.SceneManagement;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inventoryWindow;
    public GameObject inventoryBlur;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void PlayGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenInventory()
    {
        Time.timeScale = 0;
        inventoryWindow.SetActive(true);
        inventoryBlur.SetActive(true);
    }

    public void CloseInventory()
    {
        Time.timeScale = 1;
        inventoryWindow.SetActive(false);
        inventoryBlur.SetActive(false);
    }
}
