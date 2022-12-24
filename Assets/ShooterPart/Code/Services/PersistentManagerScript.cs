using InfimaGames.LowPolyShooterPack.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PersistentManagerScript : MonoBehaviour
{
    public static PersistentManagerScript Instance { get; private set; }
    private string connectionstring;
    public int score;
    public int bestScore = 0;
    public float timeRemaining = 90.0F;
    public bool timerIsRunning = false;
    public string userName = "Default";
    public bool isConnected = false;
    public bool isTryingToConnectAgain = false;
    public int connectionTimeout = 0;
    private SqlConnection dbConnection;
    public bool offlineGame = false;
    public bool endGame = false;
    public bool isSignedIn = false;
    public GameObject EndGamePref;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else {
            Destroy(gameObject);
        }
        if (!isConnected)
        {
            bool status = setupConnection();
            if (!isConnected && !offlineGame && !status)
            {
                if (connectionTimeout > 10)
                {
                    offlineGame = true;
                }
                isTryingToConnectAgain = true;
                StartCoroutine(ConnectionCoroutine());
            }
            if (isConnected)
            {
                isConnected = updateScoreOfAPlayer();
                isTryingToConnectAgain = false;
            }
        } 
        else if (!offlineGame && isConnected && isSignedIn) { setupConnection(); }
        if (isConnected) setupConnection();

    }
    IEnumerator ConnectionCoroutine()
    {
        yield return new WaitForSeconds(5);
        bool status = setupConnection();
        if (status) { isConnected = true; } 
        else {
            connectionTimeout += 1;
            if (connectionTimeout < 10)
            {
                isTryingToConnectAgain = true;
                StartCoroutine(ConnectionCoroutine());
            }
            else
            {
                offlineGame = true;
                isTryingToConnectAgain = false;
                yield break;
            }
        }
    }

    void returnToMainMenu() {
        SceneManager.LoadScene("Main_Menu");
    }
    void Update()
    {
        
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown("escape"))
            {
                timeRemaining = 90;
                score = 0;
                timerIsRunning = false;
                returnToMainMenu();
            }
            if (Input.GetKeyDown("l"))
            {
                timeRemaining = 90;
                score = 0;
                timerIsRunning = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown("n"))
            {
                timerIsRunning = true;
            }
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;

                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    if (isConnected)
                    {
                        isConnected = updateScoreOfAPlayer();
                    }
                    
                    StartCoroutine(EndScreen());

                }
            }

            if (timeRemaining <= 0 & Input.GetKeyDown("n"))
            {
                timeRemaining = 90;
                score = 0;
                timerIsRunning = true;
            }
        }


    }
    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(5);


        timeRemaining = 90;
        score = 0;
        timerIsRunning = false;
        // Scene Load
        SceneManager.LoadScene("LeaderBoard_Scene");
    }
    public List<string> getScoresFromLeaderboard()
    {
        List<string> scores = new List<string>();
        connectionstring = "Server=SQL8002.site4now.net;" +
    "Database=db_a91b89_unitysharpshooter;" +
    "User ID=db_a91b89_unitysharpshooter_admin;" +
    "Password=snayxadmin001;";
        dbConnection = new SqlConnection(connectionstring);
        string playerNameString = "SELECT TOP 10 Score FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] ORDER BY Score DESC";
        SqlCommand command = new SqlCommand(playerNameString, dbConnection);
        dbConnection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                scores.Add(Convert.ToString(reader["Score"]));
            }
        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
            return scores;
        }
        finally
        {
            reader.Close();
            dbConnection.Close();
        }

        return scores;

    }
    public List<string> getUsersOfLeaderboard() {
        List<string> users = new List<string>();
        connectionstring = "Server=SQL8002.site4now.net;" +
    "Database=db_a91b89_unitysharpshooter;" +
    "User ID=db_a91b89_unitysharpshooter_admin;" +
    "Password=snayxadmin001;";
        dbConnection = new SqlConnection(connectionstring);
        string playerNameString = "SELECT TOP 10 Player_Name FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] ORDER BY Score DESC";
        SqlCommand command = new SqlCommand(playerNameString, dbConnection);
        dbConnection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                users.Add(Convert.ToString(reader["Player_Name"])); 
            }
        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
            return users;
        }
        finally
        {
            reader.Close();
            dbConnection.Close();
        }

        return users;

    }
    private int getSameUserNames() {
        connectionstring = "Server=SQL8002.site4now.net;" +
    "Database=db_a91b89_unitysharpshooter;" +
    "User ID=db_a91b89_unitysharpshooter_admin;" +
    "Password=snayxadmin001;";
        int counter = 0;
        dbConnection = new SqlConnection(connectionstring);
        try
        {
            string playerNameString = "SELECT Player_Name FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.userAuth] WHERE Player_Name = @username";
            SqlCommand command = new SqlCommand(playerNameString, dbConnection);
            command.Parameters.AddWithValue("@userName", userName);
            dbConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    counter++;
                }
            }
            catch (SqlException _exception)
            {
                Debug.LogWarning(_exception.ToString());
                return 9999;
            }
            finally
            {
                reader.Close();
                dbConnection.Close();   
            }
            return counter;
        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
            return 9999;
        }
    }
    public bool toRegister(string password) {
        connectionstring = "Server=SQL8002.site4now.net;" +
    "Database=db_a91b89_unitysharpshooter;" +
    "User ID=db_a91b89_unitysharpshooter_admin;" +
    "Password=snayxadmin001;";
        int userNameCount = getSameUserNames();
        if (userNameCount == 9999) {
            return false;
        }
        if (userNameCount >= 1) {
            return false;
        }
        dbConnection = new SqlConnection(connectionstring);
        try
        {

            
            isConnected = true;
            string regQueryString = "INSERT INTO [db_a91b89_unitysharpshooter].[dbo].[dbo.userAuth] VALUES(@userName,@password)";
            string scoreQueryString = "INSERT INTO [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] VALUES(@userName,0)";
            SqlCommand command = new SqlCommand(scoreQueryString, dbConnection);
            command.Parameters.AddWithValue("@userName", userName);
            SqlCommand command2 = new SqlCommand(regQueryString, dbConnection);
            command2.Parameters.AddWithValue("@userName", userName);
            command2.Parameters.AddWithValue("@password", password);

            dbConnection.Open();
            try
            {
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

            }
            catch (SqlException _exception)
            {
                Debug.LogWarning(_exception.ToString());
                return false;
            }
            finally
            {
                dbConnection.Close();
            }

            bool success = getPlayerInfoAndTestServer();
            if (success)
            {
                return true;
            }
            else return false;

        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
            return false;
        }
    }
    private bool matchedPassword(string password) {
        int exist = getSameUserNames();
        string usersPassFromDb="";
        connectionstring = "Server=SQL8002.site4now.net;" +
        "Database=db_a91b89_unitysharpshooter;" +
        "User ID=db_a91b89_unitysharpshooter_admin;" +
        "Password=snayxadmin001;";
        if (exist == 1)
        {
            dbConnection = new SqlConnection(connectionstring);
            try
            {
                string playerNameString = "SELECT Player_Name,Password FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.userAuth] WHERE Player_Name = @username";
                SqlCommand command = new SqlCommand(playerNameString, dbConnection);
                command.Parameters.AddWithValue("@userName", userName);
                dbConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        usersPassFromDb = Convert.ToString(reader["Password"]);
                    }
                }
                catch (SqlException _exception)
                {
                    Debug.LogWarning(_exception.ToString());
                    return false;
                }
                finally
                {
                    reader.Close();
                    dbConnection.Close();
                }
                if (usersPassFromDb == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException _exception)
            {
                Debug.LogWarning(_exception.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public bool startSignIn(string password) {
        if (matchedPassword(password) == true && setupConnection() == true)
        {
            return true;
        }
        else {
            return false;
        }
    }
    public bool setupConnection() {
        if (getPlayerInfoAndTestServer() == true && updateScoreOfAPlayer() == true) { return true; }
        else { return false; }
    }
    bool updateScoreOfAPlayer() {
        int tempScore = 0;
            try
            {
                dbConnection.Open();
                string queryString = "SELECT Score FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] WHERE Player_Name = @userName";
                SqlCommand command = new SqlCommand(queryString, dbConnection);
                command.Parameters.AddWithValue("@userName", userName);
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        tempScore = Convert.ToInt32(reader["Score"]);
                    }
                }
                finally
                {
                    reader.Close();
                    dbConnection.Close();
                    Debug.Log("Closed");
                }
                
                if (tempScore < score)
                {
                    bestScore = score;
                    dbConnection.Open();
                    string queryStringNewScore = "UPDATE [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] SET Score = @newScore WHERE Player_Name = @userName";
                    SqlCommand commandNewScore = new SqlCommand(queryStringNewScore, dbConnection);
                    commandNewScore.Parameters.AddWithValue("@userName", userName);
                    commandNewScore.Parameters.AddWithValue("@newScore", score);
                    try
                    {
                        commandNewScore.ExecuteNonQuery();

                    }
                    catch (SqlException _exception)
                    {
                        Debug.LogWarning(_exception.ToString());
                        return false;
                    }
                    finally
                    {
                        dbConnection.Close();
                    }

                }
                return true;

            }
            catch (SqlException _exception)
            {
                Debug.LogWarning(_exception.ToString());
                return false;

        }
    }
    public bool testCon() {
        
        connectionstring = "Server=SQL8002.site4now.net;" +
            "Database=db_a91b89_unitysharpshooter;" +
            "User ID=db_a91b89_unitysharpshooter_admin;" +
            "Password=snayxadmin001;";

        dbConnection = new SqlConnection(connectionstring);
        try
        {
            dbConnection.Open();
            dbConnection.Close();
            return true;
        }
        catch {
            return false;
        }
        
    }
    bool getPlayerInfoAndTestServer() {

        connectionstring = "Server=SQL8002.site4now.net;" +
            "Database=db_a91b89_unitysharpshooter;" +
            "User ID=db_a91b89_unitysharpshooter_admin;" +
            "Password=snayxadmin001;";

        dbConnection = new SqlConnection(connectionstring);



        try
        {

            Debug.Log("Connected to database.");
            isConnected = true;
            string queryString = "SELECT Score FROM [db_a91b89_unitysharpshooter].[dbo].[dbo.leaderboard] WHERE Player_Name = @userName";
            SqlCommand command = new SqlCommand(queryString, dbConnection);
            command.Parameters.AddWithValue("@userName", userName);
            dbConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Debug.Log(reader.ToString());
            try
            {
                while (reader.Read())
                {

                    bestScore = Convert.ToInt32(reader["Score"]);
                }
            }
            finally
            {
                reader.Close();
                dbConnection.Close();
            }
            return true;

        }
        catch (SqlException _exception)
        {
            Debug.LogWarning(_exception.ToString());
            return false;
        }

    }
    void Start()
    {
    }
}
