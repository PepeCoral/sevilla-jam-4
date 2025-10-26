using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float totalTime = 120;
    public float currentTime;


    public int points = 0;
    [SerializeField] GameObject scoreHolder;

    

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        BaldChair.OnWigDelivered += UpdatePoints;
    }
    private void OnDisable()
    {
        BaldChair.OnWigDelivered -= UpdatePoints;
        
    }


    void UpdatePoints(int points_)
    {
        points += points_;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0) {
            var go = Instantiate(scoreHolder);
            ScoreHolder sc = go.GetComponent<ScoreHolder>();
            sc.points = points;
            SceneManager.LoadScene("GameOver");
        }
    }
}
