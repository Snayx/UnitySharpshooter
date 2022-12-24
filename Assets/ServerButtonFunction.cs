using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerButtonFunction : MonoBehaviour
{
    GameObject _btn;
    // Update is called once per frame
    void Update()
    {
        if (!PersistentManagerScript.Instance.isConnected)
        {
            _btn.SetActive(true);
        }
        else
        {
            _btn.SetActive(false);
        }
    }

    private void Start()
    {
        _btn = GameObject.Find("ConnectToServer");
        if (!PersistentManagerScript.Instance.isConnected)
        {
            _btn.SetActive(true);
        }
        else
        {
            _btn.SetActive(false);
        }
    }
    
}
