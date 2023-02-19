using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;

public class UIEventBus : MonoBehaviour
{
    private PlayerInstanse player;
    private PlayerController playerController;
    private UIDocument _uiDocument;

    [SerializeField] private Color SelectedIconBorderColor;
    [SerializeField] private Color DefaultIconBorderColor;


    #region Icons

    private VisualElement IceIcon;
    private VisualElement WaterIcon;
    private VisualElement AirIcon;

    #endregion

    [Inject]
    private void Construct(PlayerInstanse player)
    {
        this.player = player;
    }

    private void Start()
    {
        playerController = player.GetComponentInChildren<PlayerController>();
        _uiDocument = GetComponentInChildren<UIDocument>();


        IceIcon = _uiDocument.rootVisualElement.Q("IceIcon");
        WaterIcon = _uiDocument.rootVisualElement.Q("WaterIcon");
        AirIcon = _uiDocument.rootVisualElement.Q("AirIcon");
        

        playerController.ToWater += Water;
        playerController.ToIce += Ice;
        playerController.ToAir += Air;
    }

    void Water() => SelectingIcon(WaterIcon);
    void Ice() => SelectingIcon(IceIcon);
    void Air() => SelectingIcon(AirIcon);

    private void SelectingIcon(VisualElement icon)
    {
        UnSelectingIcon(WaterIcon);
        UnSelectingIcon(IceIcon);
        UnSelectingIcon(AirIcon);
        
        icon.style.borderBottomColor = SelectedIconBorderColor;
        icon.style.borderLeftColor = SelectedIconBorderColor;
        icon.style.borderRightColor = SelectedIconBorderColor;
        icon.style.borderTopColor = SelectedIconBorderColor;
    }

    private void UnSelectingIcon(VisualElement icon)
    {
        icon.style.borderBottomColor = DefaultIconBorderColor;
        icon.style.borderLeftColor = DefaultIconBorderColor;
        icon.style.borderRightColor = DefaultIconBorderColor;
        icon.style.borderTopColor = DefaultIconBorderColor;
    }
}