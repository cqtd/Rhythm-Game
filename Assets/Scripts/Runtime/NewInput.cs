// GENERATED AUTOMATICALLY FROM 'Assets/Input/NewInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""NewInput"",
    ""maps"": [
        {
            ""name"": ""Selector"",
            ""id"": ""aef6621a-9d16-42aa-81db-066239992d78"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""bfc3e413-a391-4a63-8b1c-9b3606467332"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""0019fb7d-bbe0-4e95-9f4e-b2fb2957d4b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""a0337423-565e-4271-9497-a38fc913db5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""5d02ffbb-bc0b-4efc-a1ae-8ef97a8fcc34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Option"",
                    ""type"": ""Button"",
                    ""id"": ""6a85507b-6638-4720-bccd-13fc7c65520d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Decide"",
                    ""type"": ""Button"",
                    ""id"": ""4625f929-0501-4cdd-8f8e-f90f8205267d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38eb6cbd-0d6c-4a68-b7c4-0fbf5544ae2e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c72cee3e-00a2-4c89-bf0f-812c73e531cb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aeb30cb3-a7e6-4316-a962-27a0234733eb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d443615-a262-4347-9e5c-6384a05461a1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb8d2f8e-0e58-4de2-beda-fa7cce910e37"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Option"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be719933-3a3b-4843-ba37-2f8dc0acf429"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay"",
            ""id"": ""1b0b0229-8c85-441e-93fe-194770b12d6e"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""747bf379-48e4-4dec-b686-3c4121725878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scratch"",
                    ""type"": ""Button"",
                    ""id"": ""e0bdda09-f4c7-4f71-865e-02c1ff34414a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note1"",
                    ""type"": ""Button"",
                    ""id"": ""2ccb4de6-ae51-42c5-9d27-715fe0abbfc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note2"",
                    ""type"": ""Button"",
                    ""id"": ""dbf8681a-15b0-47dd-b51b-deb6ad9081e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note3"",
                    ""type"": ""Button"",
                    ""id"": ""51c8f983-dbfa-46fc-909a-4fbd13e9a0da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note4"",
                    ""type"": ""Button"",
                    ""id"": ""c4469e3e-b23a-4adb-8c1b-6d3bf0813135"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note5"",
                    ""type"": ""Button"",
                    ""id"": ""61489b6c-f4d0-4037-9010-f9e97ee947cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note6"",
                    ""type"": ""Button"",
                    ""id"": ""c2842da8-05f8-4f61-b31b-17b8ba5f7b18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Note7"",
                    ""type"": ""Button"",
                    ""id"": ""3b1574f8-4fa7-4dfb-8d05-07843aa0919b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""b493e1ed-a5c7-4126-ba7b-7c76e8b3061e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpeedDown"",
                    ""type"": ""Button"",
                    ""id"": ""cdd6bff8-c38b-46b3-b656-15aa8c71a744"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5303b27-0c35-46ca-950c-87539e02ae3b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1a89201-8f13-40f6-8cbd-724ab364c111"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scratch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c36a119c-c690-4fd4-8941-a586dd0ebe65"",
                    ""path"": ""<MidiDevice>/note048"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Scratch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13e011cd-76cf-4901-aad0-35523be77d16"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5c88647-1ad4-4332-8e80-5cb9158bd5a8"",
                    ""path"": ""<MidiDevice>/note044"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""839da09d-edb4-49b6-ae96-dd66411df3ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ef03bb4-43c9-4794-a90d-1244affea5fe"",
                    ""path"": ""<MidiDevice>/note045"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dba6b892-b871-40a8-a4b7-8d33177e36fc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d044cb9-5b15-4c7f-abf2-5231f706a3fe"",
                    ""path"": ""<MidiDevice>/note049"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bb8a296-e45c-4b53-810f-b9831c30b21b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6adb8446-ac15-41b1-9d75-84f7fa717429"",
                    ""path"": ""<MidiDevice>/note050"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcf37317-7e3e-46cc-879a-13ba9ffe69f8"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32be0aab-2362-4932-b10b-ff90ee8d307b"",
                    ""path"": ""<MidiDevice>/note046"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beec9532-fa2a-4f3c-8cf0-ac8a2c5b92ea"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3f10468-8e9e-4647-9d87-a2a92811781b"",
                    ""path"": ""<MidiDevice>/note047"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5e45272-2e74-4bdc-87b0-854c816edf41"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Note7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1eb76f6-0524-464b-ac5d-68a1164a6802"",
                    ""path"": ""<MidiDevice>/note051"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""Note7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2208656-b012-494e-aacc-e33642fe2ea9"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52ced712-1b2f-4ba4-b628-3f867d03dd1e"",
                    ""path"": ""<MidiDevice>/note048"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Midi"",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20b18fee-7f54-4fba-b2ca-614bfb655346"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Midi"",
            ""bindingGroup"": ""Midi"",
            ""devices"": [
                {
                    ""devicePath"": ""<MidiDevice>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Selector
        m_Selector = asset.FindActionMap("Selector", throwIfNotFound: true);
        m_Selector_Up = m_Selector.FindAction("Up", throwIfNotFound: true);
        m_Selector_Down = m_Selector.FindAction("Down", throwIfNotFound: true);
        m_Selector_Right = m_Selector.FindAction("Right", throwIfNotFound: true);
        m_Selector_Left = m_Selector.FindAction("Left", throwIfNotFound: true);
        m_Selector_Option = m_Selector.FindAction("Option", throwIfNotFound: true);
        m_Selector_Decide = m_Selector.FindAction("Decide", throwIfNotFound: true);
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Scratch = m_Gameplay.FindAction("Scratch", throwIfNotFound: true);
        m_Gameplay_Note1 = m_Gameplay.FindAction("Note1", throwIfNotFound: true);
        m_Gameplay_Note2 = m_Gameplay.FindAction("Note2", throwIfNotFound: true);
        m_Gameplay_Note3 = m_Gameplay.FindAction("Note3", throwIfNotFound: true);
        m_Gameplay_Note4 = m_Gameplay.FindAction("Note4", throwIfNotFound: true);
        m_Gameplay_Note5 = m_Gameplay.FindAction("Note5", throwIfNotFound: true);
        m_Gameplay_Note6 = m_Gameplay.FindAction("Note6", throwIfNotFound: true);
        m_Gameplay_Note7 = m_Gameplay.FindAction("Note7", throwIfNotFound: true);
        m_Gameplay_SpeedUp = m_Gameplay.FindAction("SpeedUp", throwIfNotFound: true);
        m_Gameplay_SpeedDown = m_Gameplay.FindAction("SpeedDown", throwIfNotFound: true);
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

    // Selector
    private readonly InputActionMap m_Selector;
    private ISelectorActions m_SelectorActionsCallbackInterface;
    private readonly InputAction m_Selector_Up;
    private readonly InputAction m_Selector_Down;
    private readonly InputAction m_Selector_Right;
    private readonly InputAction m_Selector_Left;
    private readonly InputAction m_Selector_Option;
    private readonly InputAction m_Selector_Decide;
    public struct SelectorActions
    {
        private @NewInput m_Wrapper;
        public SelectorActions(@NewInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Selector_Up;
        public InputAction @Down => m_Wrapper.m_Selector_Down;
        public InputAction @Right => m_Wrapper.m_Selector_Right;
        public InputAction @Left => m_Wrapper.m_Selector_Left;
        public InputAction @Option => m_Wrapper.m_Selector_Option;
        public InputAction @Decide => m_Wrapper.m_Selector_Decide;
        public InputActionMap Get() { return m_Wrapper.m_Selector; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectorActions set) { return set.Get(); }
        public void SetCallbacks(ISelectorActions instance)
        {
            if (m_Wrapper.m_SelectorActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDown;
                @Right.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnLeft;
                @Option.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnOption;
                @Option.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnOption;
                @Option.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnOption;
                @Decide.started -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDecide;
                @Decide.performed -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDecide;
                @Decide.canceled -= m_Wrapper.m_SelectorActionsCallbackInterface.OnDecide;
            }
            m_Wrapper.m_SelectorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Option.started += instance.OnOption;
                @Option.performed += instance.OnOption;
                @Option.canceled += instance.OnOption;
                @Decide.started += instance.OnDecide;
                @Decide.performed += instance.OnDecide;
                @Decide.canceled += instance.OnDecide;
            }
        }
    }
    public SelectorActions @Selector => new SelectorActions(this);

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Scratch;
    private readonly InputAction m_Gameplay_Note1;
    private readonly InputAction m_Gameplay_Note2;
    private readonly InputAction m_Gameplay_Note3;
    private readonly InputAction m_Gameplay_Note4;
    private readonly InputAction m_Gameplay_Note5;
    private readonly InputAction m_Gameplay_Note6;
    private readonly InputAction m_Gameplay_Note7;
    private readonly InputAction m_Gameplay_SpeedUp;
    private readonly InputAction m_Gameplay_SpeedDown;
    public struct GameplayActions
    {
        private @NewInput m_Wrapper;
        public GameplayActions(@NewInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Scratch => m_Wrapper.m_Gameplay_Scratch;
        public InputAction @Note1 => m_Wrapper.m_Gameplay_Note1;
        public InputAction @Note2 => m_Wrapper.m_Gameplay_Note2;
        public InputAction @Note3 => m_Wrapper.m_Gameplay_Note3;
        public InputAction @Note4 => m_Wrapper.m_Gameplay_Note4;
        public InputAction @Note5 => m_Wrapper.m_Gameplay_Note5;
        public InputAction @Note6 => m_Wrapper.m_Gameplay_Note6;
        public InputAction @Note7 => m_Wrapper.m_Gameplay_Note7;
        public InputAction @SpeedUp => m_Wrapper.m_Gameplay_SpeedUp;
        public InputAction @SpeedDown => m_Wrapper.m_Gameplay_SpeedDown;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Scratch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScratch;
                @Scratch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScratch;
                @Scratch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScratch;
                @Note1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote1;
                @Note1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote1;
                @Note1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote1;
                @Note2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote2;
                @Note2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote2;
                @Note2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote2;
                @Note3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote3;
                @Note3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote3;
                @Note3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote3;
                @Note4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote4;
                @Note4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote4;
                @Note4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote4;
                @Note5.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote5;
                @Note5.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote5;
                @Note5.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote5;
                @Note6.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote6;
                @Note6.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote6;
                @Note6.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote6;
                @Note7.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote7;
                @Note7.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote7;
                @Note7.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNote7;
                @SpeedUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedUp;
                @SpeedDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedDown;
                @SpeedDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedDown;
                @SpeedDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpeedDown;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Scratch.started += instance.OnScratch;
                @Scratch.performed += instance.OnScratch;
                @Scratch.canceled += instance.OnScratch;
                @Note1.started += instance.OnNote1;
                @Note1.performed += instance.OnNote1;
                @Note1.canceled += instance.OnNote1;
                @Note2.started += instance.OnNote2;
                @Note2.performed += instance.OnNote2;
                @Note2.canceled += instance.OnNote2;
                @Note3.started += instance.OnNote3;
                @Note3.performed += instance.OnNote3;
                @Note3.canceled += instance.OnNote3;
                @Note4.started += instance.OnNote4;
                @Note4.performed += instance.OnNote4;
                @Note4.canceled += instance.OnNote4;
                @Note5.started += instance.OnNote5;
                @Note5.performed += instance.OnNote5;
                @Note5.canceled += instance.OnNote5;
                @Note6.started += instance.OnNote6;
                @Note6.performed += instance.OnNote6;
                @Note6.canceled += instance.OnNote6;
                @Note7.started += instance.OnNote7;
                @Note7.performed += instance.OnNote7;
                @Note7.canceled += instance.OnNote7;
                @SpeedUp.started += instance.OnSpeedUp;
                @SpeedUp.performed += instance.OnSpeedUp;
                @SpeedUp.canceled += instance.OnSpeedUp;
                @SpeedDown.started += instance.OnSpeedDown;
                @SpeedDown.performed += instance.OnSpeedDown;
                @SpeedDown.canceled += instance.OnSpeedDown;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_MidiSchemeIndex = -1;
    public InputControlScheme MidiScheme
    {
        get
        {
            if (m_MidiSchemeIndex == -1) m_MidiSchemeIndex = asset.FindControlSchemeIndex("Midi");
            return asset.controlSchemes[m_MidiSchemeIndex];
        }
    }
    public interface ISelectorActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnOption(InputAction.CallbackContext context);
        void OnDecide(InputAction.CallbackContext context);
    }
    public interface IGameplayActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnScratch(InputAction.CallbackContext context);
        void OnNote1(InputAction.CallbackContext context);
        void OnNote2(InputAction.CallbackContext context);
        void OnNote3(InputAction.CallbackContext context);
        void OnNote4(InputAction.CallbackContext context);
        void OnNote5(InputAction.CallbackContext context);
        void OnNote6(InputAction.CallbackContext context);
        void OnNote7(InputAction.CallbackContext context);
        void OnSpeedUp(InputAction.CallbackContext context);
        void OnSpeedDown(InputAction.CallbackContext context);
    }
}
