using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveWin : MonoBehaviour
{
    [SerializeField]
    PhoneAndTutorialManager phone;
    void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && phone.isGiven)
        {
            Debug.Log("g");
            SceneManager.LoadScene(0);
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        }
    }
}
