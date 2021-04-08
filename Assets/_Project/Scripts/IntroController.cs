using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider = null;

    private void Awake()
    {
        _loadingSlider.gameObject.SetActive(false);
    }
    
    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync("Main"));
    }
             
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1);
        
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            _loadingSlider.value = asyncLoad.progress;
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
