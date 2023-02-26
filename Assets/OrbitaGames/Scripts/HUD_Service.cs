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
    private ProgressBar AirBoostHP;

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
                iceHealthHP.value = 0;
                ice.CurrentHP = 0;
                Died(ice);
            }
            else if (value > ice.MaxHP)
            {
                iceHealthHP.value = ice.MaxHP;
                ice.CurrentHP = ice.MaxHP;
                Debug.Log("FULL HEALTH");
            }
            else
            {
                iceHealthHP.value = value;
                ice.CurrentHP = value;
            }
        }
    }


    private ProgressBar waterHealthHP;

    private float WaterHealthHP
    {
        get => waterHealthHP.value;
        set
        {
            if (value < 0)
            {
                waterHealthHP.value = 0;
                water.CurrentHP = 0;
                Died(water);
            }
            else if (value > water.MaxHP)
            {
                waterHealthHP.value = water.MaxHP;
                water.CurrentHP = water.MaxHP;
                Debug.Log("FULL HEALTH");
            }
            else
            {
                waterHealthHP.value = value;
                water.CurrentHP = value;
            }
        }
    }


    private ProgressBar airHealthHP;
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
        waterHealthHP.value = water.MaxHP;
        iceHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceHealthHP");
        iceHealthHP.value = ice.MaxHP;
        airHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirHealthHP");
        airHealthHP.value = air.MaxHP;

        WaterBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterBoostHP");
        IceBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceBoostHP");
        AirBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirBoostHP");
    }

    private void Subscribe()
    {
        playerController.ToIce += IceSelect;
        playerController.ToWater += WaterSelect;
        playerController.ToAir += AirSelect;

        water.TakeDamageEvent += WaterTakeDamage;
        water.TakeHealthEvent += WaterAddHealth;

        ice.TakeDamageEvent += IceTakeDamage;
        ice.TakeHealthEvent += IceAddDamage;

        air.TakeDamageEvent += AirTakeDamage;
        air.TakeHealthEvent += AirAddDamage;
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

    void AirAddDamage(float damage)
    {
        if (AirHealthHP.value < 100)
        {
            Debug.LogError(damage);
            AirHealthHP.value += damage;
        }
    }

    void IceTakeDamage(float damage)
    {
        IceHealthHP -= damage;
    }

    void AirTakeDamage(float damage)
    {
        if (AirHealthHP.value > 0)
        {
            Debug.LogError(damage);
            AirHealthHP.value -= damage;
        }
        else
            Died(air);
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
}