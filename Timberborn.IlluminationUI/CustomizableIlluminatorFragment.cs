using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Illumination;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.IlluminationUI
{
	// Token: 0x02000007 RID: 7
	public class CustomizableIlluminatorFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public CustomizableIlluminatorFragment(VisualElementLoader visualElementLoader, IlluminationService illuminationService)
		{
			this._visualElementLoader = visualElementLoader;
			this._illuminationService = illuminationService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/CustomizableIlluminatorFragment");
			this._root.ToggleDisplayStyle(false);
			this._rgbTextField = UQueryExtensions.Q<TextField>(this._root, "Rgb", null);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<string>(this._rgbTextField, new EventCallback<ChangeEvent<string>>(this.OnRgbValueChanged));
			this._rgbTextField.isDelayed = true;
			this.InitializePresets();
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002196 File Offset: 0x00000396
		public void ShowFragment(BaseComponent entity)
		{
			this._customizableIlluminator = entity.GetComponent<CustomizableIlluminator>();
			if (this._customizableIlluminator)
			{
				this.UpdateCustomColor();
				this._customizableIlluminator.CustomColorChanged += this.OnCustomColorChanged;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CE File Offset: 0x000003CE
		public void ClearFragment()
		{
			if (this._customizableIlluminator != null)
			{
				this._customizableIlluminator.CustomColorChanged -= this.OnCustomColorChanged;
			}
			this._customizableIlluminator = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002204 File Offset: 0x00000404
		public void UpdateFragment()
		{
			if (this._customizableIlluminator && this._customizableIlluminator.IsCustomized && !this._customizableIlluminator.IsLocked)
			{
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002254 File Offset: 0x00000454
		public void InitializePresets()
		{
			VisualElement visualElement = UQueryExtensions.Q(this._root, "Presets", null);
			ImmutableArray<Color>.Enumerator enumerator = this._illuminationService.PresetColors.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Color presetColor = enumerator.Current;
				Button button = (Button)this._visualElementLoader.LoadVisualElement("Game/EntityPanel/CustomizableIlluminatorPreset");
				UQueryExtensions.Q<Image>(button, "Image", null).style.unityBackgroundImageTintColor = presetColor;
				button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					this.SetCustomColorToPreset(presetColor);
				}, 0);
				visualElement.Add(button);
				this._presetColorButtons.Add(new ValueTuple<Color, Button>(presetColor, button));
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002320 File Offset: 0x00000520
		public void OnRgbValueChanged(ChangeEvent<string> changeEvent)
		{
			Color customColor;
			if (ColorUtility.TryParseHtmlString("#" + changeEvent.newValue, ref customColor) || ColorUtility.TryParseHtmlString(changeEvent.newValue, ref customColor))
			{
				this._customizableIlluminator.SetCustomColor(customColor);
			}
			this.UpdateCustomColor();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002367 File Offset: 0x00000567
		public void OnCustomColorChanged(object sender, EventArgs e)
		{
			this.UpdateCustomColor();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000236F File Offset: 0x0000056F
		public void SetCustomColorToPreset(Color presetColor)
		{
			this._customizableIlluminator.SetCustomColor(presetColor);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002380 File Offset: 0x00000580
		public void UpdateCustomColor()
		{
			Color customColor = this._customizableIlluminator.CustomColor;
			this._rgbTextField.SetValueWithoutNotify(ColorUtility.ToHtmlStringRGB(customColor).ToLowerInvariant());
			foreach (ValueTuple<Color, Button> valueTuple in this._presetColorButtons)
			{
				Color item = valueTuple.Item1;
				valueTuple.Item2.EnableInClassList(CustomizableIlluminatorFragment.SelectedPresetButtonClass, CustomizableIlluminatorFragment.ColorsAreTheSame(item, customColor));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000240C File Offset: 0x0000060C
		public static bool ColorsAreTheSame(Color a, Color b)
		{
			Color32 color = a;
			Color32 color2 = b;
			return color.Equals(color2);
		}

		// Token: 0x04000008 RID: 8
		public static readonly string SelectedPresetButtonClass = "customizable-illuminator-preset--selected";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly IlluminationService _illuminationService;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;

		// Token: 0x0400000C RID: 12
		public TextField _rgbTextField;

		// Token: 0x0400000D RID: 13
		public CustomizableIlluminator _customizableIlluminator;

		// Token: 0x0400000E RID: 14
		public readonly List<ValueTuple<Color, Button>> _presetColorButtons = new List<ValueTuple<Color, Button>>();
	}
}
