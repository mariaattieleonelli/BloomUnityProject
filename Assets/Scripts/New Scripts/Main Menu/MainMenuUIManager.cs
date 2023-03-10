using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    //Carga el juego
    public void LoadGame()
    {
        SceneManager.LoadScene(sceneName: "Game");
    }
}
