using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GoodsUI;
using Timberborn.Localization;
using Timberborn.Yielding;

namespace Timberborn.YieldingUI
{
	// Token: 0x02000005 RID: 5
	public class YieldRemovingBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000210F File Offset: 0x0000030F
		public YieldRemovingBuildingDescriber(ILoc loc, GoodDescriber goodDescriber)
		{
			this._loc = loc;
			this._goodDescriber = goodDescriber;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002125 File Offset: 0x00000325
		public void Awake()
		{
			this._yieldRemovingBuilding = base.GetComponent<YieldRemovingBuilding>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002133 File Offset: 0x00000333
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (!base.GameObject.activeInHierarchy)
			{
				yield return EntityDescription.CreateTextSection(this.Describe().TrimEnd(), 100);
			}
			yield break;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002144 File Offset: 0x00000344
		public string Describe()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringListBuilder stringListBuilder = new StringListBuilder(stringBuilder, ", ");
			stringBuilder.Append(SpecialStrings.RowStarter + this._loc.T(YieldRemovingBuildingDescriber.GatheringLocKey) + " ");
			foreach (string value in this.GetAllowedGoodNames())
			{
				stringListBuilder.BeginItem();
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021D8 File Offset: 0x000003D8
		public IEnumerable<string> GetAllowedGoodNames()
		{
			return (from yielderDecorable in this._yieldRemovingBuilding.GetAllowedYielders()
			select this.GetPluralDisplayName(yielderDecorable.Yielder)).Distinct<string>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FB File Offset: 0x000003FB
		public string GetPluralDisplayName(YielderSpec spec)
		{
			return this._goodDescriber.Describe(spec.Yield.Id);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string GatheringLocKey = "Gathering.Action";

		// Token: 0x04000007 RID: 7
		public readonly ILoc _loc;

		// Token: 0x04000008 RID: 8
		public readonly GoodDescriber _goodDescriber;

		// Token: 0x04000009 RID: 9
		public YieldRemovingBuilding _yieldRemovingBuilding;
	}
}
