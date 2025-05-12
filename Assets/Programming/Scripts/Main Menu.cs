using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void MenuButton(string name)
   {
     SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
   }
    public void Quit()
    {
        Application.Quit();
    } 
}
