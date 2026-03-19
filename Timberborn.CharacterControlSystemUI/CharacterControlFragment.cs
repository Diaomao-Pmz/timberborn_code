using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BehaviorSystem;
using Timberborn.CharacterControlSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystem;
using Timberborn.InputSystemUI;
using Timberborn.KeyBindingSystemUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CharacterControlSystemUI
{
	// Token: 0x02000006 RID: 6
	public class CharacterControlFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002270 File Offset: 0x00000470
		public CharacterControlFragment(DebugFragmentFactory debugFragmentFactory, VisualElementLoader visualElementLoader, InputService inputService, CursorService cursorService, CharacterControlDestinationPicker characterControlDestinationPicker, DropdownItemsSetter dropdownItemsSetter, InputBindingDescriber inputBindingDescriber, BindableButtonFactory bindableButtonFactory)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
			this._cursorService = cursorService;
			this._characterControlDestinationPicker = characterControlDestinationPicker;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._inputBindingDescriber = inputBindingDescriber;
			this._bindableButtonFactory = bindableButtonFactory;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C0 File Offset: 0x000004C0
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create("CharacterControl");
			string elementName = "Game/EntityPanel/CharacterControlFragment";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<VisualElement>(this._root, "Content", null).Add(visualElement);
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._animations = UQueryExtensions.Q<Dropdown>(this._root, "Animations", null);
			this._forcedWalking = UQueryExtensions.Q<Toggle>(this._root, "ForcedWalking", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._forcedWalking, new EventCallback<ChangeEvent<bool>>(this.OnForcedWalkingChanged));
			Button button = UQueryExtensions.Q<Button>(visualElement, "MoveTo", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.PickCoordinates();
			}, 0);
			Button button2 = button;
			button2.text = button2.text + " [" + this._inputBindingDescriber.GetInputBindingText(CharacterControlFragment.CharacterControlPickCoordinatesKey) + "]";
			this._moveButton = this._bindableButtonFactory.Create(button, CharacterControlFragment.CharacterControlPickCoordinatesKey, new Action(this.PickCoordinates), true);
			this._release = UQueryExtensions.Q<Button>(visualElement, "Release", null);
			this._release.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ReleaseControl), 0);
			return this._root;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000240C File Offset: 0x0000060C
		public void ShowFragment(BaseComponent entity)
		{
			this._controllableCharacter = entity.GetComponent<ControllableCharacter>();
			if (this._controllableCharacter)
			{
				this._behaviorManager = entity.GetComponent<BehaviorManager>();
				this._forcedWalking.SetValueWithoutNotify(this._controllableCharacter.ForcedWalking);
				this.InitializeAnimations();
				this._inputService.AddInputProcessor(this);
				this._moveButton.Bind();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002474 File Offset: 0x00000674
		public void ClearFragment()
		{
			this._inputService.RemoveInputProcessor(this);
			this._moveButton.Unbind();
			this._root.ToggleDisplayStyle(false);
			this._animations.ClearItems();
			this._controllableCharacter = null;
			this._behaviorManager = null;
			this._text.text = "";
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024D0 File Offset: 0x000006D0
		public void UpdateFragment()
		{
			if (this._controllableCharacter)
			{
				bool underControl = this._controllableCharacter.UnderControl;
				if (underControl && !this._pickingCoordinates)
				{
					string name = this._behaviorManager.RunningBehavior.Name;
					this._text.text = ((name != "CharacterControlRootBehavior") ? ("Waiting for: " + name) : "Executing command");
				}
				this._release.SetEnabled(underControl);
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002564 File Offset: 0x00000764
		public bool ProcessInput()
		{
			if (this._pickingCoordinates)
			{
				if (this._inputService.MainMouseButtonDown && !this._inputService.MouseOverUI)
				{
					Vector3? vector = this._characterControlDestinationPicker.PickDestination();
					if (vector != null)
					{
						Vector3 valueOrDefault = vector.GetValueOrDefault();
						this._controllableCharacter.TakeControlAndMoveTo(valueOrDefault);
					}
				}
				if (this._inputService.MainMouseButtonDown || this._inputService.Cancel)
				{
					this._pickingCoordinates = false;
					this._cursorService.ResetCursor();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025EB File Offset: 0x000007EB
		public void OnForcedWalkingChanged(ChangeEvent<bool> newValue)
		{
			if (newValue.newValue)
			{
				this._controllableCharacter.EnableForcedWalking();
				return;
			}
			this._controllableCharacter.DisableForcedWalking();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000260C File Offset: 0x0000080C
		public void PickCoordinates()
		{
			this._text.text = "Click to pick destination";
			this._pickingCoordinates = true;
			this._cursorService.SetCursor(CharacterControlFragment.CursorKey);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002635 File Offset: 0x00000835
		public void ReleaseControl(ClickEvent evt)
		{
			this._controllableCharacter.ReleaseControl();
			this._text.text = "";
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002654 File Offset: 0x00000854
		public void InitializeAnimations()
		{
			ControllableCharacterDropdownProvider component = this._controllableCharacter.GetComponent<ControllableCharacterDropdownProvider>();
			this._dropdownItemsSetter.SetItems(this._animations, component);
			component.SetInitialAnimation();
		}

		// Token: 0x0400000D RID: 13
		public static readonly string CursorKey = "PickDestinationCursor";

		// Token: 0x0400000E RID: 14
		public static readonly string CharacterControlPickCoordinatesKey = "CharacterControlPickCoordinates";

		// Token: 0x0400000F RID: 15
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000010 RID: 16
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000011 RID: 17
		public readonly InputService _inputService;

		// Token: 0x04000012 RID: 18
		public readonly CursorService _cursorService;

		// Token: 0x04000013 RID: 19
		public readonly CharacterControlDestinationPicker _characterControlDestinationPicker;

		// Token: 0x04000014 RID: 20
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x04000015 RID: 21
		public readonly InputBindingDescriber _inputBindingDescriber;

		// Token: 0x04000016 RID: 22
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x04000017 RID: 23
		public ControllableCharacter _controllableCharacter;

		// Token: 0x04000018 RID: 24
		public BehaviorManager _behaviorManager;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;

		// Token: 0x0400001A RID: 26
		public Label _text;

		// Token: 0x0400001B RID: 27
		public Button _release;

		// Token: 0x0400001C RID: 28
		public Dropdown _animations;

		// Token: 0x0400001D RID: 29
		public Toggle _forcedWalking;

		// Token: 0x0400001E RID: 30
		public bool _pickingCoordinates;

		// Token: 0x0400001F RID: 31
		public BindableButton _moveButton;
	}
}
