// Copyright 2021, Infima Games. All Rights Reserved.

using TMPro;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Component that changes a text to match the current time scale.
    /// </summary>
    
    public class EndGameInterface : ElementText
    {
        #region METHODS
        public void EndGame() {
            gameObject.SetActive(true);

        }
        private void Start()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}