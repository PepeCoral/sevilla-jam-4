using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    void Start()
    {
        ScoreHolder scoreHolder  = FindAnyObjectByType<ScoreHolder>();
        scoreText.text = "Score: "+ scoreHolder.points.ToString();
    }

}
