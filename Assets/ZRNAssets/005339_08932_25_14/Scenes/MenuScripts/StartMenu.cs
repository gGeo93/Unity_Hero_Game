using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour,IMenuOptions
{
    [SerializeField] GameObject startingPanel;
    [SerializeField] GameObject howToPlayPanel;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Instructions(string guidelines)
    {
        startingPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        howToPlayPanel.GetComponentInChildren<Text>().text = guidelines;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        howToPlayPanel.SetActive(false);
        startingPanel.SetActive(true);
    }
}
