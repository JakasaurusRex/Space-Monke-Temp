//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input System/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""LaunchScene"",
            ""id"": ""c0600a2b-58de-423c-96ae-39dc49e87e59"",
            ""actions"": [
                {
                    ""name"": ""Angling"",
                    ""type"": ""Button"",
                    ""id"": ""e9f26950-1acc-4e4a-b3e6-3f375f937aa2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Launching"",
                    ""type"": ""Button"",
                    ""id"": ""17b18ee6-ef63-49d6-956e-c26d915f7f6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""3f444877-e150-4e4c-ba15-89c2576ff1ac"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Angling"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4edd480f-78f8-409f-9c2a-edcabd1d887e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Angling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d74322f0-9c33-4e55-a80f-59e24c5f4654"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Angling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e13dd108-2084-4bf0-b60d-b09edbaf9878"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Launching"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EncounterScene"",
            ""id"": ""63565bf2-44e2-4e2d-abd1-3ccf62810ece"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0d8892a2-de2b-4631-92a8-0e3d3d2ecf21"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""638b232b-0baf-4f45-afd7-9ce26fb54032"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""2eaae2a6-4e17-490d-a5e0-b52c470d529b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0678d126-78a7-4b2e-b14f-b3732a15e027"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""203ecd14-c7e9-494d-b3ca-63dccff91ed7"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""79c85006-258c-4e0c-bab7-1c34eb03ec44"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // LaunchScene
        m_LaunchScene = asset.FindActionMap("LaunchScene", throwIfNotFound: true);
        m_LaunchScene_Angling = m_LaunchScene.FindAction("Angling", throwIfNotFound: true);
        m_LaunchScene_Launching = m_LaunchScene.FindAction("Launching", throwIfNotFound: true);

        // EncounterScene
        m_EncounterScene = asset.FindActionMap("EncounterScene", throwIfNotFound: true);
        m_EncounterScene_Movement = m_EncounterScene.FindAction("Movement", throwIfNotFound: true);
        m_EncounterScene_Shoot = m_EncounterScene.FindAction("Shoot", throwIfNotFound: true);
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

    // LaunchScene
    private readonly InputActionMap m_LaunchScene;
    private ILaunchSceneActions m_LaunchSceneActionsCallbackInterface;
    private readonly InputAction m_LaunchScene_Angling;
    private readonly InputAction m_LaunchScene_Launching;
    public struct LaunchSceneActions
    {
        private @PlayerControls m_Wrapper;
        public LaunchSceneActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Angling => m_Wrapper.m_LaunchScene_Angling;
        public InputAction @Launching => m_Wrapper.m_LaunchScene_Launching;
        public InputActionMap Get() { return m_Wrapper.m_LaunchScene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LaunchSceneActions set) { return set.Get(); }
        public void SetCallbacks(ILaunchSceneActions instance)
        {
            if (m_Wrapper.m_LaunchSceneActionsCallbackInterface != null)
            {
                @Angling.started -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnAngling;
                @Angling.performed -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnAngling;
                @Angling.canceled -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnAngling;
                @Launching.started -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnLaunching;
                @Launching.performed -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnLaunching;
                @Launching.canceled -= m_Wrapper.m_LaunchSceneActionsCallbackInterface.OnLaunching;
            }
            m_Wrapper.m_LaunchSceneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Angling.started += instance.OnAngling;
                @Angling.performed += instance.OnAngling;
                @Angling.canceled += instance.OnAngling;
                @Launching.started += instance.OnLaunching;
                @Launching.performed += instance.OnLaunching;
                @Launching.canceled += instance.OnLaunching;
            }
        }
    }
    public LaunchSceneActions @LaunchScene => new LaunchSceneActions(this);

    // EncounterScene
    private readonly InputActionMap m_EncounterScene;
    private IEncounterSceneActions m_EncounterSceneActionsCallbackInterface;
    private readonly InputAction m_EncounterScene_Movement;
    private readonly InputAction m_EncounterScene_Shoot;
    public struct EncounterSceneActions
    {
        private @PlayerControls m_Wrapper;
        public EncounterSceneActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_EncounterScene_Movement;
        public InputAction @Shoot => m_Wrapper.m_EncounterScene_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_EncounterScene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EncounterSceneActions set) { return set.Get(); }
        public void SetCallbacks(IEncounterSceneActions instance)
        {
            if (m_Wrapper.m_EncounterSceneActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnMovement;
                @Shoot.started -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_EncounterSceneActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_EncounterSceneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public EncounterSceneActions @EncounterScene => new EncounterSceneActions(this);
    public interface ILaunchSceneActions
    {
        void OnAngling(InputAction.CallbackContext context);
        void OnLaunching(InputAction.CallbackContext context);
    }
    public interface IEncounterSceneActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
