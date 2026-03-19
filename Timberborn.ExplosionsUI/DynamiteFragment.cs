using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Explosions;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.ExplosionsUI
{
	// Token: 0x02000006 RID: 6
	public class DynamiteFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021F3 File Offset: 0x000003F3
		public DynamiteFragment(VisualElementLoader visualElementLoader, ILoc loc, InputService inputService, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._inputService = inputService;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002218 File Offset: 0x00000418
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DynamiteFragment");
			this._button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.DetonateSelectedDynamite), 0);
			this._tooltipRegistrar.RegisterWithKeyBinding(this._button, this._loc.T(DynamiteFragment.DetonateLocKey), DynamiteFragment.UniqueBuildingActionKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022A2 File Offset: 0x000004A2
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(DynamiteFragment.UniqueBuildingActionKey))
			{
				this.DetonateSelectedDynamite();
				return true;
			}
			return false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022BF File Offset: 0x000004BF
		public void ShowFragment(BaseComponent entity)
		{
			this._dynamite = entity.GetComponent<Dynamite>();
			if (this._dynamite)
			{
				this._inputService.AddInputProcessor(this);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022E6 File Offset: 0x000004E6
		public void ClearFragment()
		{
			this._dynamite = null;
			this._root.ToggleDisplayStyle(false);
			this._inputService.RemoveInputProcessor(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002307 File Offset: 0x00000507
		public void UpdateFragment()
		{
			if (this._dynamite && this._dynamite.IsFinished)
			{
				this.UpdateButton();
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002344 File Offset: 0x00000544
		public void UpdateButton()
		{
			if (!this._dynamite.IsFinished)
			{
				this.UpdateButton(this._loc.T(DynamiteFragment.CantDetonateLocKey), false);
				return;
			}
			if (this._dynamite.IsTriggered)
			{
				this.UpdateButton(this._loc.T(DynamiteFragment.ArmedLocKey), false);
				return;
			}
			this.UpdateButton(this._loc.T(DynamiteFragment.DetonateLocKey), true);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023B2 File Offset: 0x000005B2
		public void UpdateButton(string text, bool interactable)
		{
			this._button.text = text;
			this._button.SetEnabled(interactable);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023CC File Offset: 0x000005CC
		public void DetonateSelectedDynamite(ClickEvent evt)
		{
			this.DetonateSelectedDynamite();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		public void DetonateSelectedDynamite()
		{
			if (this._dynamite)
			{
				if (this._inputService.IsKeyHeld(DynamiteFragment.DetonationDelayKey))
				{
					this._dynamite.TriggerDelayed(10);
					return;
				}
				if (this._inputService.IsKeyHeld(DynamiteFragment.LongDetonationDelayKey))
				{
					this._dynamite.TriggerDelayed(20);
					return;
				}
				this._dynamite.Trigger();
			}
		}

		// Token: 0x0400000D RID: 13
		public static readonly string ArmedLocKey = "Building.Dynamite.Armed";

		// Token: 0x0400000E RID: 14
		public static readonly string CantDetonateLocKey = "Building.Dynamite.CantDetonate";

		// Token: 0x0400000F RID: 15
		public static readonly string DetonateLocKey = "Building.Dynamite.Detonate";

		// Token: 0x04000010 RID: 16
		public static readonly string DetonationDelayKey = "DetonationDelay";

		// Token: 0x04000011 RID: 17
		public static readonly string LongDetonationDelayKey = "LongDetonationDelay";

		// Token: 0x04000012 RID: 18
		public static readonly string UniqueBuildingActionKey = "UniqueBuildingAction";

		// Token: 0x04000013 RID: 19
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000014 RID: 20
		public readonly ILoc _loc;

		// Token: 0x04000015 RID: 21
		public readonly InputService _inputService;

		// Token: 0x04000016 RID: 22
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000017 RID: 23
		public Button _button;

		// Token: 0x04000018 RID: 24
		public Dynamite _dynamite;

		// Token: 0x04000019 RID: 25
		public VisualElement _root;
	}
}
