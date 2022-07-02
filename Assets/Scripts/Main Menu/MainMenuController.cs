using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script will handle all the required logic for the main menu scenes, loading save files, creating new games,
/// adjusting settings etc.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private string versionNumber;
    [SerializeField] private TMP_Text versionText;

    [Header("Sounds References")]
    [SerializeField] private Sound hoverSound;
    [Header("Button References")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button loadGameBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitBtn;

    [Header("Panel References")] 
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject buttonsPanel;
    
    private void Start()
    {
        #region Assigning button clicks

        startBtn.onClick.AddListener(StartPress);
        continueBtn.onClick.AddListener(Continue);
        newGameBtn.onClick.AddListener(NewGame);
        loadGameBtn.onClick.AddListener(LoadGame);
        settingsBtn.onClick.AddListener(Settings);
        exitBtn.onClick.AddListener(Exit);
        
        #endregion

        versionText.text = versionNumber;
        
        Initialization();
    }

    private void Initialization()
    {
        //DOTween.Init();
        // TODO: Implement the following 
        // disable Continue button if no save file present
        // disable Load Game button if no save file present 
        
        // Update settings panel to reflect user changes if any
    }


    public void OnUiHover()
    {
        // TODO: Sound manager ui hover sound
    }

    #region Button Functions

    private void StartPress()
    {
        startPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }
    
    private void Continue()
    {
        // load last save
    }
    
    private void NewGame()
    {
        // start a new save 
    }
    
    private void LoadGame()
    {
        // load a previous save
    }
    
    private void Settings()
    {
        // open settings panel
    }
    
    private void Exit()
    {
        // close the game
        Application.Quit();
    }

    #endregion
   
}
