using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject homePanel;
    [SerializeField] private GameObject mainMenuPanel;

    [SerializeField] private GameObject pressAnyButton_PT;
    [SerializeField] private GameObject pressAnyButton_EN;


    //TESTE -------------------------------------
    [SerializeField] private TMP_Dropdown languageDropdown;
    //-------------------------------------------

    //private bool homePanelActivated;

    //Awake
    void Awake()
    {
        //homePanelActivated = true;
    }

    // Update is called once per frame
    void Update()
    {
        //CHECANDO SE O JOGADOR JA APERTOU ALGUM BOTÃO E SAIU DA TELA INICIAL E FOI PRA TELA DE MENU
        if (homePanel.activeInHierarchy)
        {

            if(LanguageManager.language == "portuguese")
            {
                pressAnyButton_EN.SetActive(false);
                pressAnyButton_PT.SetActive(true);
            }
            else if (LanguageManager.language == "english")
            {
                pressAnyButton_PT.SetActive(false);
                pressAnyButton_EN.SetActive(true);
            }

            if (Input.anyKeyDown)
            {
                homePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                Debug.Log("MainMenuPanel ACTIVATED");
                //homePanelActivated = false;
            }
        }
    }

    #region LanguageHandler
    //MÉTODO USADO NO CHANGE VALUE DO DROPDOWN DE SELECT LANGUAGE
    public void ChangeLanguage(int value)
    {
        if (value == 0)
        {
            LanguageManager.language = "portuguese";
        }
        else if (value == 1)
        {
            LanguageManager.language = "english";
        }
    }
    #endregion

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
