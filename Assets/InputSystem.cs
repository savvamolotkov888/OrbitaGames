//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputSystem : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Control"",
            ""id"": ""f13aaf27-d6b9-419c-860a-08e7b7659bb0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""924b925d-8623-474e-a230-fd5d35b83461"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2c12c2f3-53bc-4955-b28c-3a5245238f5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""acb6862c-e9f9-49e6-b282-504ed817b0ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b1b56364-871c-48bb-a77e-658a57a0d6cb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e14824ec-43a7-4d35-a82c-a95b15144a2e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f1ff90e6-5de9-40ba-b1b1-b929cc55cdaf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""af29fea9-446c-43b8-9534-71632f8d72b9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b234d0d-36cf-4588-ad66-10fc0dfc376a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""a613bd5c-7c56-4b1c-8d60-27d6dfdabedb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2a57e4e7-a0a4-46b1-8df3-b2da569a139a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1af5a084-ebae-4db8-abae-eb68e2fe6c74"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d4fe382c-ebfb-4a07-90d0-60aca63b70a8"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f7379fe7-722f-414e-b871-8a2b6121631a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0d24e146-71eb-4312-99a5-f21379ef0992"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bbc705b-76bf-4b1d-8ecb-8f5c2c9e69e3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Transformation"",
            ""id"": ""c5d85277-26ef-4788-8b90-eaaf7881ade8"",
            ""actions"": [
                {
                    ""name"": ""ToWater"",
                    ""type"": ""Button"",
                    ""id"": ""29b16359-1447-40e2-b3ed-8255d286bf9f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToIce"",
                    ""type"": ""Button"",
                    ""id"": ""ba0742f5-c491-4ef4-bb4a-f8593f254112"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToAir"",
                    ""type"": ""Button"",
                    ""id"": ""f8732685-2293-4f3d-8f63-d32a37638e96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5327e36-74a7-4297-8965-7d273f084d76"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ToWater"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66768e3f-0e7a-4a63-83d3-268ef4e531dd"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToIce"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60d9e1f6-4034-43e1-97d5-ed7834d676bc"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToAir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Control
        m_Control = asset.FindActionMap("Control", throwIfNotFound: true);
        m_Control_Move = m_Control.FindAction("Move", throwIfNotFound: true);
        m_Control_Jump = m_Control.FindAction("Jump", throwIfNotFound: true);
        m_Control_Shift = m_Control.FindAction("Shift", throwIfNotFound: true);
        // Transformation
        m_Transformation = asset.FindActionMap("Transformation", throwIfNotFound: true);
        m_Transformation_ToWater = m_Transformation.FindAction("ToWater", throwIfNotFound: true);
        m_Transformation_ToIce = m_Transformation.FindAction("ToIce", throwIfNotFound: true);
        m_Transformation_ToAir = m_Transformation.FindAction("ToAir", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Control
    private readonly InputActionMap m_Control;
    private IControlActions m_ControlActionsCallbackInterface;
    private readonly InputAction m_Control_Move;
    private readonly InputAction m_Control_Jump;
    private readonly InputAction m_Control_Shift;
    public struct ControlActions
    {
        private @InputSystem m_Wrapper;
        public ControlActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Control_Move;
        public InputAction @Jump => m_Wrapper.m_Control_Jump;
        public InputAction @Shift => m_Wrapper.m_Control_Shift;
        public InputActionMap Get() { return m_Wrapper.m_Control; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlActions set) { return set.Get(); }
        public void SetCallbacks(IControlActions instance)
        {
            if (m_Wrapper.m_ControlActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnJump;
                @Shift.started -= m_Wrapper.m_ControlActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_ControlActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_ControlActionsCallbackInterface.OnShift;
            }
            m_Wrapper.m_ControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
            }
        }
    }
    public ControlActions @Control => new ControlActions(this);

    // Transformation
    private readonly InputActionMap m_Transformation;
    private ITransformationActions m_TransformationActionsCallbackInterface;
    private readonly InputAction m_Transformation_ToWater;
    private readonly InputAction m_Transformation_ToIce;
    private readonly InputAction m_Transformation_ToAir;
    public struct TransformationActions
    {
        private @InputSystem m_Wrapper;
        public TransformationActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToWater => m_Wrapper.m_Transformation_ToWater;
        public InputAction @ToIce => m_Wrapper.m_Transformation_ToIce;
        public InputAction @ToAir => m_Wrapper.m_Transformation_ToAir;
        public InputActionMap Get() { return m_Wrapper.m_Transformation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TransformationActions set) { return set.Get(); }
        public void SetCallbacks(ITransformationActions instance)
        {
            if (m_Wrapper.m_TransformationActionsCallbackInterface != null)
            {
                @ToWater.started -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToWater;
                @ToWater.performed -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToWater;
                @ToWater.canceled -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToWater;
                @ToIce.started -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToIce;
                @ToIce.performed -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToIce;
                @ToIce.canceled -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToIce;
                @ToAir.started -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToAir;
                @ToAir.performed -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToAir;
                @ToAir.canceled -= m_Wrapper.m_TransformationActionsCallbackInterface.OnToAir;
            }
            m_Wrapper.m_TransformationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToWater.started += instance.OnToWater;
                @ToWater.performed += instance.OnToWater;
                @ToWater.canceled += instance.OnToWater;
                @ToIce.started += instance.OnToIce;
                @ToIce.performed += instance.OnToIce;
                @ToIce.canceled += instance.OnToIce;
                @ToAir.started += instance.OnToAir;
                @ToAir.performed += instance.OnToAir;
                @ToAir.canceled += instance.OnToAir;
            }
        }
    }
    public TransformationActions @Transformation => new TransformationActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
    }
    public interface ITransformationActions
    {
        void OnToWater(InputAction.CallbackContext context);
        void OnToIce(InputAction.CallbackContext context);
        void OnToAir(InputAction.CallbackContext context);
    }
}
