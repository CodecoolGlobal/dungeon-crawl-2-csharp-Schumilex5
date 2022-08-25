
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManu : MonoBehaviour
{
    public void NewGamePRINT()
    {
        Debug.Log("NEWGAME");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }

    public void Continue()
    {
        //pass
    }

    public void Quit()
    {
        Application.Quit();   
    }
}
