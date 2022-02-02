using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class Card : MonoBehaviour
{
    bool ace = false;
    bool dace = false;
    int score = 0;
    int dealerScore = 0;
    public UnityEngine.GameObject Card1;
    public UnityEngine.GameObject PlayerArea;
    public UnityEngine.GameObject EnemyArea;
    Cards card;
    List<Cards> Deck = new List<Cards>();
    List<Cards> PlayingCards = new List<Cards>();
    List<Cards> playerHand = new List<Cards>();
    List<Cards> dealerHand = new List<Cards>();
 
    public Sprite[] imgs = new Sprite[52];
    string[] face = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
    string[] suit = { "hearts", "clubs", "diamonds", "spades" };
    public Text text;
    public Text text2;
    public Text winner;
    public Text loser;
    public Button button;
    public Button button2;

    List<UnityEngine.GameObject> cards = new List<UnityEngine.GameObject>();

    void Start()
    {
        winner.enabled = false;
        loser.enabled = false;




        cards.Add(Card1);
        for (int i = 0; i < suit.Length; i++)
        {
            for (int j = 0; j < face.Length; j++)
            {
                card = new Cards(face[j], suit[i]);
                Deck.Add(card);
                PlayingCards.Add(card);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            UnityEngine.GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
            int rnd = Random.Range(0, Deck.Count);
            string rndFace = Deck[rnd].Face;
            string rndSuit = Deck[rnd].Suit;
            string assetName = string.Format("{0}_of_{1}", rndFace, rndSuit);
            playerHand.Add(Deck[rnd]);

            foreach (Sprite cards in imgs)
            {
                Debug.Log(cards.name);
                if (cards.name == assetName)
                {
                    playerCard.GetComponent<Image>().sprite = cards;
                }
            }
            score += cardValues(rndFace);
            text.text = score.ToString();
        }
        
        for (int i = 0; i < 2; i++)
        {
            UnityEngine.GameObject enemyCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
            int rnd = Random.Range(0, Deck.Count);
            string rndFace = Deck[rnd].Face;
            string rndSuit = Deck[rnd].Suit;
            string assetName = string.Format("{0}_of_{1}", rndFace, rndSuit);
            dealerHand.Add(Deck[rnd]);

            foreach (Sprite cards in imgs)
            {
                Debug.Log(cards.name);
                if (cards.name == assetName)
                {
                    enemyCard.GetComponent<Image>().sprite = cards;
                }
            }
            dealerScore += cardValues(rndFace);
            text2.text = dealerScore.ToString();
        }
    }

    public void Stand()
    {
        while (dealerScore <= 16)
        {
            UnityEngine.GameObject enemyCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
            int rnd = Random.Range(0, Deck.Count);
            string rndFace = Deck[rnd].Face;
            string rndSuit = Deck[rnd].Suit;
            string assetName = string.Format("{0}_of_{1}", rndFace, rndSuit);
            dealerHand.Add(Deck[rnd]);

            foreach (Sprite cards in imgs)
            {
                Debug.Log(cards.name);
                if (cards.name == assetName)
                {
                    enemyCard.GetComponent<Image>().sprite = cards;
                }
            }
            dealerScore += cardValuesDealer(rndFace);
            text2.text = dealerScore.ToString();
        }
        WinLoss();
    }

    public void WinLoss()
    {
        if(score == 21)
        {
            winner.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        }
        else if ( score <= 21 && playerHand.Count == 5)
        {
            winner.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        }
        else if (score > 21)
        {
            loser.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        }
        else if((dealerScore == 21) || (dealerScore <= 21 && dealerHand.Count == 5))
        {
            loser.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        } 
        else if(score > dealerScore && score < 21)
        {
            winner.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        }
        else
        {
            loser.enabled = true;
            button.enabled = false;
            button2.enabled = false;
        }
    }
    public int cardValues(string face) 
    {
        int cardValue = 0;

        cardValue += CardValue(face);

        if (score >= 22 && ace == true) 
        {
            score -= 10;
            cardValue -= 10;
        }
        return cardValue;
    }
    public int cardValuesDealer(string face)
    {
        int cardValue = 0;
        cardValue += CardValue(face);

        if (score >= 22 && dace == true)
        {
            dealerScore -= 10;
            cardValue -= 10;
        }
        return cardValue;


    }

    int CardValue(string face)
    {
        int cardValue = 0;
        switch (face)
        {
            case "ace":
                dace = true;
                cardValue += 11;
                break;
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
                cardValue += int.Parse(face);
                break;
            case "jack":
            case "queen":
            case "king":
                cardValue += 10;
                break;
        }

        return cardValue;
    }
    public void OnClick()
    {
        UnityEngine.GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(PlayerArea.transform, false);
        int rnd = Random.Range(0, Deck.Count);
        string rndFace = Deck[rnd].Face;
        string rndSuit = Deck[rnd].Suit;
        playerHand.Add(Deck[rnd]);
        string assetName = string.Format("{0}_of_{1}", rndFace, rndSuit);

        foreach (Sprite cards in imgs)
        {
            Debug.Log(cards.name);
            if (cards.name == assetName)
            {
                playerCard.GetComponent<Image>().sprite = cards;
            }
        }
        score += cardValues(rndFace);
        text.text = score.ToString();
        if ((score == 21) || (score <= 21 && playerHand.Count == 5) || (score > 21))
        WinLoss();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
