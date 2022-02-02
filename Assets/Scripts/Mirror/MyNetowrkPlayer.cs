using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyNetowrkPlayer : NetworkBehaviour
{
    [SerializeField] UnityEngine.GameObject gameManager;

    ReusableStuff reusable;
    CardPositions cardPositions;
    UnityEngine.GameObject PlayerArea;

    #region Card Positions
    [Space(100)]
    [Header("Card Positions")][SerializeField] Vector3 card1 = new Vector3();
    [SerializeField] Vector3 card2 = new Vector3();
    [SerializeField] Vector3 card3 = new Vector3();
    [SerializeField] Vector3 card4 = new Vector3();
    [SerializeField] Vector3 card5 = new Vector3();
    [Space(100)]
    #endregion

    //Vector3 cardScale = new Vector3(0.03f, 0.11f, 0);

    [SerializeField] Vector3 topLeft = new Vector3(), bottomRight = new Vector3();

    [SyncVar(hook = nameof(NameChanged))] string userNameString;

    [SerializeField] TMP_Text playerName;
    UnityEngine.GameObject card;
    int playerNumber;

    [SyncVar(hook = nameof(HandChanged))] [SerializeField] List<Values> playerHand;

    [Server]//Prevents clients from executing this method
    public void SetDisplayName(string newDisplayName, int playerNumber, TMP_Text userName)
    {
        this.playerNumber = playerNumber;
        userNameString = userName.text;
        playerName.text = userNameString;
        PlayerNamePosition(playerNumber);
    }

    private void FixedUpdate()
    {
        switch (playerNumber)
        {
            case 1:
                this.transform.position = new Vector3(-7, 4, 0);
                break;
            case 2:
                this.transform.position = new Vector3(-7, -4, 0);
                break;
        }   
    }

    void PlayerNamePosition(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                this.transform.position = new Vector3(-7, 4, 0);
                break;
            case 2:
                this.transform.position = new Vector3(-7, -4, 0);
                break;
        }
    }

    
    public void StartTheGame()
    {
        PlayerNames();

        GetComponentMethod();

        InitialHand();

        switch (playerNumber)
        {
            case 1:
                for (int i = 0; i < playerHand.Count; i++)
                {
                    playerHand[i].card.transform.position = new Vector3(0, 4, 0);
                }
                break;
            case 2:
                for (int i = 0; i < playerHand.Count; i++)
                {
                    playerHand[i].card.transform.position = new Vector3(0, -4, 0);
                }
                break;
        }
    }

    private void PlayerNames()
    {
        Debug.Log("yes");
    }

    private void GetComponentMethod()
    {
        reusable = gameManager.GetComponent<ReusableStuff>();
        PlayerArea = UnityEngine.GameObject.FindGameObjectWithTag("PlayerArea1");
        cardPositions = GetComponent<CardPositions>();
    }
    
    private void InitialHand()
    {
        
        int random;
        for (int i = 0; i < 5; i++)
        {
            random = Mathf.RoundToInt(Random.Range(0, 51));
            playerHand.Add(reusable.cards[random]);
        }

        //DistributeCards(playerHand);
        for (int i = 0; i < 5; i++)
        {
            card = Instantiate(playerHand[i].card, reusable.positions[i], Quaternion.identity);
            card.transform.position = reusable.positions[i];
            Debug.Log(reusable.positions[i]);
            card.transform.SetParent(PlayerArea.transform, false);
            //cardPositions.CreatePosition(reusable.positions[i]);
        }
    }

    //void DistributeCards(List<Values> hand)
    //{
    //    // Taken from: https://forum.unity.com/threads/distribute-sprites-evenly-on-scene.290020/
    //    // Get width of screen
    //    //Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector3.zero);
    //    //Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector3.one);
    //    Vector3 pos = Camera.main.ViewportToWorldPoint(Vector3.one * 0.5f);

    //    // Calculate spacing
    //    float space = (Mathf.Abs(topLeft.x) + Mathf.Abs(bottomRight.x)) / (hand.Count);

    //    // Calculate position of first card
    //    pos.x -= space * (hand.Count - 1) / 2f;

    //    for (int i = 0; i < hand.Count; i++)
    //    {
    //        hand[i].card.transform.position = pos;

    //        hand[i].card.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10;

    //        pos.x += space;
    //    }
    //}

    private void HandChanged(List<Values> oldHand, List<Values> newHand)
    {
        //DistributeCards(newHand);
    }

    private void NameChanged(string oldText, string newText)
    {
        playerName.text = newText;
        PlayerNamePosition(playerNumber);
    }

    #region Old code
    //ReusableStuff reusable;

    //[Header("Player 1")]
    //[SerializeField] List<Values> player1Hand;

    //[Header("Player 2")]
    //[SerializeField] List<Values> player2Hand;

    //// Start is called before the first frame update
    //public void StartTheGame()
    //{
    //    GetComponentMethod();

    //    InitialHand();
    //}

    //private void Update()
    //{
    //    //DistributeCards(player1Hand);
    //    //DistributeCards(player2Hand);
    //}

    //public void GetComponentMethod()
    //{
    //    reusable = GetComponent<ReusableStuff>();
    //}

    //public void InitialHand()
    //{
    //    int random;
    //    for (int i = 0; i < 2; i++)
    //    {
    //        random = Mathf.RoundToInt(Random.Range(0, 51));
    //        player1Hand.Add(reusable.cards[random]);
    //        Instantiate(player1Hand[i].card);

    //        random = Mathf.RoundToInt(Random.Range(0, 51));
    //        player2Hand.Add(reusable.cards[random]);
    //        Instantiate(player2Hand[i].card);
    //    }
    //}

    //public void DistributeCards(List<Values> hand)
    //{
    //    // Taken from: https://forum.unity.com/threads/distribute-sprites-evenly-on-scene.290020/
    //    // Get width of screen
    //    Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector3.zero);
    //    Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector3.one);
    //    Vector3 pos = Camera.main.ViewportToWorldPoint(Vector3.one * 0.5f);

    //    // Calculate spacing
    //    float space = (Mathf.Abs(rightTop.x) + Mathf.Abs(leftBottom.x)) / (hand.Count);

    //    // Calculate position of first card
    //    pos.x -= space * (hand.Count - 1) / 2f;

    //    for (int i = 0; i < hand.Count; i++)
    //    {
    //        hand[i].card.transform.position = pos;

    //        hand[i].card.GetComponentInChildren<SpriteRenderer>().sortingOrder = 10;

    //        pos.x += space;
    //    }
    //}

    //private Vector3 Midpoint(float x1, float x2, float y1, float y2)
    //{
    //    float xGroup, yGroup;

    //    xGroup = (x1 + x2) / 2;
    //    yGroup = (y1 + y2) / 2;

    //    return new Vector3(xGroup, yGroup, 0);
    //}
    #endregion

}
