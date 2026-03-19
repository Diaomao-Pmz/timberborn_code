using System;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Modding;
using Timberborn.ModdingUI;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuModdingUI
{
	// Token: 0x02000007 RID: 7
	public class MainMenuModItemFactory : IModItemFactory
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000251F File Offset: 0x0000071F
		public MainMenuModItemFactory(IModManagerTooltipRegistrar modManagerTooltipRegistrar, VisualElementLoader visualElementLoader, InputService inputService)
		{
			this._modManagerTooltipRegistrar = modManagerTooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
			this._inputService = inputService;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000253C File Offset: 0x0000073C
		public ModItem CreateModItem(Mod mod, Action<Mod, bool> onPriorityIncreased, Action<Mod, bool> onPriorityDecreased)
		{
			VisualElement root = this._visualElementLoader.LoadVisualElement("Modding/ModItem");
			ModItem modItem = new ModItem(this._modManagerTooltipRegistrar, root, mod, new Func<bool>(this.IsAlternateKeyHeld));
			modItem.Initialize(onPriorityIncreased, onPriorityDecreased);
			return modItem;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000257B File Offset: 0x0000077B
		public bool IsAlternateKeyHeld()
		{
			return this._inputService.IsKeyHeld(MainMenuModItemFactory.AlternateClickableActionKey);
		}

		// Token: 0x0400001B RID: 27
		public static readonly string AlternateClickableActionKey = "AlternateClickableAction";

		// Token: 0x0400001C RID: 28
		public readonly IModManagerTooltipRegistrar _modManagerTooltipRegistrar;

		// Token: 0x0400001D RID: 29
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001E RID: 30
		public readonly InputService _inputService;
	}
}
