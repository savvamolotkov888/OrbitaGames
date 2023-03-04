using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PausePanel_Service : MonoBehaviour
{
    private bool pausePanelIsVisible;
    private InputSystem inputSystem;

    [SerializeField] private UIDocument UI_Pause_Document;
    private VisualElement PausePanel;

    private void Awake()
    {
        inputSystem = new InputSystem();
        InitializationUI_Elements();

        inputSystem.UI.Pause.performed += context => PanelVisibleChange();
    }

    void InitializationUI_Elements()
    {
        PausePanel = UI_Pause_Document.rootVisualElement.Q("PausePanel");
    }

    private void PanelVisibleChange()
    {
        if (pausePanelIsVisible)
        {
            PausePanel.visible = true;
        }
        else
        {
            PausePanel.visible = false;
        }


        pausePanelIsVisible = !pausePanelIsVisible;
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