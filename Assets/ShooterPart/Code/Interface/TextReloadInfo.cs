using System.Globalization;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Reload Info Text.
    /// </summary>
    public class TextReloadInfo : ElementText
    {
        #region METHODS
        
        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            
            //Total Ammunition.
            float ammunitionCurrent = equippedWeapon.GetAmmunitionCurrent();
            
            
            //Update Text.
            if (ammunitionCurrent <= 5)
            {
                textMesh.text = "Press 'R' to Reload!";
            }
            else {
                textMesh.text = "";
            }
        }
        
        #endregion
    }
}