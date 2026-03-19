using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Common;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000020 RID: 32
	public class KeyBindingSpecService : ILoadableSingleton
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000041C4 File Offset: 0x000023C4
		public KeyBindingSpecService(CustomInputBindingSerializer customInputBindingSerializer, ISettings settings, ISpecService specService, CtrlKeyOverwriter ctrlKeyOverwriter, EventBus eventBus)
		{
			this._customInputBindingSerializer = customInputBindingSerializer;
			this._settings = settings;
			this._specService = specService;
			this._ctrlKeyOverwriter = ctrlKeyOverwriter;
			this._eventBus = eventBus;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000041FC File Offset: 0x000023FC
		public ReadOnlyList<KeyBindingDefinition> KeyBindingDefinitions
		{
			get
			{
				return this._keyBindingDefinitions.AsReadOnlyList<KeyBindingDefinition>();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000420C File Offset: 0x0000240C
		public void Load()
		{
			foreach (KeyBindingSpec keyBindingSpec in from spec in (from spec in this._specService.GetSpecs<KeyBindingSpec>()
			where spec.Blueprint.IsAllowedByFeatureToggles()
			select spec).Select(new Func<KeyBindingSpec, KeyBindingSpec>(this.Overwrite))
			orderby spec.Order
			select spec)
			{
				this._keyBindingDefinitions.Add(KeyBindingDefinition.Create(keyBindingSpec, this.GetCustomPrimaryBinding(keyBindingSpec), this.GetCustomSecondaryBinding(keyBindingSpec)));
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000042D0 File Offset: 0x000024D0
		public void ResetToDefault()
		{
			foreach (KeyBindingDefinition keyBindingDefinition in this._keyBindingDefinitions)
			{
				string id = keyBindingDefinition.KeyBindingSpec.Id;
				this._settings.Clear(KeyBindingSpecService.GetPrimarySettingsKey(id));
				this._settings.Clear(KeyBindingSpecService.GetSecondarySettingsKey(id));
				keyBindingDefinition.ResetToDefault();
				this._eventBus.Post(new KeyReboundEvent(id));
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004360 File Offset: 0x00002560
		public void RebindInputBinding(DefinableInputBinding definableInputBinding, CustomInputBinding customInputBinding)
		{
			KeyBindingDefinition keyBindingDefinition = this._keyBindingDefinitions.Single((KeyBindingDefinition key) => key.KeyBindingSpec.Id == definableInputBinding.KeyBinding.Id);
			string settingsKey;
			if (definableInputBinding.IsPrimary())
			{
				keyBindingDefinition.RebindPrimaryInputBinding(customInputBinding);
				settingsKey = KeyBindingSpecService.GetPrimarySettingsKey(definableInputBinding.KeyBinding.Id);
			}
			else
			{
				keyBindingDefinition.RebindSecondaryInputBinding(customInputBinding);
				settingsKey = KeyBindingSpecService.GetSecondarySettingsKey(definableInputBinding.KeyBinding.Id);
			}
			this.SaveInputBinding(customInputBinding, settingsKey);
			definableInputBinding.KeyBinding.Lock();
			this._eventBus.Post(new KeyReboundEvent(definableInputBinding.KeyBinding.Id));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004414 File Offset: 0x00002614
		public void SaveInputBinding(CustomInputBinding customInputBinding, string settingsKey)
		{
			string value = this._customInputBindingSerializer.Serialize(customInputBinding);
			this._settings.SetString(settingsKey, value);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000443C File Offset: 0x0000263C
		public CustomInputBinding GetCustomPrimaryBinding(KeyBindingSpec keyBindingSpec)
		{
			string primarySettingsKey = KeyBindingSpecService.GetPrimarySettingsKey(keyBindingSpec.Id);
			return this.GetBindingFromSettings(primarySettingsKey);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000445C File Offset: 0x0000265C
		public CustomInputBinding GetCustomSecondaryBinding(KeyBindingSpec keyBindingSpec)
		{
			string secondarySettingsKey = KeyBindingSpecService.GetSecondarySettingsKey(keyBindingSpec.Id);
			return this.GetBindingFromSettings(secondarySettingsKey);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000447C File Offset: 0x0000267C
		public CustomInputBinding GetBindingFromSettings(string settingsKey)
		{
			string @string = this._settings.GetString(settingsKey, string.Empty);
			if (!string.IsNullOrEmpty(@string))
			{
				return this._customInputBindingSerializer.Deserialize(@string);
			}
			return null;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000044B1 File Offset: 0x000026B1
		public static string GetPrimarySettingsKey(string keyBindingId)
		{
			return string.Format(KeyBindingSpecService.PrimaryBindingFormat, keyBindingId);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000044BE File Offset: 0x000026BE
		public static string GetSecondarySettingsKey(string keyBindingId)
		{
			return string.Format(KeyBindingSpecService.SecondaryBindingFormat, keyBindingId);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000044CC File Offset: 0x000026CC
		public KeyBindingSpec Overwrite(KeyBindingSpec keyBindingSpec)
		{
			ComponentSpec componentSpec = keyBindingSpec;
			PrimaryInputBindingSpec spec = componentSpec.GetSpec<PrimaryInputBindingSpec>();
			if (spec != null)
			{
				componentSpec = this._ctrlKeyOverwriter.OverwriteIfOnMacOS(spec);
			}
			SecondaryInputBindingSpec spec2 = componentSpec.GetSpec<SecondaryInputBindingSpec>();
			if (spec2 != null)
			{
				componentSpec = this._ctrlKeyOverwriter.OverwriteIfOnMacOS(spec2);
			}
			return componentSpec.GetSpec<KeyBindingSpec>();
		}

		// Token: 0x04000061 RID: 97
		public static readonly string PrimaryBindingFormat = "KeyBinding.{0}.Primary";

		// Token: 0x04000062 RID: 98
		public static readonly string SecondaryBindingFormat = "KeyBinding.{0}.Secondary";

		// Token: 0x04000063 RID: 99
		public readonly CustomInputBindingSerializer _customInputBindingSerializer;

		// Token: 0x04000064 RID: 100
		public readonly ISettings _settings;

		// Token: 0x04000065 RID: 101
		public readonly ISpecService _specService;

		// Token: 0x04000066 RID: 102
		public readonly CtrlKeyOverwriter _ctrlKeyOverwriter;

		// Token: 0x04000067 RID: 103
		public readonly EventBus _eventBus;

		// Token: 0x04000068 RID: 104
		public readonly List<KeyBindingDefinition> _keyBindingDefinitions = new List<KeyBindingDefinition>();
	}
}
