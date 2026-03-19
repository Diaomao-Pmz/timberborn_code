using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Gathering;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Growing;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.YieldingUI;
using UnityEngine.UIElements;

namespace Timberborn.GatheringUI
{
	// Token: 0x02000004 RID: 4
	public class GatherableFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public GatherableFragment(VisualElementLoader visualElementLoader, ITooltipRegistrar tooltipRegistrar, YieldTooltipFactory yieldTooltipFactory, ILoc loc, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._tooltipRegistrar = tooltipRegistrar;
			this._yieldTooltipFactory = yieldTooltipFactory;
			this._loc = loc;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002114 File Offset: 0x00000314
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ResourceYieldFragment");
			this._progressText = UQueryExtensions.Q<Label>(this._root, "ProgressText", null);
			this._growthTime = UQueryExtensions.Q<Label>(this._root, "GrowthTime", null);
			this._yieldAmount = UQueryExtensions.Q<Label>(this._root, "YieldAmount", null);
			this._yieldIcon = UQueryExtensions.Q<Image>(this._root, "YieldIcon", null);
			UQueryExtensions.Q<VisualElement>(this._root, "Calendar", null).AddToClassList(GatherableFragment.IconClass);
			this._root.ToggleDisplayStyle(false);
			this._tooltipRegistrar.Register(this._root, new Func<VisualElement>(this.GetTooltip));
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021E0 File Offset: 0x000003E0
		public void ShowFragment(BaseComponent entity)
		{
			this._gatherable = entity.GetComponent<Gatherable>();
			if (this._gatherable && this._gatherable.UsableWithCurrentFeatureToggles)
			{
				this._growable = this._gatherable.GetComponent<Growable>();
				this._gatherableYieldGrower = this._gatherable.GetComponent<GatherableYieldGrower>();
				this._livingNaturalResource = this._gatherable.GetComponent<LivingNaturalResource>();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002246 File Offset: 0x00000446
		public void ClearFragment()
		{
			this._gatherable = null;
			this._growable = null;
			this._gatherableYieldGrower = null;
			this._livingNaturalResource = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002264 File Offset: 0x00000464
		public void UpdateFragment()
		{
			bool flag = this._gatherable && this._gatherable.UsableWithCurrentFeatureToggles && (!this._livingNaturalResource.IsDead || this._gatherable.Yielder.IsYielding);
			this._root.ToggleDisplayStyle(flag);
			if (flag)
			{
				float growthProgress = this._gatherableYieldGrower.GrowthProgress;
				this._progressText.text = NumberFormatter.FormatAsPercentFloored((double)growthProgress);
				bool flag2 = !this._growable || this._growable.IsGrown;
				this._progressText.ToggleDisplayStyle(flag2);
				this._root.EnableInClassList(GatherableFragment.InactiveClass, !flag2);
				this._growthTime.text = this._loc.T<string>(this._growthTimePhrase, this.GrowthTime);
				GoodAmountSpec yield = this._gatherable.YielderSpec.Yield;
				this._yieldAmount.text = yield.Amount.ToString();
				this._yieldIcon.sprite = this._goodDescriber.GetIcon(yield.Id);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002384 File Offset: 0x00000584
		public string GrowthTime
		{
			get
			{
				return this._gatherable.YieldGrowthTimeInDays.ToString("F0");
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023A9 File Offset: 0x000005A9
		public VisualElement GetTooltip()
		{
			return this._yieldTooltipFactory.Create(this._gatherable.YielderSpec, this.GrowthTime, this._loc.T(GatherableFragment.GrowsWhenMatureLocKey));
		}

		// Token: 0x04000006 RID: 6
		public static readonly string GrowsWhenMatureLocKey = "Growing.GrowsWhenMature";

		// Token: 0x04000007 RID: 7
		public static readonly string IconClass = "resource-yield__icon--calendar-cycle";

		// Token: 0x04000008 RID: 8
		public static readonly string InactiveClass = "resource-yield--inactive";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000B RID: 11
		public readonly YieldTooltipFactory _yieldTooltipFactory;

		// Token: 0x0400000C RID: 12
		public readonly ILoc _loc;

		// Token: 0x0400000D RID: 13
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x0400000E RID: 14
		public VisualElement _root;

		// Token: 0x0400000F RID: 15
		public Label _progressText;

		// Token: 0x04000010 RID: 16
		public Label _growthTime;

		// Token: 0x04000011 RID: 17
		public Label _yieldAmount;

		// Token: 0x04000012 RID: 18
		public Image _yieldIcon;

		// Token: 0x04000013 RID: 19
		public Gatherable _gatherable;

		// Token: 0x04000014 RID: 20
		public Growable _growable;

		// Token: 0x04000015 RID: 21
		public GatherableYieldGrower _gatherableYieldGrower;

		// Token: 0x04000016 RID: 22
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000017 RID: 23
		public readonly Phrase _growthTimePhrase = Phrase.New().Format<string>(new Func<string, ILoc, string>(UnitFormatter.FormatDays));
	}
}
