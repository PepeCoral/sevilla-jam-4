using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void onButton()
    {
        SceneManager.LoadScene("ScenaDani");
    }
}
