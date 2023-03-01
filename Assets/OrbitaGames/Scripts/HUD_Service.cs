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

    private ProgressBar IceBoostHP;
    private ProgressBar WaterBoostHP;
    private ProgressBar airBoostHP;

    #endregion

    #region HealthHP

    private ProgressBar iceHealthHP;

    private float IceHealthHP
    {
        get => iceHealthHP.value;
        set
        {
            if (value < 0)
            {
                iceHealthHP.value = ice.CurrentHealthHP = 0;
                Died(ice);
            }
            else if (value > ice.MaxHealthHP)
            {
                iceHealthHP.value = ice.CurrentHealthHP = ice.MaxHealthHP;
                Debug.Log("FULL HEALTH");
            }
            else
            {
                iceHealthHP.value = ice.CurrentHealthHP = value;
            }
        }
    }


    private ProgressBar waterHealthHP;

    public float WaterHealthHP
    {
        get => waterHealthHP.value;
        set { waterHealthHP.value = value; }
    }


    private ProgressBar airHealthHP;

    public float AirHealthHP
    {
        get => airHealthHP.value;
        set
        {
            if (value < 0)
            {
                airHealthHP.value = 0;
                Died(air);
            }
        }
    }

    public float AirBoostHP
    {
        get => airBoostHP.value;
        set => airBoostHP.value = value;
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

        waterHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterHealthHP");
        waterHealthHP.value = waterHealthHP.highValue = water.MaxHealthHP;

        iceHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceHealthHP");
        iceHealthHP.value = iceHealthHP.highValue = ice.MaxHealthHP;

        airHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirHealthHP");
        airHealthHP.value = airHealthHP.highValue = air.MaxHealthHP;

        WaterBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterBoostHP");
        WaterBoostHP.value = WaterBoostHP.highValue = water.MaxBoostHP;

        IceBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceBoostHP");
        IceBoostHP.value = IceBoostHP.highValue = ice.MaxBoostHP;

        airBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirBoostHP");
        airBoostHP.value = airBoostHP.highValue = air.MaxBoostHP;
    }

    private void Subscribe()
    {
        PlayerController.ToIce += IceSelect;
        PlayerController.ToWater += WaterSelect;
        PlayerController.ToAir += AirSelect;

        water.TakeDamageEvent += WaterTakeDamage;
        water.TakeHealthEvent += WaterAddHealth;

        ice.TakeDamageEvent += IceTakeDamage;
        ice.TakeHealthEvent += IceAddDamage;
        ice.LoseBoostEvent += IceLooseBoost;
    }


    void WaterSelect() => SelectingIcon(WaterIcon);
    void IceSelect() => SelectingIcon(IceIcon);
    void AirSelect() => SelectingIcon(AirIcon);


    void WaterTakeDamage(float damage)
    {
        WaterHealthHP -= damage;
    }

    void WaterAddHealth(float damage)
    {
        WaterHealthHP += damage;
    }

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
        IceBoostHP.value -= damage;
        Debug.LogError(IceBoostHP.value);
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


    private void Died(IDied player) => player.Died();
    private void Regeniration(IHealthRegeneration player) => player.Regeniration();
}