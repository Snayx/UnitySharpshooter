using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterInterface : MonoBehaviour
{
    [Header("Fade")]
    [Space(10)][SerializeField] Animator fadeAnimator;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Scene")]
    [Space(10)][SerializeField] string sceneToLoad;

    TMP_InputField tmp_f;
    TMP_InputField tmp_p;
    GameObject err_dia;
    GameObject sn;
    GameObject sn_p;
    bool status;

    public void LoadLevel()
    {
        // Fade Animation
        fadeAnimator.SetTrigger("FadeOut");
        PersistentManagerScript.Instance.isSignedIn = false;
        PersistentManagerScript.Instance.offlineGame = true;
        StartCoroutine(WaitToLoadLevel());
    }
    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);


        // Scene Load
        SceneManager.LoadScene(sceneToLoad);
    }
    IEnumerator WaitForButtonToDisappear()
    {
        
        yield return new WaitForSeconds(3);
        err_dia.SetActive(false);
    }
    public void Start()
    {
        err_dia = GameObject.Find("ProblemWithSignIn");
        err_dia.SetActive(false);
        sn = GameObject.Find("Username");
        tmp_f = sn.GetComponent<TMP_InputField>();
        sn_p = GameObject.Find("Password");
        tmp_p = sn_p.GetComponent<TMP_InputField>();
        Debug.Log(tmp_p.text);
    }
    public void signIn() {

            
            string password = "";
            Debug.Log(tmp_f.text);
            PersistentManagerScript.Instance.userName = tmp_f.text;
            password = tmp_p.text;
            password = EncodePasswordToBase64(password);
            
            bool success = PersistentManagerScript.Instance.startSignIn(password);
            if (success)
            {
                PersistentManagerScript.Instance.isConnected = true;
                PersistentManagerScript.Instance.isSignedIn = true;
                fadeAnimator.SetTrigger("FadeOut");
                StartCoroutine(WaitToLoadLevel());
            }
            else
            {
                err_dia.SetActive(true);
                PersistentManagerScript.Instance.isConnected = false;
                PersistentManagerScript.Instance.isSignedIn = false;
                StartCoroutine(WaitForButtonToDisappear());
            }
        
        
    }
    private static string EncodePasswordToBase64(string password)
    {
        try
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
    public void register() {
        bool success = false;
        string password = "";
        PersistentManagerScript.Instance.userName = tmp_f.text;
        password = tmp_p.text;
        password = EncodePasswordToBase64(password);
        success = PersistentManagerScript.Instance.toRegister(password);
        if (success)
        {
            PersistentManagerScript.Instance.isConnected = true;
            PersistentManagerScript.Instance.isSignedIn = true;
            fadeAnimator.SetTrigger("FadeOut");
            StartCoroutine(WaitToLoadLevel());
        }
        else {
            err_dia.SetActive(true);
            PersistentManagerScript.Instance.isConnected = false;
            PersistentManagerScript.Instance.isSignedIn = false;
            StartCoroutine(WaitForButtonToDisappear());
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

}
