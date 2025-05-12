using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    void Start()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
    public void MenuButton(string name)
   {
     SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
   }
    public void Quit()
    {
        Application.Quit();
    } 
}
