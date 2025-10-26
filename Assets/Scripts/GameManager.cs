using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float totalTime = 120;
    public float currentTime;

    public delegate void _OnGameEnd(int points);
    public static event _OnGameEnd OnGameEnd;

    public int points = 0;

    

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
            if(OnGameEnd!=null) OnGameEnd(points);
        }
    }
}
