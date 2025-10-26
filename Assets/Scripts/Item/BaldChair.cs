using System.Collections.Generic;
using UnityEngine;

public class BaldChair : MonoBehaviour
{
    public (WigColor, WigType) expectedWig;

    [SerializeField] List<Sprite> balds;
    [SerializeField] List<WigItem> wigs;
    [SerializeField] List<AnimalItem> animals;
    [SerializeField] List<RawHairItem> rawHair;
    [SerializeField] List<Sprite> bucket;
    [SerializeField] SpriteRenderer baldRenderer;
    [SerializeField] SpriteRenderer wigRenderer;

    [SerializeField] GameObject wigDialog;
    [SerializeField] SpriteRenderer dialogWigRenderer;
    [SerializeField] SpriteRenderer dialogAnimalRenderer;
    [SerializeField] SpriteRenderer dialogBucketRenderer;
    [SerializeField] SpriteRenderer dialogRawHair;
    [SerializeField] SpriteRenderer dialogUncoloredHair;
    private (WigColor, WigType) generateRandomWig()
    {
        int wigColorRdn = Random.Range(0, 3);
        int wigTypeRdn = Random.Range(0, 3);

        return ((WigColor)wigColorRdn, (WigType)wigTypeRdn);

    }



    private void generateRandomBald() 
    {
        baldRenderer.sprite = balds[Random.Range(0,balds.Count)];
    }

    float timeForNext;
    public bool isBusy = false;
    bool isGoingOut = false;
    float timeToGoOut=2;
    private void Start()
    {
        timeForNext = Random.Range(2f, 4f);
    }

    private void newOrder()
    {
        generateRandomBald();
        expectedWig = generateRandomWig();
        setDialog();
    }

    private void setDialog() 
    {
        wigDialog.SetActive(true);

        dialogWigRenderer.color = wigColors[(int)expectedWig.Item1];
        dialogWigRenderer.sprite = wigs[(int)expectedWig.Item2].sprite;
        
        dialogAnimalRenderer.sprite = animals[(int)expectedWig.Item2].sprite;

        dialogBucketRenderer.sprite = bucket[(int)expectedWig.Item1];

        dialogRawHair.sprite = rawHair[(int)expectedWig.Item1].sprite;

        dialogUncoloredHair.sprite = wigs[(int)expectedWig.Item2].sprite;




    }

    private void Update()
    {
        if (isGoingOut) {
            timeToGoOut -= Time.deltaTime;
            if (timeToGoOut < 0) {

                isGoingOut = false;
                timeToGoOut = 2;

                wigRenderer.sprite = null;
                wigRenderer.color = Color.white;
                baldRenderer.sprite = null;
            }
        }

        if (!isBusy|| isGoingOut) 
        {
            timeForNext -= Time.deltaTime;
            if (timeForNext < 0) { 
                newOrder();
                isBusy = true;
                timeForNext = Random.Range(2f, 4f);
            }
        }
    }
    Color32[] wigColors = { new Color32(241, 99, 85, 255), new Color32(146, 88, 62, 255), new Color32(241, 196, 85, 255), new Color32(255, 255, 255, 255) };

    public delegate void _OnWigDelivered(int points);
    public static event _OnWigDelivered OnWigDelivered;

    public void giveWig(WigItem wig) 
    {
        wigRenderer.sprite = wig.wigForBald;
        wigRenderer.color = wigColors[(int)wig.color];
        isGoingOut = true;
        isBusy = false;
        wigDialog.SetActive(false);


        

        if (OnWigDelivered != null) { OnWigDelivered(isWigCorrect(wig) ? 100 : -100); }
    
    }

    private bool isWigCorrect(WigItem wig)
    {

        print("WIG:");
        print(wig.color);
        print(wig.wigType);
        print("EXPECTED:");
        print(expectedWig.Item1);
        print(expectedWig.Item2);
        return wig.color == expectedWig.Item1 && wig.wigType == expectedWig.Item2;
    }

}
