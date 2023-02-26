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
            else if (value > 100)
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

    private ProgressBar WaterHealthHP;
  

    private ProgressBar airHealthHP;
    private ProgressBar AirHealthHP;
    
    private void ValueCheck(ProgressBar progressBar, int maxValue)
    {
        if (progressBar.value > maxValue)
            progressBar.value = maxValue;
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
        water.TakeHealthEvent += WaterAddHealth;

        ice.TakeDamageEvent += IceTakeDamage;
        ice.TakeHealthEvent += IceAddDamage;

        air.TakeDamageEvent += AirTakeDamage;
        air.TakeHealthEvent += AirAddDamage;
    }

    private void InitializationUIElements()
    {
        playerController = player.GetComponentInChildren<PlayerController>();
        _uiDocument = GetComponentInChildren<UIDocument>();

        IceIcon = _uiDocument.rootVisualElement.Q("IceIcon");
        WaterIcon = _uiDocument.rootVisualElement.Q("WaterIcon");
        AirIcon = _uiDocument.rootVisualElement.Q("AirIcon");

        WaterHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterHealthHP");
//        WaterHealthHP.value = water.MaxHP;
        iceHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceHealthHP");
        iceHealthHP.value = 100;
        airHealthHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirHealthHP");
//        airHealthHP.value = air.MaxHP;

        WaterBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("WaterBoostHP");
        IceBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("IceBoostHP");
        AirBoostHP = (ProgressBar)_uiDocument.rootVisualElement.Q("AirBoostHP");
    }

    void WaterSelect() => SelectingIcon(WaterIcon);
    void IceSelect() => SelectingIcon(IceIcon);
    void AirSelect() => SelectingIcon(AirIcon);


    void WaterTakeDamage(float damage)
    {
        if (WaterHealthHP.value > 0)
        {
            Debug.LogError(damage);
            WaterHealthHP.value -= damage;
        }
        else
            Died(water);
    }

    void WaterAddHealth(float damage)
    {
        if (WaterHealthHP.value < 100)
        {
            Debug.LogError(damage);
            WaterHealthHP.value += damage;
        }
    }

    void IceAddDamage(float damage)
    {
        
            Debug.LogError(damage);
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

    private void Died(IDied player) => player.Died();
}