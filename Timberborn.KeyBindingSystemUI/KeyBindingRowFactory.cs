using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.KeyBindingSystem;
using Timberborn.LocalizationSerialization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000008 RID: 8
	public class KeyBindingRowFactory
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000023DE File Offset: 0x000005DE
		public KeyBindingRowFactory(KeyBindingGroupSpecService keyBindingGroupSpecService, KeyBindingRegistry keyBindingRegistry, KeyBindingShortcutService keyBindingShortcutService, KeyRebinder keyRebinder, VisualElementLoader visualElementLoader)
		{
			this._keyBindingGroupSpecService = keyBindingGroupSpecService;
			this._keyBindingRegistry = keyBindingRegistry;
			this._keyBindingShortcutService = keyBindingShortcutService;
			this._keyRebinder = keyRebinder;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000240B File Offset: 0x0000060B
		public IEnumerable<KeyBindingGroup> CreateAll()
		{
			foreach (KeyBindingGroupSpec keyBindingGroupSpec in this._keyBindingGroupSpecService.KeyBindingGroupSpecs)
			{
				yield return this.CreateGroup(keyBindingGroupSpec);
			}
			List<KeyBindingGroupSpec>.Enumerator enumerator = default(List<KeyBindingGroupSpec>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000241C File Offset: 0x0000061C
		public KeyBindingGroup CreateGroup(KeyBindingGroupSpec keyBindingGroupSpec)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Options/KeyBindingGroup");
			UQueryExtensions.Q<Label>(visualElement, "Header", null).text = KeyBindingRowFactory.GetDisplayName(keyBindingGroupSpec);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(visualElement, "Items", null);
			foreach (KeyBinding keyBinding in this._keyBindingRegistry.KeyBindings)
			{
				if (keyBinding.GroupId == keyBindingGroupSpec.Id)
				{
					this.CreateKeyBindingRow(keyBinding, parent);
				}
			}
			return new KeyBindingGroup(visualElement, keyBindingGroupSpec);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024CC File Offset: 0x000006CC
		public void CreateKeyBindingRow(KeyBinding keyBinding, VisualElement parent)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Options/KeyBindingRow");
			UQueryExtensions.Q<Label>(visualElement, "Name", null).text = keyBinding.DisplayName;
			parent.Add(visualElement);
			this.CreateElement(visualElement, keyBinding, true);
			this.CreateElement(visualElement, keyBinding, false);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000251C File Offset: 0x0000071C
		public void CreateElement(VisualElement root, KeyBinding keyBinding, bool isPrimary)
		{
			DefinableInputBinding definableInputBinding = new DefinableInputBinding(keyBinding, new bool?(isPrimary));
			Button button = UQueryExtensions.Q<Button>(root, isPrimary ? "PrimaryInput" : "SecondaryInput", null);
			if (definableInputBinding.GetSingleInputBinding().InputBindingSpec.Unchangeable)
			{
				button.AddToClassList(KeyBindingRowFactory.DisabledElementClass);
				button.SetEnabled(false);
			}
			else
			{
				button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					this._keyRebinder.StartRebinding(definableInputBinding);
				}, 0);
			}
			this._keyBindingShortcutService.CreateSingle(button, definableInputBinding);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025B0 File Offset: 0x000007B0
		public static string GetDisplayName(KeyBindingGroupSpec keyBindingGroupSpec)
		{
			LocalizedText displayName = keyBindingGroupSpec.DisplayName;
			string id = keyBindingGroupSpec.Id;
			if (displayName == null)
			{
				if (!keyBindingGroupSpec.IsHiddenGroup)
				{
					Debug.LogWarning("Loc key not defined for key binding group: " + id);
				}
				return "<color=\"orange\">" + id + "</color>";
			}
			return displayName.Value;
		}

		// Token: 0x0400000F RID: 15
		public static readonly string DisabledElementClass = "disabled";

		// Token: 0x04000010 RID: 16
		public readonly KeyBindingGroupSpecService _keyBindingGroupSpecService;

		// Token: 0x04000011 RID: 17
		public readonly KeyBindingRegistry _keyBindingRegistry;

		// Token: 0x04000012 RID: 18
		public readonly KeyBindingShortcutService _keyBindingShortcutService;

		// Token: 0x04000013 RID: 19
		public readonly KeyRebinder _keyRebinder;

		// Token: 0x04000014 RID: 20
		public readonly VisualElementLoader _visualElementLoader;
	}
}
