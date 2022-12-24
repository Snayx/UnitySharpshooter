using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Component that changes a text to match the current time scale.
    /// </summary>
    public class LeaderBoardInterface : MonoBehaviour
    {
        
        public GameObject Leaderboard;
        public GameObject pos_pref;
        public GameObject name_pref;
        public GameObject score_pref;
        List<TextMeshProUGUI> names;
        List<TextMeshProUGUI> scores;
        #region METHODS
        private void Start()
        {
            List<string> users = PersistentManagerScript.Instance.getUsersOfLeaderboard();
            List<string> scores = PersistentManagerScript.Instance.getScoresFromLeaderboard();
            buildLeaderboard(users, scores);
            //for (int i = 0; i < 10; i++)
            //{
            /*
            TextMeshProUGUI position;
            TextMeshProUGUI name;
            TextMeshProUGUI score;


            position.text = 
            name.text = "Snayx";
            score.text = "999";
            */



            //names.Add(name);
            //scores.Add(score);
            //}

            //Debug.Log(names.ToString());

        }

        private void buildLeaderboard(List<string> users,List<string> scores) {

            int y = 240;
            for (int i = 0; i < users.Count; i++)
            {
                GameObject pos_ = Instantiate(pos_pref);
                GameObject name_ = Instantiate(name_pref);
                GameObject score_ = Instantiate(score_pref);
                pos_.transform.SetParent(Leaderboard.transform);
                name_.transform.SetParent(Leaderboard.transform);
                score_.transform.SetParent(Leaderboard.transform);
                pos_.GetComponent<TextMeshProUGUI>().text = Convert.ToString(i + 1);
                name_.GetComponent<TextMeshProUGUI>().text = Convert.ToString(users[i]);
                score_.GetComponent<TextMeshProUGUI>().text = scores[i];
                pos_.transform.localScale = new Vector3(1, 1, 1);
                name_.transform.localScale = new Vector3(1, 1, 1);
                score_.transform.localScale = new Vector3(1, 1, 1);
                pos_.transform.localPosition = new Vector3(-708, y, 0);
                name_.transform.localPosition = new Vector3(-169, y, 0);
                score_.transform.localPosition = new Vector3(859, y, 0);

                y -= 60;
            }
        }
        //List<string> names, List<int> scores
        #endregion
    }
}