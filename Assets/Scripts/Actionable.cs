using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{
    public UnityEvent Action;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TEST1");
        Action.Invoke();
    }
}
