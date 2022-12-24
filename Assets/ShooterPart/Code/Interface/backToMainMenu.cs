using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
