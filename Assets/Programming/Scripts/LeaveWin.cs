using UnityEngine;

public class LeaveWin : MonoBehaviour
{
    PhoneAndTutorialManager phone;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && phone.isGiven)
        {
            Application.Quit();
        }
    }
}
