using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    private float TimeValue = 100;
    private int ScoreValue;
    public GameObject GameOverPanel;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    private void FixedUpdate()
    {
        TimeValue -= Time.deltaTime;
        if (TimeValue < 0)
            SceneManager.LoadScene("StartMenu");
        UpdateUI();
    }
    public void UpdateUI()
    {
        TimeText.text = "Time: "+((int)TimeValue).ToString();
        ScoreText.text = "Score: "+ ScoreValue.ToString();
    }
    public void AddScoreUI(int score)
    {
        ScoreValue += score;
        UpdateUI();
    }
    public void UpdateUI(int Time,int Score)
    {
        TimeValue = Time;
        ScoreValue = Score;
        UpdateUI();
    }
}
