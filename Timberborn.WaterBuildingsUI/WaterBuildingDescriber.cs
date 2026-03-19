using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000016 RID: 22
	public class WaterBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000046B2 File Offset: 0x000028B2
		public WaterBuildingDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000046C1 File Offset: 0x000028C1
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._contaminatedWaterNeedingBuilding = base.GetComponent<IContaminatedWaterNeedingBuilding>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000046DB File Offset: 0x000028DB
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			if (this._blockObject.IsPreview)
			{
				string key = (this._contaminatedWaterNeedingBuilding != null) ? WaterBuildingDescriber.BadwaterAccessLocKey : WaterBuildingDescriber.WaterAccessLocKey;
				string content = SpecialStrings.RowStarter + this._loc.T(key);
				yield return EntityDescription.CreateTextSection(content, 2010);
			}
			yield break;
		}

		// Token: 0x040000A4 RID: 164
		public static readonly string WaterAccessLocKey = "Buildings.WaterAccess";

		// Token: 0x040000A5 RID: 165
		public static readonly string BadwaterAccessLocKey = "Buildings.BadwaterAccess";

		// Token: 0x040000A6 RID: 166
		public readonly ILoc _loc;

		// Token: 0x040000A7 RID: 167
		public BlockObject _blockObject;

		// Token: 0x040000A8 RID: 168
		public IContaminatedWaterNeedingBuilding _contaminatedWaterNeedingBuilding;
	}
}
