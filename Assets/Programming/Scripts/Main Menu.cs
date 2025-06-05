using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        playerInput.SwitchCurrentActionMap("UI");
    }
    public void MenuButton(int name)
   {
     SceneManager.LoadScene(name);
     SceneManager.SetActiveScene(SceneManager.GetSceneAt(name));
   }
    public void Quit()
    {
        Application.Quit();
    } 
}
