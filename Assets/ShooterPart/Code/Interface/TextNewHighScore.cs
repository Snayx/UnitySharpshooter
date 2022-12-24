using System.Globalization;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Reload Info Text.
    /// </summary>
    public class TextNewHighScore : ElementText
    {
        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        bool shownAlready = false;
        private void Start()
        {
            textMesh.enabled = false;
        }
        protected override void Tick()
        {

            int score = PersistentManagerScript.Instance.score;
            int bestScore = PersistentManagerScript.Instance.bestScore;
            if (score > bestScore && !shownAlready)
            {
                textMesh.enabled = true;
                textMesh.text = "New High Score!";
                StartCoroutine(HighScoreCoroutine());
            }
        }
        IEnumerator HighScoreCoroutine()
        {
            yield return new WaitForSeconds(5);
            textMesh.enabled = false;
            shownAlready = true;
        }
        #endregion
    }

}