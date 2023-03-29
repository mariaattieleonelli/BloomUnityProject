using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance { get; private set; }

    public GameObject loadingPanel;
    public Image progressBar;

    public void Awake()
    {
        //si hay otra instancia, destruir la extra
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void LoadLevel(string sceneToLoad)
    {
        StartCoroutine(LoadSceneAsyc(sceneToLoad));
    }

    IEnumerator LoadSceneAsyc(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingPanel.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressBar.fillAmount = progress;

            yield return null;
        }
    }

    public void IsNewSaveFile(bool var)
    {
        //Si se inicia una nueva partida
        PlayerStats.isNewGame = var;
    }
}
