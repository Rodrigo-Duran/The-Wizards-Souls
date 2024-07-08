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
        //Checando se as vari�veis iniciais est�o diferentes das globais
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
    //M�todos usados no RestoreDefault()
    //----------------------------------------------------------------------------------

    void RestoreGeneralSettings()
    {
        //Restaurando configura��es gerais para padr�o

        //Idioma - Portugu�s
        ConfigurationsManager.language = 0;

        //Dificuldade - F�cil
        ConfigurationsManager.difficulty = 0;

        //Legendas - Desligadas
        ConfigurationsManager.subtitles = false;

        //Corrida Autom�tica - Desligada
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
    //M�todos usados no OnClick dos bot�es
    //----------------------------------------------------------------------------------

    //SubtitlesToggleSwitch - Painel de Configura��es Gerais (GeneralSettingsPanel)
    public void ChangeSubtitle()
    {
        if (ConfigurationsManager.subtitles) ConfigurationsManager.subtitles = false;
        else ConfigurationsManager.subtitles = true;
    }

    //AutomaticRunToggleSwitch - Painel de Configura��es Gerais (GeneralSettingsPanel)
    public void ChangeAutomaticRun()
    {
        if (ConfigurationsManager.automaticRun) ConfigurationsManager.automaticRun = false;
        else ConfigurationsManager.automaticRun = true;
    }

    //QuitButton - Painel de Configura��es (ConfigurationsPanel)
    public void ConfirmExit()
    {
        //se tiver alguma mudan�a, chamar o painel de confirma��o de sa�da
        if (hasChanges) confirmExitPanel.SetActive(true);
        //se n�o tiver nenhuma mudan�a
        else
        {
            configurationsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    //RestoreDefaultButton - Painel de Configura��es (ConfigurationsPanel)
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
        //Colocando os valores atuais das vari�veis em vari�veis de ajuda
        initialDifficulty = ConfigurationsManager.difficulty;
        initialSubtitles = ConfigurationsManager.subtitles;
        initialLanguage = ConfigurationsManager.language;
        initialAutomaticRun = ConfigurationsManager.automaticRun;

        //Setando vari�vel de mudan�a para falso
        hasChanges = false;
    }

    //ConfirmButton - Painel de Confirma��o de Sa�da (ConfirmExitPanel)
    public void KeepConfigurations()
    {
        //Mantendo as configura��es iguais as iniciais quando o player entrou na tela de configura��es

        //Dificuldade
        ConfigurationsManager.difficulty = initialDifficulty;

        //Legendas
        ConfigurationsManager.subtitles = initialSubtitles;

        //Idioma
        ConfigurationsManager.language = initialLanguage;

        //CorridaAutom�tica
        ConfigurationsManager.automaticRun = initialAutomaticRun;
    }

    #endregion

    #region DropdownsOnValueChangedHandlers

    //----------------------------------------------------------------------------------
    //M�todos usados nos dropdowns para cada troca de valor
    //----------------------------------------------------------------------------------

    //LanguageDropdown - Painel de Configura��es Gerais (GeneralSettingsPanel)
    public void ChangeLanguage(int value)
    {
        ConfigurationsManager.language = value;
    }

    //DifficultyDropdown - Painel de Configura��es Gerais (GeneralSettingsPanel)
    public void ChangeDifficulty(int value)
    {
        ConfigurationsManager.difficulty = value;
    }
    #endregion

}
