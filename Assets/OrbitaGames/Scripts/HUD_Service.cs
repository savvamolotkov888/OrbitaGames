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

    private float WaterHealthHP
    {
        get => waterHealthHP.value;
        set
        {
            if (value < 0)
            {
                waterHealthHP.value = water.CurrentHealthHP = 0;
                Died(water);
            }
            else if (value > water.MaxHealthHP)
            {
                waterHealthHP.value = water.CurrentHealthHP = water.MaxHealthHP;
                Debug.Log("FULL HEALTH");
            }
            else
            {
                waterHealthHP.value = water.CurrentHealthHP = value;
            }
        }
    }


    private ProgressBar airHealthHP;

    private float AirHealthHP
    {
        get => airHealthHP.value;
        set
        {
            if (value < 0)
            {
                airHealthHP.value = air.CurrentHealthHP = 0;
                Died(air);
            }
        }
    }

    private float AirBoostHP
    {
        get => airBoostHP.value;
        set
        {
            if (value < 0)
            {
                airHealthHP.value = air.CurrentBoostHP = 0;
                playerController.TransformaitionToPreviousState();
            }
            else if (value > air.MaxBoostHP)
            {
                airBoostHP.value = air.CurrentBoostHP = air.MaxBoostHP;
                Debug.Log("FULL Boost");
            }
            else
            {
                airBoostHP.value = ice.CurrentBoostHP = value;
            }
        }
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
        playerController.ToIce += IceSelect;
        playerController.ToWater += WaterSelect;
        playerController.ToAir += AirSelect;

        water.TakeDamageEvent += WaterTakeDamage;
        water.TakeHealthEvent += WaterAddHealth;

        ice.TakeDamageEvent += IceTakeDamage;
        ice.TakeHealthEvent += IceAddDamage;
        ice.LoseBoostEvent += IceLooseBoost;

        air.TakeDamageEvent += AirTakeDamage;
        air.LoseBoostEvent += AirLooseBoost;
        //air.TakeHealthEvent 
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

    void AirLooseBoost(float boostLose)
    {
        AirBoostHP -= boostLose;

        Debug.LogError(airBoostHP.value);
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
}