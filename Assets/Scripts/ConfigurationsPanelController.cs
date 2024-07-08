using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationsPanelController : MonoBehaviour
{
    [Header("GeneralSettings: ")]

    //Dropdowns
    [SerializeField] TMP_Dropdown difficultyDropdown;
    [SerializeField] TMP_Dropdown languageDropdown;

    //Toggles
    [SerializeField] Sprite toggleOn;
    [SerializeField] Sprite toggleOff;

    //Subtitles
    [SerializeField] Image subtitlesImage;

    //AutomaticRun
    [SerializeField] Image automaticRunImage;

    //ExitConfigurationsPanel
    [SerializeField] GameObject confirmExitPanel;
    [SerializeField] GameObject configurationsPanel;
    [SerializeField] GameObject mainMenuPanel;

    //Initial Variables
    private int initialDifficulty;
    private bool initialSubtitles;
    private int initialLanguage;
    private bool initialAutomaticRun;

    private bool hasChanges;

    // Awake
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Checando se as variáveis iniciais estão diferentes das globais
        if ((initialDifficulty != ConfigurationsManager.difficulty || 
            initialAutomaticRun != ConfigurationsManager.automaticRun || 
            initialLanguage != ConfigurationsManager.language || 
            initialSubtitles != ConfigurationsManager.subtitles) && !hasChanges)
        {
            //Debug.Log("Changed");
            hasChanges = true;
        }

        //Subtitles
        if (ConfigurationsManager.subtitles) subtitlesImage.sprite = toggleOn;
        else subtitlesImage.sprite = toggleOff;

        //AutomaticRun
        if (ConfigurationsManager.automaticRun) automaticRunImage.sprite = toggleOn;
        else automaticRunImage.sprite = toggleOff;

        //Language
        languageDropdown.value = ConfigurationsManager.language;

        //Difficulty
        difficultyDropdown.value = ConfigurationsManager.difficulty;

    }

    #region RestoreSettings

    //----------------------------------------------------------------------------------
    //Métodos usados no RestoreDefault()
    //----------------------------------------------------------------------------------

    void RestoreGeneralSettings()
    {
        //Restaurando configurações gerais para padrão

        //Idioma - Português
        ConfigurationsManager.language = 0;

        //Dificuldade - Fácil
        ConfigurationsManager.difficulty = 0;

        //Legendas - Desligadas
        ConfigurationsManager.subtitles = false;

        //Corrida Automática - Desligada
        ConfigurationsManager.automaticRun = false;
    }

    void RestoreVideoSettings()
    {

    }

    void RestoreAudioSettings()
    {

    }

    void RestoreControlsSettings()
    {

    }

    #endregion

    #region ButtonsOnClickHandlers

    //----------------------------------------------------------------------------------
    //Métodos usados no OnClick dos botões
    //----------------------------------------------------------------------------------

    //SubtitlesToggleSwitch - Painel de Configurações Gerais (GeneralSettingsPanel)
    public void ChangeSubtitle()
    {
        if (ConfigurationsManager.subtitles) ConfigurationsManager.subtitles = false;
        else ConfigurationsManager.subtitles = true;
    }

    //AutomaticRunToggleSwitch - Painel de Configurações Gerais (GeneralSettingsPanel)
    public void ChangeAutomaticRun()
    {
        if (ConfigurationsManager.automaticRun) ConfigurationsManager.automaticRun = false;
        else ConfigurationsManager.automaticRun = true;
    }

    //QuitButton - Painel de Configurações (ConfigurationsPanel)
    public void ConfirmExit()
    {
        //se tiver alguma mudança, chamar o painel de confirmação de saída
        if (hasChanges) confirmExitPanel.SetActive(true);
        //se não tiver nenhuma mudança
        else
        {
            configurationsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    //RestoreDefaultButton - Painel de Configurações (ConfigurationsPanel)
    public void RestoreDefault()
    {
        RestoreGeneralSettings();
        //RestoreVideoSettings();
        //RestoreAudioSettings();
        //RestoreControlsSettings();
        hasChanges = false;
    }

    //ConfigurationsButton - Painel de Menu (MainMenuPanel)
    public void StartConfigurationsPanel()
    {
        //Colocando os valores atuais das variáveis em variáveis de ajuda
        initialDifficulty = ConfigurationsManager.difficulty;
        initialSubtitles = ConfigurationsManager.subtitles;
        initialLanguage = ConfigurationsManager.language;
        initialAutomaticRun = ConfigurationsManager.automaticRun;

        //Setando variável de mudança para falso
        hasChanges = false;
    }

    //ConfirmButton - Painel de Confirmação de Saída (ConfirmExitPanel)
    public void KeepConfigurations()
    {
        //Mantendo as configurações iguais as iniciais quando o player entrou na tela de configurações

        //Dificuldade
        ConfigurationsManager.difficulty = initialDifficulty;

        //Legendas
        ConfigurationsManager.subtitles = initialSubtitles;

        //Idioma
        ConfigurationsManager.language = initialLanguage;

        //CorridaAutomática
        ConfigurationsManager.automaticRun = initialAutomaticRun;
    }

    #endregion

    #region DropdownsOnValueChangedHandlers

    //----------------------------------------------------------------------------------
    //Métodos usados nos dropdowns para cada troca de valor
    //----------------------------------------------------------------------------------

    //LanguageDropdown - Painel de Configurações Gerais (GeneralSettingsPanel)
    public void ChangeLanguage(int value)
    {
        ConfigurationsManager.language = value;
    }

    //DifficultyDropdown - Painel de Configurações Gerais (GeneralSettingsPanel)
    public void ChangeDifficulty(int value)
    {
        ConfigurationsManager.difficulty = value;
    }
    #endregion

}
