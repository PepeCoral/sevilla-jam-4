using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    public int scoreValue;
    void Start()
    {
        scoreText.text = "Score: "+ scoreValue.ToString();
    }

}
