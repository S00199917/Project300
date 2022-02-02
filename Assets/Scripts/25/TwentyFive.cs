using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwentyFive : MonoBehaviour
{

    //Ingame componenets
    [SerializeField] Text trumpText;

    [SerializeField] List<Values> cards;
    
    [SerializeField] UnityEngine.GameObject PlayerArea;

    List<Values> playerDeck = new List<Values>(), enemyDeck = new List<Values>();

    //Arrays
    string[] suits = { "HEART", "DIAMOND", "CLUB", "SPADE" };
    string[] value = { "ACE", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "JACK", "QUEEN", "KING" };

    void Start()
    {
        NewGame();
    }

    void NewGame()
    {
        SelectTrump();

        UnityEngine.GameObject card = Instantiate(cards[1].card, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void SelectTrump()
    {
        int rng = Random.Range(0, 4);

        trumpText.text = suits[rng];
    }

    void Update()
    {
        Restart();
    }

    void Restart()
    {
        Debug.Log("Changed this method");
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(1);
        //}
    }
}
