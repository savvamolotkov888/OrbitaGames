using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PausePanel_Service : MonoBehaviour
{
    private bool pausePanelIsVisible;
    private InputSystem inputSystem;

    [SerializeField] private UIDocument UI_Pause_Document;
    private CinemachineBrain cinemachineBrain;
    private VisualElement PausePanel;
    private Button ResumeButton;
    private Button ExitToMainMenuButton;
    private Button QuitGameButton;

    private void Awake()
    {
        inputSystem = new InputSystem();
        InitializationUI_Elements();
        cinemachineBrain = GetComponentInChildren<CinemachineBrain>();

        inputSystem.UI.Pause.performed += context => GameModeChange();
    }

    void InitializationUI_Elements()
    {
        PausePanel = UI_Pause_Document.rootVisualElement.Q("PausePanel");
        ResumeButton = (Button)UI_Pause_Document.rootVisualElement.Q("Resume");
        ExitToMainMenuButton = (Button)UI_Pause_Document.rootVisualElement.Q("ExitToMainMenu");
        QuitGameButton = (Button)UI_Pause_Document.rootVisualElement.Q("QuitGame");

        ResumeButton.clicked += GameModeChange;
        ExitToMainMenuButton.clicked += () => Debug.LogAssertion("TO MENU");
        QuitGameButton.clicked += () => Application.Quit();

        PausePanel.visible = false;
    }

    private void GameModeChange()
    {
        if (!pausePanelIsVisible)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        pausePanelIsVisible = !pausePanelIsVisible;
    }

    private void PauseGame()
    {
        // TODO УЛУЧШИТЬ
        Time.timeScale = 0;
        PausePanel.visible = true;
        cinemachineBrain.enabled = false;
        CursorSettings.ShowCursor(true);
    }

    private void ResumeGame()
    {
        PausePanel.visible = false;
        cinemachineBrain.enabled = true;
        Time.timeScale = 1;
        CursorSettings.ShowCursor(false);
    }


    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }
}