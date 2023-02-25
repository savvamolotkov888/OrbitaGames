using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;

public class HUD_Service : MonoBehaviour
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

    #region BoostHP

    private ProgressBar IceBoostHP;
    private ProgressBar WaterBoostHP;
    private ProgressBar AirBoostHP;

    #endregion

    #region HealthHP

    private ProgressBar IceHealthHP;
    private ProgressBar WaterHealthHP;
    private ProgressBar AirHealthHP;

    #endregion


    private Water water;
    private Ice ice;
    private Air air;


    [Inject]
    private void Construct(PlayerInstanse player)
    {
        this.player = player;
    }

    private void Awake()
    {
        InitializationUIElements();
      
        playerController.ToIce += IceSelect;
        playerController.ToWater += WaterSelect;
        playerController.ToAir += AirSelect;
       
    }

    private void Start()
    {
        water = playerController.water;
        ice = playerController.ice;
        air = playerController.air;
        
  


        water.TakeDamageEvent += WaterTakeDamage;
        ice.TakeDamageEvent += IceTakeDamage;
        air.TakeDamageEvent += AirTakeDamage;
    }

    private void InitializationUIElements()
    {
        playerController = player.GetComponentInChildren<PlayerController>();
        _uiDocument = GetComponentInChildren<UIDocument>();

        IceIcon = _uiDocument.rootVisualElement.Q("IceIcon");
        WaterIcon = _uiDocument.rootVisualElement.Q("WaterIcon");
        AirIcon = _uiDocument.rootVisualElement.Q("AirIcon");

        WaterHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterHealthHP");
        IceHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceHealthHP");
        AirHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirHealthHP");

        WaterBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterBoostHP");
        IceBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceBoostHP");
        AirBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirBoostHP");
    }

    void WaterSelect() => SelectingIcon(WaterIcon);
    void IceSelect() => SelectingIcon(IceIcon);
    void AirSelect() => SelectingIcon(AirIcon);


    void WaterTakeDamage(float damage)
    {
        Debug.LogError(damage);
    }

    void IceTakeDamage(float damage)
    {
        Debug.LogError(damage);
    }

    void AirTakeDamage(float damage)
    {
        Debug.LogError(damage);
    }

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