using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.Gathering;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.GatheringUI
{
	// Token: 0x02000008 RID: 8
	public class GatherablePrioritizerDropdownProvider : BaseComponent, IAwakableComponent, IInitializableEntity, IExtendedDropdownProvider, IDropdownProvider
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002562 File Offset: 0x00000762
		public GatherablePrioritizerDropdownProvider(GoodDescriber goodDescriber, ILoc loc)
		{
			this._goodDescriber = goodDescriber;
			this._loc = loc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002583 File Offset: 0x00000783
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._items.AsReadOnlyList<string>();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002598 File Offset: 0x00000798
		public bool HasMultipleOptions
		{
			get
			{
				return this._gathererFlag.AllowedGatherables.Length > 1;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025BB File Offset: 0x000007BB
		public void Awake()
		{
			this._gatherablePrioritizer = base.GetComponent<GatherablePrioritizer>();
			this._gathererFlag = base.GetComponent<GathererFlag>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025D8 File Offset: 0x000007D8
		public void InitializeEntity()
		{
			ImmutableArray<GatherableSpec> allowedGatherables = this._gathererFlag.AllowedGatherables;
			this._items.Add(this._loc.T(GatherablePrioritizerDropdownProvider.NothingItemLocKey));
			this._items.AddRange(allowedGatherables.Select(new Func<GatherableSpec, string>(this.GatherableGoodName)));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002629 File Offset: 0x00000829
		public string GetValue()
		{
			if (!(this._gatherablePrioritizer.PrioritizedGatherable != null))
			{
				return this._loc.T(GatherablePrioritizerDropdownProvider.NothingItemLocKey);
			}
			return this.GatherableGoodName(this._gatherablePrioritizer.PrioritizedGatherable);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002660 File Offset: 0x00000860
		public void SetValue(string value)
		{
			GatherableSpec prioritizedGatherable = this.GetPrioritizedGatherable(value);
			this._gatherablePrioritizer.PrioritizeGatherable(prioritizedGatherable);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002681 File Offset: 0x00000881
		public string FormatDisplayText(string value, bool selected)
		{
			return value;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002684 File Offset: 0x00000884
		public Sprite GetIcon(string value)
		{
			GatherableSpec prioritizedGatherable = this.GetPrioritizedGatherable(value);
			if (prioritizedGatherable != null)
			{
				string id = prioritizedGatherable.Yielder.Yield.Id;
				return this._goodDescriber.GetIcon(id);
			}
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026C1 File Offset: 0x000008C1
		public ImmutableArray<string> GetItemClasses(string value)
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026C8 File Offset: 0x000008C8
		public string GatherableGoodName(GatherableSpec gatherableSpec)
		{
			return this._goodDescriber.Describe(gatherableSpec.Yielder.Yield.Id);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026E8 File Offset: 0x000008E8
		public GatherableSpec GetPrioritizedGatherable(string value)
		{
			return this._gathererFlag.AllowedGatherables.SingleOrDefault((GatherableSpec gatherable) => this.GatherableGoodName(gatherable) == value);
		}

		// Token: 0x04000022 RID: 34
		public static readonly string NothingItemLocKey = "Gathering.Nothing";

		// Token: 0x04000023 RID: 35
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000024 RID: 36
		public readonly ILoc _loc;

		// Token: 0x04000025 RID: 37
		public GatherablePrioritizer _gatherablePrioritizer;

		// Token: 0x04000026 RID: 38
		public GathererFlag _gathererFlag;

		// Token: 0x04000027 RID: 39
		public readonly List<string> _items = new List<string>();
	}
}
