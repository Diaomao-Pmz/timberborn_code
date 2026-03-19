using System;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.GraphicsQualitySystem;
using Timberborn.PlatformUtilities;
using UnityEngine.UIElements;

namespace Timberborn.SettingsSystemUI
{
	// Token: 0x02000010 RID: 16
	public class GraphicsSettingsController
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public GraphicsSettingsController(GraphicsQualitySettings graphicsQualitySettings, AntiAliasingDropdownProvider antiAliasingDropdownProvider, DropdownItemsSetter dropdownItemsSetter, GraphicsQualityDropdownProvider graphicsQualityDropdownProvider, LightQualityDropdownProvider lightQualityDropdownProvider, ShadowQualityGraphicsDropdownProvider shadowQualityGraphicsDropdownProvider, TextureQualityDropdownProvider textureQualityDropdownProvider, AnisotropicFilteringDropdownProvider anisotropicFilteringDropdownProvider, WaterQualityDropdownProvider waterQualityDropdownProvider, BloomDropdownProvider bloomDropdownProvider)
		{
			this._graphicsQualitySettings = graphicsQualitySettings;
			this._antiAliasingDropdownProvider = antiAliasingDropdownProvider;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._graphicsQualityDropdownProvider = graphicsQualityDropdownProvider;
			this._lightQualityDropdownProvider = lightQualityDropdownProvider;
			this._shadowQualityGraphicsDropdownProvider = shadowQualityGraphicsDropdownProvider;
			this._textureQualityDropdownProvider = textureQualityDropdownProvider;
			this._anisotropicFilteringDropdownProvider = anisotropicFilteringDropdownProvider;
			this._waterQualityDropdownProvider = waterQualityDropdownProvider;
			this._bloomDropdownProvider = bloomDropdownProvider;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C00 File Offset: 0x00000E00
		public void Initialize(VisualElement root)
		{
			this._anisotropicFilteringDropdown = UQueryExtensions.Q<Dropdown>(root, "AnisotropicFiltering", null);
			this._anisotropicFilteringDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._antiAliasingDropdown = UQueryExtensions.Q<Dropdown>(root, "AntiAliasing", null);
			this._antiAliasingDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._graphicsQualityDropdown = UQueryExtensions.Q<Dropdown>(root, "GraphicsQuality", null);
			this._graphicsQualityDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._lightQualityDropdown = UQueryExtensions.Q<Dropdown>(root, "LightQuality", null);
			this._lightQualityDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._shadowQualityDropdown = UQueryExtensions.Q<Dropdown>(root, "ShadowQuality", null);
			this._shadowQualityDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._textureQualityDropdown = UQueryExtensions.Q<Dropdown>(root, "TextureQuality", null);
			this._textureQualityDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._waterQualityDropdown = UQueryExtensions.Q<Dropdown>(root, "WaterQuality", null);
			this._waterQualityDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._bloomDropdown = UQueryExtensions.Q<Dropdown>(root, "Bloom", null);
			this._bloomDropdown.ValueChanged += delegate(object _, EventArgs _)
			{
				this.UpdateSettings();
			};
			this._macMSAAWarning = UQueryExtensions.Q<VisualElement>(root, "MacMSAAWarning", null);
			this.UpdateSettings();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D70 File Offset: 0x00000F70
		public void UpdateSettings()
		{
			this._dropdownItemsSetter.SetItems(this._anisotropicFilteringDropdown, this._anisotropicFilteringDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._antiAliasingDropdown, this._antiAliasingDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._graphicsQualityDropdown, this._graphicsQualityDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._lightQualityDropdown, this._lightQualityDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._shadowQualityDropdown, this._shadowQualityGraphicsDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._textureQualityDropdown, this._textureQualityDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._waterQualityDropdown, this._waterQualityDropdownProvider);
			this._dropdownItemsSetter.SetItems(this._bloomDropdown, this._bloomDropdownProvider);
			this.UpdateMacMSAAWarning();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E3C File Offset: 0x0000103C
		public void UpdateMacMSAAWarning()
		{
			AntialiasingType antiAliasingType = this._graphicsQualitySettings.AntiAliasingType;
			bool flag = antiAliasingType == AntialiasingType.MSAAx2 || antiAliasingType == AntialiasingType.MSAAx4 || antiAliasingType == AntialiasingType.MSAAx8;
			this._macMSAAWarning.ToggleDisplayStyle(flag && ApplicationPlatform.IsMacOS());
		}

		// Token: 0x04000037 RID: 55
		public readonly GraphicsQualitySettings _graphicsQualitySettings;

		// Token: 0x04000038 RID: 56
		public readonly AntiAliasingDropdownProvider _antiAliasingDropdownProvider;

		// Token: 0x04000039 RID: 57
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400003A RID: 58
		public readonly GraphicsQualityDropdownProvider _graphicsQualityDropdownProvider;

		// Token: 0x0400003B RID: 59
		public readonly LightQualityDropdownProvider _lightQualityDropdownProvider;

		// Token: 0x0400003C RID: 60
		public readonly ShadowQualityGraphicsDropdownProvider _shadowQualityGraphicsDropdownProvider;

		// Token: 0x0400003D RID: 61
		public readonly TextureQualityDropdownProvider _textureQualityDropdownProvider;

		// Token: 0x0400003E RID: 62
		public readonly AnisotropicFilteringDropdownProvider _anisotropicFilteringDropdownProvider;

		// Token: 0x0400003F RID: 63
		public readonly WaterQualityDropdownProvider _waterQualityDropdownProvider;

		// Token: 0x04000040 RID: 64
		public readonly BloomDropdownProvider _bloomDropdownProvider;

		// Token: 0x04000041 RID: 65
		public Dropdown _anisotropicFilteringDropdown;

		// Token: 0x04000042 RID: 66
		public Dropdown _antiAliasingDropdown;

		// Token: 0x04000043 RID: 67
		public Dropdown _graphicsQualityDropdown;

		// Token: 0x04000044 RID: 68
		public Dropdown _lightQualityDropdown;

		// Token: 0x04000045 RID: 69
		public Dropdown _shadowQualityDropdown;

		// Token: 0x04000046 RID: 70
		public Dropdown _textureQualityDropdown;

		// Token: 0x04000047 RID: 71
		public Dropdown _waterQualityDropdown;

		// Token: 0x04000048 RID: 72
		public Dropdown _bloomDropdown;

		// Token: 0x04000049 RID: 73
		public VisualElement _macMSAAWarning;
	}
}
