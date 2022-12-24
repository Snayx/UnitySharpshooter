using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardInterface : MonoBehaviour
{
    [Header("Fade")]
    [Space(10)][SerializeField] Animator fadeAnimator;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Scene")]
    [Space(10)][SerializeField] string sceneToLoad;


    public void LoadLevel()
    {
        // Fade Animation
        fadeAnimator.SetTrigger("FadeOut");
        StartCoroutine(WaitToLoadLevel());
    }
    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);


        // Scene Load
        SceneManager.LoadScene(sceneToLoad);
    }
    public void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;

    }

}
