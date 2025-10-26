using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialScript : MonoBehaviour
{
    public Animator anim;

    public void startAnim()
    {
        StartCoroutine(Wait());
    }
    public IEnumerator Wait()
    {
        anim.SetBool("transition", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("ScenaDani");
    }
}


