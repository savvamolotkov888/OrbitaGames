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

    private IDied died;


    #region Icons

    private VisualElement IceIcon;
    private VisualElement WaterIcon;
    private VisualElement AirIcon;

    #endregion

    #region BoostHP

    private ProgressBar iceBoostHPBar;
    private ProgressBar waterBoostHPBar;
    private ProgressBar airBoostHPBar;

    #endregion

    #region HealthHP

    private ProgressBar iceHealthHPBar;

    public float IceHealthHP
    {
        get => iceHealthHPBar.value;
        set
        {
            iceHealthHPBar.value  = value;
        }
    }


    private ProgressBar waterHealthHPBar;

    public float WaterHealthHP
    {
        get => waterHealthHPBar.value;
        set { waterHealthHPBar.value = value; }
    }
    
    public float WaterBoostHP
    {
        set => waterBoostHPBar.value = value; 
    }


    private ProgressBar airHealthHPBar;

    public float AirHealthHP
    {
        get => airHealthHPBar.value;
        set
        {
            //dosnt work
            airHealthHPBar.value = value;
        }
    }

    public float AirBoostHP
    {
        get => airBoostHPBar.value;
        set => airBoostHPBar.value = value;
    }

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
        Initialization();
        Subscribe();
    }

    private void Initialization()
    {
        playerController = player.GetComponentInChildren<PlayerController>();
        _uiDocument = GetComponentInChildren<UIDocument>();

        water = playerController.water;
        ice = playerController.ice;
        air = playerController.air;
        InitializationUIElements();
    }

    private void InitializationUIElements()
    {
        IceIcon = _uiDocument.rootVisualElement.Q("IceIcon");
        WaterIcon = _uiDocument.rootVisualElement.Q("WaterIcon");
        AirIcon = _uiDocument.rootVisualElement.Q("AirIcon");

        waterHealthHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterHealthHP");
        waterHealthHPBar.value = waterHealthHPBar.highValue = water.MaxHealthHP;

        iceHealthHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("IceHealthHP");
        iceHealthHPBar.value = iceHealthHPBar.highValue = ice.MaxHealthHP;

        airHealthHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("AirHealthHP");
        airHealthHPBar.value = airHealthHPBar.highValue = air.MaxHealthHP;

        waterBoostHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterBoostHP");
        waterBoostHPBar.value = waterBoostHPBar.highValue = water.MaxBoostHP;

        iceBoostHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("IceBoostHP");
        iceBoostHPBar.value = iceBoostHPBar.highValue = ice.MaxBoostHP;

        airBoostHPBar = (ProgressBar)_uiDocument.rootVisualElement.Q("AirBoostHP");
        airBoostHPBar.value = airBoostHPBar.highValue = air.MaxBoostHP;
    }

    private void Subscribe()
    {
        PlayerController.ToIce += IceSelect;
        PlayerController.ToWater += WaterSelect;
        PlayerController.ToAir += AirSelect;

        ice.TakeDamageEvent += IceTakeDamage;
        ice.TakeHealthEvent += IceAddDamage;
        ice.LoseBoostEvent += IceLooseBoost;
    }


    void WaterSelect() => SelectingIcon(WaterIcon);
    void IceSelect() => SelectingIcon(IceIcon);
    void AirSelect() => SelectingIcon(AirIcon);

    
    void IceAddDamage(float damage)
    {
        IceHealthHP += damage;
    }


    void IceTakeDamage(float damage)
    {
        IceHealthHP -= damage;
    }

    void IceLooseBoost(float damage)
    {
        iceBoostHPBar.value -= damage;
        Debug.LogError(iceBoostHPBar.value);
    }


    void AirTakeDamage(float damage)
    {
        AirHealthHP -= damage;
    }

    private void SelectingIcon(VisualElement icon)
    {
        UnSelectingIcon(WaterIcon);
        UnSelectingIcon(IceIcon);
        UnSelectingIcon(AirIcon);

        icon.style.borderBottomColor = icon.style.borderLeftColor =
            icon.style.borderRightColor = icon.style.borderTopColor = SelectedIconBorderColor;
    }

    private void UnSelectingIcon(VisualElement icon) =>
        icon.style.borderBottomColor = icon.style.borderLeftColor =
            icon.style.borderRightColor = icon.style.borderTopColor = DefaultIconBorderColor;
}