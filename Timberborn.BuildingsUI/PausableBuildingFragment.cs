using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystemUI;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.BuildingsUI
{
	// Token: 0x0200000F RID: 15
	public class PausableBuildingFragment : IEntityPanelFragment
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002C65 File Offset: 0x00000E65
		public PausableBuildingFragment(VisualElementLoader visualElementLoader, BindableToggleFactory bindableToggleFactory, ITooltipRegistrar tooltipRegistrar)
		{
			this._visualElementLoader = visualElementLoader;
			this._bindableToggleFactory = bindableToggleFactory;
			this._tooltipRegistrar = tooltipRegistrar;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C84 File Offset: 0x00000E84
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/PausableBuildingFragment");
			Toggle toggle = UQueryExtensions.Q<Toggle>(this._root, "Toggle", null);
			this._pauseToggle = this._bindableToggleFactory.Create(toggle, PausableBuildingFragment.ToggleBuildingPauseKey, new Action<bool>(this.ToggleActivationState), () => !this._pausableBuilding.Paused);
			this._root.ToggleDisplayStyle(false);
			this._tooltipRegistrar.RegisterWithKeyBinding(toggle, PausableBuildingFragment.ToggleBuildingPauseKey);
			return this._root;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D0B File Offset: 0x00000F0B
		public void ShowFragment(BaseComponent entity)
		{
			this._pausableBuilding = entity.GetComponent<PausableBuilding>();
			if (this._pausableBuilding)
			{
				this._pauseToggle.Bind();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D31 File Offset: 0x00000F31
		public void ClearFragment()
		{
			if (this._pausableBuilding)
			{
				this.ToggleHighlight(false);
			}
			this._pausableBuilding = null;
			this._root.ToggleDisplayStyle(false);
			this._pauseToggle.Unbind();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D68 File Offset: 0x00000F68
		public void UpdateFragment()
		{
			if (this._pausableBuilding && this._pausableBuilding.IsPausable())
			{
				this._root.ToggleDisplayStyle(true);
				this._pauseToggle.Enable();
				return;
			}
			this._root.ToggleDisplayStyle(false);
			this._pauseToggle.Disable();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DBE File Offset: 0x00000FBE
		public void ToggleHighlight(bool state)
		{
			this._root.EnableInClassList("highlight", state);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public void ToggleActivationState(bool resume)
		{
			if (this._pausableBuilding && this._pausableBuilding.IsPausable())
			{
				if (resume)
				{
					this._pausableBuilding.Resume();
					return;
				}
				this._pausableBuilding.Pause();
			}
		}

		// Token: 0x0400003D RID: 61
		public static readonly string ToggleBuildingPauseKey = "ToggleBuildingPause";

		// Token: 0x0400003E RID: 62
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400003F RID: 63
		public readonly BindableToggleFactory _bindableToggleFactory;

		// Token: 0x04000040 RID: 64
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000041 RID: 65
		public PausableBuilding _pausableBuilding;

		// Token: 0x04000042 RID: 66
		public VisualElement _root;

		// Token: 0x04000043 RID: 67
		public BindableToggle _pauseToggle;
	}
}
