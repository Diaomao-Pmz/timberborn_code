using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x02000011 RID: 17
	public class UndergroundDepthDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002C3F File Offset: 0x00000E3F
		public UndergroundDepthDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C6A File Offset: 0x00000E6A
		public void Awake()
		{
			this._undergroundDepthDescriberSpec = base.GetComponent<UndergroundDepthDescriberSpec>();
			this._infiniteUndergroundModel = base.GetComponent<IInfiniteUndergroundModel>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C84 File Offset: 0x00000E84
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string param = (this._infiniteUndergroundModel != null) ? this._loc.T(UndergroundDepthDescriber.InfiniteDepthLocKey) : this._loc.T<int>(this._undergroundDepthPhrase, this._undergroundDepthDescriberSpec.Depth);
			yield return EntityDescription.CreateTextSection(SpecialStrings.RowStarter + this._loc.T<string>(UndergroundDepthDescriber.UndergroundDepthLocKey, param), 2200);
			yield break;
		}

		// Token: 0x04000028 RID: 40
		public static readonly string UndergroundDepthLocKey = "Buildings.UndergroundDepth";

		// Token: 0x04000029 RID: 41
		public static readonly string InfiniteDepthLocKey = "Buildings.InfiniteDepth";

		// Token: 0x0400002A RID: 42
		public readonly ILoc _loc;

		// Token: 0x0400002B RID: 43
		public UndergroundDepthDescriberSpec _undergroundDepthDescriberSpec;

		// Token: 0x0400002C RID: 44
		public IInfiniteUndergroundModel _infiniteUndergroundModel;

		// Token: 0x0400002D RID: 45
		public readonly Phrase _undergroundDepthPhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
