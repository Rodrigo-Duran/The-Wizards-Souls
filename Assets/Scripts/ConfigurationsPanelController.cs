using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationsPanelController : MonoBehaviour
{
    [Header("GeneralSettings")]
    [SerializeField] TMP_Dropdown difficultyDropdown;

    [SerializeField] Sprite toggleOn;
    [SerializeField] Sprite toggleOff;

    //Subtitles
    [SerializeField] Image subtitlesImage;
    private bool subtitles;

    //AutomaticRun
    [SerializeField] Image automaticRunImage;
    private bool automaticRun;

    // Awake
    void Awake()
    {
        subtitles = false;
        automaticRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Subtitles
        if (subtitles) subtitlesImage.sprite = toggleOn;
        else subtitlesImage.sprite = toggleOff;

        //AutomaticRun
        if (automaticRun) automaticRunImage.sprite = toggleOn;
        else automaticRunImage.sprite = toggleOff;

    }

    #region RestoreDefault

    public void RestoreDefault()
    {
        RestoreGeneralSettings();
        //RestoreVideoSettings();
        //RestoreAudioSettings();
        //RestoreControlsSettings();
    }

    void RestoreGeneralSettings()
    {
        //OBS: NÃO IREI RESTAURAR PARA PADRÃO O IDIOMA, POIS O PLAYER QUE ESCOLHE O IDIOMA QUE DESEJA

        //Colocando dificuldade no fácil
        difficultyDropdown.value = 0;
        subtitles = false;
        automaticRun = false;
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

    #region OnClickHandler

    public void ChangeSubtitle()
    {
        if (subtitles) subtitles = false;
        else subtitles = true;
    }

    public void ChangeAutomaticRun()
    {
        if (automaticRun) automaticRun = false;
        else automaticRun = true;
    }

    #endregion
}
