using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{ 
    public GameObject menu;
    public void Options()
    {

        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }else
        {
            menu.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
