using System;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Component that changes a text to match the current time scale.
    /// </summary>
    public class TextTimer : ElementText
    {
        #region METHODS

        protected override void Tick()
        {
            
            //Change text to match the time scale!
            float timeRem = PersistentManagerScript.Instance.timeRemaining;
            float minutes = Mathf.FloorToInt(timeRem / 60);
            float seconds = Mathf.FloorToInt(timeRem % 60);
            if(seconds < 10)
            {
                textMesh.text = "0" + Convert.ToString(minutes) + ":0" + Convert.ToString(seconds);

            }
            else
            {
                textMesh.text = "0" + Convert.ToString(minutes) + ":" + Convert.ToString(seconds);
            }
            
        }        

        #endregion
    }
}