using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomNetworkManager : NetworkManager
{
    DealCards dealCards;

    GameObject cardo;

    ReusableStuff reusable;

    public int numberOfPlayers;
    [SerializeField] GameObject card;
    GameObject Area;
    //[SerializeField] GameObject player;

    #region OnClientConnect
    public override void OnClientConnect(NetworkConnection conn)
    {
        //switch (numPlayers)
        //{
        //    case 1:
        //        playerPrefab.transform.position = new Vector3(-7, 4, 0);
        //        Debug.Log("player 1");
        //        break;
        //    case 2:
        //        playerPrefab.transform.position = new Vector3(-7, -4, 0);
        //        conn.identity.GetComponent<MyNetowrkPlayer>().StartTheGame();
        //        Debug.Log("player 2");
        //        break;
        //}
        
        base.OnClientConnect(conn);

        Debug.Log("I connectd to a server!");
        Debug.Log(conn.connectionId.ToString());

        cardo = Instantiate(card);
        cardo.transform.position = Vector3.zero;
        cardo.transform.localScale = new Vector3(0.35f, 0.35f, 0f);

    }
    #endregion


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetowrkPlayer playerNetwork = conn.identity.GetComponent<MyNetowrkPlayer>();

        TMP_Text userName = conn.identity.GetComponentInChildren<TMP_Text>();

       
        userName.text = string.Format($"Player {numPlayers}");

        playerNetwork.SetDisplayName($"Player {numPlayers}", numPlayers, userName);


        
        playerNetwork.StartTheGame();
    }
}