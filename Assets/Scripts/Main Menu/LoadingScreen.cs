using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingPanel;
    public Image progressBar;

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


}
