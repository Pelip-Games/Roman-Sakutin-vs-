using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField, Min(0)] private int _mainSceneIndex = 1;
    [SerializeField] private AboutUsPanel _aboutUsPanel;
    
    public void OnStartClick()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }

    public void OnAboutClick()
    {
        Instantiate(_aboutUsPanel, transform);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}