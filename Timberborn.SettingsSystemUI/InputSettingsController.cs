using System;
using Timberborn.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000011 RID: 17
	public class InputSettingsController
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002E89 File Offset: 0x00001089
		public InputSettingsController(InputSettings inputSettings)
		{
			this._inputSettings = inputSettings;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E98 File Offset: 0x00001098
		public void Initialize(VisualElement root)
		{
			this._invertZoomToggle = UQueryExtensions.Q<Toggle>(root, "InvertZoom", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._invertZoomToggle, delegate(ChangeEvent<bool> v)
			{
				this._inputSettings.InvertZoom = v.newValue;
			});
			this._swapMouseToggle = UQueryExtensions.Q<Toggle>(root, "SwapMouseCameraMovementWithRotation", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._swapMouseToggle, delegate(ChangeEvent<bool> v)
			{
				this._inputSettings.SwapMouseCameraMovementWithRotation = v.newValue;
			});
			this._dragCameraToggle = UQueryExtensions.Q<Toggle>(root, "DragCamera", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._dragCameraToggle, delegate(ChangeEvent<bool> v)
			{
				this._inputSettings.DragCamera = v.newValue;
			});
			this._lockCursorInWindowToggle = UQueryExtensions.Q<Toggle>(root, "LockCursorInWindow", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._lockCursorInWindowToggle, delegate(ChangeEvent<bool> v)
			{
				this._inputSettings.LockCursorInWindow = v.newValue;
			});
			this._edgePanCameraToggle = UQueryExtensions.Q<Toggle>(root, "EdgePanCamera", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._edgePanCameraToggle, delegate(ChangeEvent<bool> v)
			{
				this._inputSettings.EdgePanCamera = v.newValue;
			});
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "EdgePanCameraSpeed", null);
			this._edgePanCameraSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement, "Value", null);
			this._edgePanCameraSpeedSlider = InputSettingsController.InitializeSlider(visualElement, this._edgePanCameraSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.EdgePanCameraSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(root, "KeyboardCameraMovementSpeed", null);
			this._keyboardCameraMovementSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement2, "Value", null);
			this._keyboardCameraMovementSpeedSlider = InputSettingsController.InitializeSlider(visualElement2, this._keyboardCameraMovementSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.KeyboardCameraMovementSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
			VisualElement visualElement3 = UQueryExtensions.Q<VisualElement>(root, "KeyboardCameraRotationSpeed", null);
			this._keyboardCameraRotationSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement3, "Value", null);
			this._keyboardCameraRotationSpeedSlider = InputSettingsController.InitializeSlider(visualElement3, this._keyboardCameraRotationSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.KeyboardCameraRotationSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
			VisualElement visualElement4 = UQueryExtensions.Q<VisualElement>(root, "KeyboardCameraZoomSpeed", null);
			this._keyboardCameraZoomSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement4, "Value", null);
			this._keyboardCameraZoomSpeedSlider = InputSettingsController.InitializeSlider(visualElement4, this._keyboardCameraZoomSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.KeyboardCameraZoomSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
			VisualElement visualElement5 = UQueryExtensions.Q<VisualElement>(root, "MouseWheelCameraZoomSpeed", null);
			this._mouseWheelCameraZoomSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement5, "Value", null);
			this._mouseWheelCameraZoomSpeedSlider = InputSettingsController.InitializeSlider(visualElement5, this._mouseWheelCameraZoomSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.MouseWheelCameraZoomSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
			VisualElement visualElement6 = UQueryExtensions.Q<VisualElement>(root, "MouseCameraRotationSpeed", null);
			this._mouseCameraRotationSpeedValueLabel = UQueryExtensions.Q<Label>(visualElement6, "Value", null);
			this._mouseCameraRotationSpeedSlider = InputSettingsController.InitializeSlider(visualElement6, this._mouseCameraRotationSpeedValueLabel, delegate(float v)
			{
				this._inputSettings.MouseCameraRotationSpeed = v;
			}, InputSettingsController.ReverseUIMultiplier);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000310C File Offset: 0x0000130C
		public void Update()
		{
			this._invertZoomToggle.SetValueWithoutNotify(this._inputSettings.InvertZoom);
			this._swapMouseToggle.SetValueWithoutNotify(this._inputSettings.SwapMouseCameraMovementWithRotation);
			this._dragCameraToggle.SetValueWithoutNotify(this._inputSettings.DragCamera);
			this._lockCursorInWindowToggle.SetValueWithoutNotify(this._inputSettings.LockCursorInWindow);
			this._edgePanCameraToggle.SetValueWithoutNotify(this._inputSettings.EdgePanCamera);
			InputSettingsController.UpdateSlider(this._edgePanCameraSpeedSlider, this._edgePanCameraSpeedValueLabel, this._inputSettings.EdgePanCameraSpeed);
			InputSettingsController.UpdateSlider(this._keyboardCameraMovementSpeedSlider, this._keyboardCameraMovementSpeedValueLabel, this._inputSettings.KeyboardCameraMovementSpeed);
			InputSettingsController.UpdateSlider(this._keyboardCameraRotationSpeedSlider, this._keyboardCameraRotationSpeedValueLabel, this._inputSettings.KeyboardCameraRotationSpeed);
			InputSettingsController.UpdateSlider(this._keyboardCameraZoomSpeedSlider, this._keyboardCameraZoomSpeedValueLabel, this._inputSettings.KeyboardCameraZoomSpeed);
			InputSettingsController.UpdateSlider(this._mouseWheelCameraZoomSpeedSlider, this._mouseWheelCameraZoomSpeedValueLabel, this._inputSettings.MouseWheelCameraZoomSpeed);
			InputSettingsController.UpdateSlider(this._mouseCameraRotationSpeedSlider, this._mouseCameraRotationSpeedValueLabel, this._inputSettings.MouseCameraRotationSpeed);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000322F File Offset: 0x0000142F
		public static float ReverseUIMultiplier
		{
			get
			{
				return 1f / InputSettingsController.UIValueMultiplier;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000323C File Offset: 0x0000143C
		public static Slider InitializeSlider(VisualElement root, TextElement valueLabel, Action<float> setter, float multiplier)
		{
			Slider slider = UQueryExtensions.Q<Slider>(root, "Slider", null);
			slider.lowValue = 0f;
			slider.highValue = InputSettingsController.MaxSliderValue;
			INotifyValueChangedExtensions.RegisterValueChangedCallback<float>(slider, delegate(ChangeEvent<float> v)
			{
				setter(Mathf.Clamp(v.newValue * multiplier, 0f, InputSettingsController.MaxSliderValue));
				valueLabel.text = v.newValue.ToString("P0");
			});
			return slider;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000329C File Offset: 0x0000149C
		public static void UpdateSlider(Slider slider, Label label, float value)
		{
			float num = Mathf.Clamp(value, 0f, InputSettingsController.MaxSliderValue);
			slider.SetValueWithoutNotify(num * InputSettingsController.UIValueMultiplier);
			label.text = (InputSettingsController.UIValueMultiplier * num).ToString("P0");
		}

		// Token: 0x0400004A RID: 74
		public static readonly float MaxSliderValue = 3f;

		// Token: 0x0400004B RID: 75
		public static readonly float UIValueMultiplier = 2.5f;

		// Token: 0x0400004C RID: 76
		public readonly InputSettings _inputSettings;

		// Token: 0x0400004D RID: 77
		public Toggle _invertZoomToggle;

		// Token: 0x0400004E RID: 78
		public Toggle _swapMouseToggle;

		// Token: 0x0400004F RID: 79
		public Toggle _dragCameraToggle;

		// Token: 0x04000050 RID: 80
		public Toggle _lockCursorInWindowToggle;

		// Token: 0x04000051 RID: 81
		public Toggle _edgePanCameraToggle;

		// Token: 0x04000052 RID: 82
		public Label _edgePanCameraSpeedValueLabel;

		// Token: 0x04000053 RID: 83
		public Slider _edgePanCameraSpeedSlider;

		// Token: 0x04000054 RID: 84
		public Label _keyboardCameraMovementSpeedValueLabel;

		// Token: 0x04000055 RID: 85
		public Slider _keyboardCameraMovementSpeedSlider;

		// Token: 0x04000056 RID: 86
		public Label _keyboardCameraRotationSpeedValueLabel;

		// Token: 0x04000057 RID: 87
		public Slider _keyboardCameraRotationSpeedSlider;

		// Token: 0x04000058 RID: 88
		public Label _keyboardCameraZoomSpeedValueLabel;

		// Token: 0x04000059 RID: 89
		public Slider _keyboardCameraZoomSpeedSlider;

		// Token: 0x0400005A RID: 90
		public Label _mouseWheelCameraZoomSpeedValueLabel;

		// Token: 0x0400005B RID: 91
		public Slider _mouseWheelCameraZoomSpeedSlider;

		// Token: 0x0400005C RID: 92
		public Label _mouseCameraRotationSpeedValueLabel;

		// Token: 0x0400005D RID: 93
		public Slider _mouseCameraRotationSpeedSlider;
	}
}
