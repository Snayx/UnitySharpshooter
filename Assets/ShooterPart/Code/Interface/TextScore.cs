using System.Globalization;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Reload Info Text.
    /// </summary>
    
    public class TextScore : ElementText
    {
        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            int score = PersistentManagerScript.Instance.score;
           textMesh.text = Convert.ToString(score);
        }


            #endregion
        }
}