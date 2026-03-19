using System;
using Timberborn.Modding;
using UnityEngine.UIElements;

namespace Timberborn.ModdingUI
{
	// Token: 0x02000007 RID: 7
	public class ModItem
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000A RID: 10 RVA: 0x000020D4 File Offset: 0x000002D4
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x0000210C File Offset: 0x0000030C
		public event EventHandler ModToggled;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002141 File Offset: 0x00000341
		public VisualElement Root { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002149 File Offset: 0x00000349
		public Mod Mod { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002151 File Offset: 0x00000351
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002159 File Offset: 0x00000359
		public ModWarningReason WarningReason { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002162 File Offset: 0x00000362
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000216A File Offset: 0x0000036A
		public string WarningInfo { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002173 File Offset: 0x00000373
		public ModItem(IModManagerTooltipRegistrar modManagerTooltipRegistrar, VisualElement root, Mod mod, Func<bool> priorityChangeModifier)
		{
			this._modManagerTooltipRegistrar = modManagerTooltipRegistrar;
			this.Root = root;
			this.Mod = mod;
			this._priorityChangeModifier = priorityChangeModifier;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002198 File Offset: 0x00000398
		public ModManifest ModManifest
		{
			get
			{
				return this.Mod.Manifest;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021A5 File Offset: 0x000003A5
		public void Update()
		{
			this.Root.EnableInClassList(ModItem.PriorityModifierClass, this._priorityChangeModifier());
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C4 File Offset: 0x000003C4
		public void Initialize(Action<Mod, bool> onPriorityIncreased, Action<Mod, bool> onPriorityDecreased)
		{
			this._warningIcon = UQueryExtensions.Q<VisualElement>(this.Root, "WarningIcon", null);
			Toggle toggle = UQueryExtensions.Q<Toggle>(this.Root, "ModToggle", null);
			toggle.value = ModPlayerPrefsHelper.IsModEnabled(this.Mod);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> evt)
			{
				this.ToggleMod(evt.newValue);
			});
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(this.Root, "ModIcon", null);
			visualElement.AddToClassList(this.Mod.ModDirectory.IsUserMod ? ModItem.LocalModIconClass : ModItem.CloudModIconClass);
			this._modManagerTooltipRegistrar.RegisterModIcon(visualElement, this);
			Label label = UQueryExtensions.Q<Label>(this.Root, "ModName", null);
			label.enableRichText = false;
			label.text = this.Mod.DisplayName;
			UQueryExtensions.Q<Label>(this.Root, "ModVersion", null).text = this.ModManifest.Version.Formatted;
			this._modManagerTooltipRegistrar.RegisterModWarning(this._warningIcon, this);
			Button button = UQueryExtensions.Q<Button>(this.Root, "Increase", null);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				onPriorityIncreased(this.Mod, this._priorityChangeModifier());
			}, 0);
			this._modManagerTooltipRegistrar.RegisterIncreaseButton(button);
			Button button2 = UQueryExtensions.Q<Button>(this.Root, "Decrease", null);
			button2.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				onPriorityDecreased(this.Mod, this._priorityChangeModifier());
			}, 0);
			this._modManagerTooltipRegistrar.RegisterDecreaseButton(button2);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002344 File Offset: 0x00000544
		public void SetWarning(ModWarningReason warningReason, string warningInfo)
		{
			this.WarningReason = warningReason;
			this.WarningInfo = warningInfo;
			this._warningIcon.style.display = ((this.WarningReason == ModWarningReason.None) ? 1 : 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002375 File Offset: 0x00000575
		public void ToggleMod(bool isEnabled)
		{
			ModPlayerPrefsHelper.ToggleMod(isEnabled, this.Mod);
			EventHandler modToggled = this.ModToggled;
			if (modToggled == null)
			{
				return;
			}
			modToggled(this, EventArgs.Empty);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string LocalModIconClass = "mod-item__icon--local";

		// Token: 0x04000007 RID: 7
		public static readonly string CloudModIconClass = "mod-item__icon--cloud";

		// Token: 0x04000008 RID: 8
		public static readonly string PriorityModifierClass = "mod-item__priority-modifier";

		// Token: 0x0400000E RID: 14
		public readonly IModManagerTooltipRegistrar _modManagerTooltipRegistrar;

		// Token: 0x0400000F RID: 15
		public readonly Func<bool> _priorityChangeModifier;

		// Token: 0x04000010 RID: 16
		public VisualElement _warningIcon;
	}
}
