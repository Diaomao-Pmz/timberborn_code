using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000A RID: 10
	public class FloodgateFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000028A4 File Offset: 0x00000AA4
		public FloodgateFragment(VisualElementLoader visualElementLoader, ILoc loc, EventBus eventBus, InputService inputService)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._eventBus = eventBus;
			this._inputService = inputService;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028F8 File Offset: 0x00000AF8
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/FloodgateFragment");
			this._height = UQueryExtensions.Q<Label>(this._root, "Height", null);
			this._heightState = UQueryExtensions.Q<Label>(this._root, "HeightState", null);
			this._heightSlider = UQueryExtensions.Q<Slider>(this._root, "HeightSlider", null);
			this._automationHeightWrapper = UQueryExtensions.Q<VisualElement>(this._root, "AutomationHeightWrapper", null);
			this._automationHeight = UQueryExtensions.Q<Label>(this._root, "AutomationHeight", null);
			this._automationHeightState = UQueryExtensions.Q<Label>(this._root, "AutomationHeightState", null);
			this._automationHeightSlider = UQueryExtensions.Q<Slider>(this._root, "AutomationHeightSlider", null);
			this._synchronizeToggle = UQueryExtensions.Q<Toggle>(this._root, "Synchronize", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._heightSlider, new EventCallback<ChangeEvent<float>>(this.OnHeightSliderValueChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(this._automationHeightSlider, new EventCallback<ChangeEvent<float>>(this.OnAutomationHeightSliderValueChanged));
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._synchronizeToggle, new EventCallback<ChangeEvent<bool>>(this.ToggleSynchronization));
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A3C File Offset: 0x00000C3C
		public void ShowFragment(BaseComponent entity)
		{
			Floodgate component = entity.GetComponent<Floodgate>();
			if (component)
			{
				this._heightSlider.highValue = (float)component.MaxHeight;
				this._automationHeightSlider.highValue = (float)component.MaxHeight;
				this._heightSlider.SetValueWithoutNotify(component.Height);
				this._automationHeightSlider.SetValueWithoutNotify(component.AutomationHeight);
				this._inputService.AddInputProcessor(this);
			}
			this._floodgate = component;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void ClearFragment()
		{
			this._floodgate = null;
			this._root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void UpdateFragment()
		{
			if (this._floodgate)
			{
				this._height.text = this._loc.T<float>(this._heightPhrase, this._floodgate.Height);
				this._heightState.ToggleDisplayStyle(this._floodgate.IsAutomated);
				this._automationHeight.text = this._loc.T<float>(this._heightPhrase, this._floodgate.AutomationHeight);
				this._synchronizeToggle.SetValueWithoutNotify(this._floodgate.IsSynchronized);
				this._root.ToggleDisplayStyle(true);
				this._automationHeightWrapper.ToggleDisplayStyle(this._floodgate.IsAutomated);
				this._heightState.EnableInClassList(FloodgateFragment.ActiveStateLabelClass, !this._floodgate.IsInputOn);
				this._automationHeightState.EnableInClassList(FloodgateFragment.ActiveStateLabelClass, this._floodgate.IsInputOn);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyHeld(FloodgateFragment.DecreaseFloodgateHeightKey))
			{
				this.DecreaseHeight();
				return true;
			}
			if (this._inputService.IsKeyUp(FloodgateFragment.DecreaseFloodgateHeightKey) && !this._heightChangedOnHold)
			{
				this.DecreaseHeightIfPossible();
				return true;
			}
			if (this._inputService.IsKeyHeld(FloodgateFragment.IncreaseFloodgateHeightKey))
			{
				this.IncreaseHeight();
				return true;
			}
			if (this._inputService.IsKeyUp(FloodgateFragment.IncreaseFloodgateHeightKey) && !this._heightChangedOnHold)
			{
				this.IncreaseHeightIfPossible();
				return true;
			}
			this._timeSinceLastChange = 0f;
			this._heightChangedOnHold = false;
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002C6C File Offset: 0x00000E6C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			GameObject gameObject = enteredFinishedStateEvent.BlockObject.GameObject;
			if (this._floodgate && this._floodgate.GameObject == gameObject)
			{
				this._heightSlider.SetValueWithoutNotify(this._floodgate.Height);
				this._automationHeightSlider.SetValueWithoutNotify(this._floodgate.AutomationHeight);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public void OnHeightSliderValueChanged(ChangeEvent<float> evt)
		{
			this.ChangeHeight(evt.newValue);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CDF File Offset: 0x00000EDF
		public void OnAutomationHeightSliderValueChanged(ChangeEvent<float> evt)
		{
			this.ChangeAutomationHeight(evt.newValue);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CED File Offset: 0x00000EED
		public void DecreaseHeight()
		{
			if (this._timeSinceLastChange > FloodgateFragment.ChangeTimeThreshold)
			{
				this.DecreaseHeightIfPossible();
				this._heightChangedOnHold = true;
				this._timeSinceLastChange = 0f;
			}
			this._timeSinceLastChange += Time.unscaledDeltaTime;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D26 File Offset: 0x00000F26
		public void IncreaseHeight()
		{
			if (this._timeSinceLastChange > FloodgateFragment.ChangeTimeThreshold)
			{
				this.IncreaseHeightIfPossible();
				this._heightChangedOnHold = true;
				this._timeSinceLastChange = 0f;
			}
			this._timeSinceLastChange += Time.unscaledDeltaTime;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D5F File Offset: 0x00000F5F
		public void DecreaseHeightIfPossible()
		{
			if (this._floodgate.Height > 0f)
			{
				this.ChangeHeight(this._floodgate.Height - FloodgateFragment.HeightChangeStep);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D8A File Offset: 0x00000F8A
		public void IncreaseHeightIfPossible()
		{
			if (this._floodgate.Height < (float)this._floodgate.MaxHeight)
			{
				this.ChangeHeight(this._floodgate.Height + FloodgateFragment.HeightChangeStep);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DBC File Offset: 0x00000FBC
		public void ChangeHeight(float newHeight)
		{
			float num = this.UpdateHeightSliderValue(newHeight);
			if (this._floodgate && this._floodgate.Height != num)
			{
				this._floodgate.SetHeightAndSynchronize(num);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public void ChangeAutomationHeight(float newHeight)
		{
			float num = this.UpdateAutomationHeightSliderValue(newHeight);
			if (this._floodgate && this._floodgate.AutomationHeight != num)
			{
				this._floodgate.SetAutomationHeightAndSynchronize(num);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E34 File Offset: 0x00001034
		public float UpdateHeightSliderValue(float value)
		{
			float num = FloodgateFragment.RoundHeight(value);
			this._heightSlider.SetValueWithoutNotify(num);
			return num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E58 File Offset: 0x00001058
		public float UpdateAutomationHeightSliderValue(float value)
		{
			float num = FloodgateFragment.RoundHeight(value);
			this._automationHeightSlider.SetValueWithoutNotify(num);
			return num;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E79 File Offset: 0x00001079
		public void ToggleSynchronization(ChangeEvent<bool> changeEvent)
		{
			this._floodgate.ToggleSynchronization(changeEvent.newValue);
			this.UpdateHeightSliderValue(this._floodgate.Height);
			this.UpdateAutomationHeightSliderValue(this._floodgate.AutomationHeight);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002EB0 File Offset: 0x000010B0
		public static float RoundHeight(float value)
		{
			return (float)Math.Round((double)(value * 20f)) / 20f;
		}

		// Token: 0x04000025 RID: 37
		public static readonly float ChangeTimeThreshold = 0.1f;

		// Token: 0x04000026 RID: 38
		public static readonly float HeightChangeStep = 0.05f;

		// Token: 0x04000027 RID: 39
		public static readonly string DecreaseFloodgateHeightKey = "DecreaseFloodgateHeight";

		// Token: 0x04000028 RID: 40
		public static readonly string IncreaseFloodgateHeightKey = "IncreaseFloodgateHeight";

		// Token: 0x04000029 RID: 41
		public static readonly string ActiveStateLabelClass = "entity-panel__text--highlight-white";

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly ILoc _loc;

		// Token: 0x0400002C RID: 44
		public readonly EventBus _eventBus;

		// Token: 0x0400002D RID: 45
		public readonly InputService _inputService;

		// Token: 0x0400002E RID: 46
		public readonly Phrase _heightPhrase = Phrase.New("Building.Floodgate.Height").Format<float>(new Func<float, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x0400002F RID: 47
		public VisualElement _root;

		// Token: 0x04000030 RID: 48
		public Label _height;

		// Token: 0x04000031 RID: 49
		public Label _heightState;

		// Token: 0x04000032 RID: 50
		public Slider _heightSlider;

		// Token: 0x04000033 RID: 51
		public VisualElement _automationHeightWrapper;

		// Token: 0x04000034 RID: 52
		public Label _automationHeight;

		// Token: 0x04000035 RID: 53
		public Label _automationHeightState;

		// Token: 0x04000036 RID: 54
		public Slider _automationHeightSlider;

		// Token: 0x04000037 RID: 55
		public Toggle _synchronizeToggle;

		// Token: 0x04000038 RID: 56
		public Floodgate _floodgate;

		// Token: 0x04000039 RID: 57
		public float _timeSinceLastChange;

		// Token: 0x0400003A RID: 58
		public bool _heightChangedOnHold;
	}
}
