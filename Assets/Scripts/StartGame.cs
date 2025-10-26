using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] public AudioSource ClickSound;
    public Animator anim;
    public void OnButton()
    {
        ClickSound.Play();
        anim.SetBool("transition", true);
        StartCoroutine(Wait());
        
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Tutorial");
    }
}
