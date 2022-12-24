using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuInformationGainer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameOfThePlayer;
    [SerializeField] TextMeshProUGUI bestScoreOfThePlayer;
    [SerializeField] PersistentManagerScript pms;
    [SerializeField] TextMeshProUGUI connectionStatus;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.Find("PersistentManager");
        pms = gm.GetComponent<PersistentManagerScript>();
        nameOfThePlayer.text = "ID: " + pms.userName;
        bestScoreOfThePlayer.text = "Best score: " + pms.bestScore;

        if (pms.isConnected)
        {
            connectionStatus.text = "Connected!";
        }
        else
        {
            connectionStatus.text = "There is a problem with the server.";
        }
        if (pms.isTryingToConnectAgain) {
            connectionStatus.text = "Trying to connect to the server again! (Tries: "+pms.connectionTimeout+"/10";
        }
        if (!pms.isConnected && pms.connectionTimeout >= 10) {
            connectionStatus.text = "Connection to the server failed!\nOFFLINE GAME /w out leaderboard.";
        }
        if (!pms.isSignedIn)
        {
            connectionStatus.text = "Not signed in! OFFLINE GAME /w out leaderboard.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pms.isConnected)
        {
            connectionStatus.text = "Connected!";
        }
        else
        {
            connectionStatus.text = "There is a problem with the server.";
        }
        if (pms.isTryingToConnectAgain)
        {
            connectionStatus.text = "Trying to connect to the server again! (Tries: " + pms.connectionTimeout + "/10";
        }
        
        if (!pms.isConnected && pms.connectionTimeout >= 10)
        {
            connectionStatus.text = "Connection to the server failed! OFFLINE GAME /w out leaderboard.";
        }
        if (!pms.isSignedIn) {
            connectionStatus.text = "Not signed in! OFFLINE GAME /w out leaderboard.";
        }
    }
}
