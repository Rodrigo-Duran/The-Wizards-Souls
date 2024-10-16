using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject mainMenuPanel;

    [SerializeField] private GameObject pressAnyButton_PT;
    [SerializeField] private GameObject pressAnyButton_EN;

    //Awake
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //CHECANDO SE O JOGADOR JA APERTOU ALGUM BOTÃO E SAIU DA TELA INICIAL E FOI PRA TELA DE MENU
        if (homePanel.activeInHierarchy)
        {

            if(ConfigurationsManager.language == 0)
            {
                pressAnyButton_EN.SetActive(false);
                pressAnyButton_PT.SetActive(true);
            }
            else if (ConfigurationsManager.language == 1)
            {
                pressAnyButton_PT.SetActive(false);
                pressAnyButton_EN.SetActive(true);
            }

            if (Input.anyKeyDown)
            {
                homePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                Debug.Log("MainMenuPanel ACTIVATED");
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Phase1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
