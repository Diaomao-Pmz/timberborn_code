using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200001C RID: 28
	public class WaterInputSpecDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00004E96 File Offset: 0x00003096
		public WaterInputSpecDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004EC6 File Offset: 0x000030C6
		public void Awake()
		{
			this._waterInputSpec = base.GetComponent<WaterInputSpec>();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004ED4 File Offset: 0x000030D4
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string content = SpecialStrings.RowStarter + this._loc.T<int>(this._maxDepthPhrase, this._waterInputSpec.MaxDepth);
			yield return EntityDescription.CreateTextSection(content, 80);
			yield break;
		}

		// Token: 0x040000C5 RID: 197
		public readonly ILoc _loc;

		// Token: 0x040000C6 RID: 198
		public WaterInputSpec _waterInputSpec;

		// Token: 0x040000C7 RID: 199
		public readonly Phrase _maxDepthPhrase = Phrase.New("Work.MaxDepth").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
