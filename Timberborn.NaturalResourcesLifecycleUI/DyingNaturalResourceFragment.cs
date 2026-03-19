using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Cutting;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.UIFormatters;
using UnityEngine.UIElements;

namespace Timberborn.NaturalResourcesLifecycleUI
{
	// Token: 0x02000005 RID: 5
	public class DyingNaturalResourceFragment : IEntityPanelFragment
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021C4 File Offset: 0x000003C4
		public DyingNaturalResourceFragment(VisualElementLoader visualElementLoader, ILoc loc, DeadNaturalResourceDescriber deadNaturalResourceDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._deadNaturalResourceDescriber = deadNaturalResourceDescriber;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E4 File Offset: 0x000003E4
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/DyingNaturalResourceFragment");
			this._root.ToggleDisplayStyle(false);
			this._progressBar = UQueryExtensions.Q<ProgressBar>(this._root, "ProgressBar", null);
			this._description = UQueryExtensions.Q<Label>(this._root, "Description", null);
			return this._root;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002247 File Offset: 0x00000447
		public void ShowFragment(BaseComponent entity)
		{
			this._dyingNaturalResource = entity.GetComponent<DyingNaturalResource>();
			this._cuttable = entity.GetComponent<Cuttable>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002261 File Offset: 0x00000461
		public void ClearFragment()
		{
			this._dyingNaturalResource = null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000226C File Offset: 0x0000046C
		public void UpdateFragment()
		{
			bool flag = this._dyingNaturalResource && (!this._cuttable || !this._cuttable.Yielder.IsYieldRemoved);
			this._root.ToggleDisplayStyle(flag);
			if (flag)
			{
				DyingProgress closestDyingProgress = this._dyingNaturalResource.GetClosestDyingProgress();
				this._progressBar.SetProgress(closestDyingProgress.Progress);
				this._description.text = this.BuildDescription(closestDyingProgress);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022EC File Offset: 0x000004EC
		public string BuildDescription(DyingProgress dyingProgress)
		{
			if (dyingProgress.Died)
			{
				return this._deadNaturalResourceDescriber.Describe(this._dyingNaturalResource);
			}
			if (dyingProgress.IsDying)
			{
				string param = NumberFormatter.CeilToTenthsPlace((double)dyingProgress.DaysLeft);
				return this._loc.T<string>(DyingNaturalResourceFragment.DaysToDieLocKey, param);
			}
			return this._loc.T(DyingNaturalResourceFragment.HealthyLocKey);
		}

		// Token: 0x0400000C RID: 12
		public static readonly string HealthyLocKey = "NaturalResources.Healthy";

		// Token: 0x0400000D RID: 13
		public static readonly string DaysToDieLocKey = "NaturalResources.DaysToDie";

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public readonly DeadNaturalResourceDescriber _deadNaturalResourceDescriber;

		// Token: 0x04000011 RID: 17
		public DyingNaturalResource _dyingNaturalResource;

		// Token: 0x04000012 RID: 18
		public Cuttable _cuttable;

		// Token: 0x04000013 RID: 19
		public VisualElement _root;

		// Token: 0x04000014 RID: 20
		public ProgressBar _progressBar;

		// Token: 0x04000015 RID: 21
		public Label _description;
	}
}
