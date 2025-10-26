using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    [SerializeField] GameManager gameManager;
    private void Update()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        TimeText.text = "Time: "+((int)gameManager.currentTime).ToString();
        ScoreText.text = "Score: "+ gameManager.points.ToString();
    }
    
    
}
