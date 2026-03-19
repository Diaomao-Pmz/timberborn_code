using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.Cutting;
using Timberborn.EntityPanelSystem;
using Timberborn.Goods;
using Timberborn.GoodsUI;
using Timberborn.Growing;
using Timberborn.Localization;
using Timberborn.NaturalResourcesLifecycle;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.YieldingUI;
using UnityEngine.UIElements;

namespace Timberborn.GrowingUI
{
	// Token: 0x02000004 RID: 4
	public class GrowableFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public GrowableFragment(VisualElementLoader visualElementLoader, ILoc loc, YieldTooltipFactory yieldTooltipFactory, ITooltipRegistrar tooltipRegistrar, GoodDescriber goodDescriber)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._yieldTooltipFactory = yieldTooltipFactory;
			this._tooltipRegistrar = tooltipRegistrar;
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
			UQueryExtensions.Q<VisualElement>(this._root, "Calendar", null).AddToClassList(GrowableFragment.IconClass);
			this._tooltipRegistrar.Register(this._root, new Func<VisualElement>(this.GetTooltip));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021DD File Offset: 0x000003DD
		public void ShowFragment(BaseComponent entity)
		{
			this._growable = entity.GetComponent<Growable>();
			if (this._growable)
			{
				this._cuttable = this._growable.GetComponent<Cuttable>();
				this._livingNaturalResource = this._growable.GetComponent<LivingNaturalResource>();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000221A File Offset: 0x0000041A
		public void ClearFragment()
		{
			this._growable = null;
			this._cuttable = null;
			this._livingNaturalResource = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002234 File Offset: 0x00000434
		public void UpdateFragment()
		{
			bool flag = this._growable && (!this._livingNaturalResource.IsDead || (this._cuttable && this._cuttable.Yielder.IsYielding));
			this._root.ToggleDisplayStyle(flag);
			if (flag)
			{
				this._progressText.text = NumberFormatter.FormatAsPercentFloored((double)this._growable.GrowthProgress);
				this._growthTime.text = this._loc.T<string>(this._growthTimePhrase, this.GrowthTime);
				if (this._cuttable)
				{
					GoodAmountSpec yield = this._cuttable.YielderSpec.Yield;
					this._yieldAmount.text = yield.Amount.ToString();
					this._yieldIcon.sprite = this._goodDescriber.GetIcon(yield.Id);
				}
				else
				{
					this._yieldIcon.sprite = null;
				}
				this._yieldAmount.ToggleDisplayStyle(this._cuttable);
				this._yieldIcon.EnableInClassList(GrowableFragment.NoYieldClass, !this._cuttable);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002368 File Offset: 0x00000568
		public string GrowthTime
		{
			get
			{
				return this._growable.GrowthTimeInDays.ToString("F0");
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002390 File Offset: 0x00000590
		public VisualElement GetTooltip()
		{
			if (this._cuttable)
			{
				return this._yieldTooltipFactory.Create(this._cuttable.YielderSpec, this.GrowthTime, null);
			}
			Label label = new Label(this._loc.T<string>(GrowableFragment.GrowingTimeLocKey, this.GrowthTime));
			label.AddToClassList("game-text-normal");
			label.AddToClassList("text--grey");
			return label;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string GrowingTimeLocKey = "Growing.Time";

		// Token: 0x04000007 RID: 7
		public static readonly string IconClass = "resource-yield__icon--calendar";

		// Token: 0x04000008 RID: 8
		public static readonly string NoYieldClass = "resource-yield__no-yield";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly ILoc _loc;

		// Token: 0x0400000B RID: 11
		public readonly YieldTooltipFactory _yieldTooltipFactory;

		// Token: 0x0400000C RID: 12
		public readonly ITooltipRegistrar _tooltipRegistrar;

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
		public Growable _growable;

		// Token: 0x04000014 RID: 20
		public Cuttable _cuttable;

		// Token: 0x04000015 RID: 21
		public LivingNaturalResource _livingNaturalResource;

		// Token: 0x04000016 RID: 22
		public readonly Phrase _growthTimePhrase = Phrase.New().Format<string>(new Func<string, ILoc, string>(UnitFormatter.FormatDays));
	}
}
