using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject homePanel;
    public GameObject mainMenuPanel;
    public bool homePanelActivated;

    //Awake
    void Awake()
    {
        homePanelActivated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (homePanelActivated)
        {
            if (Input.anyKeyDown)
            {
                homePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                Debug.Log("MainMenuPanel ACTIVATED");
                homePanelActivated = false;
            }
        }
    }


    public void ActivatingHomePanel()
    {
        homePanelActivated = true;
    }
}
