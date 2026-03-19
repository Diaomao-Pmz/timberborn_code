using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.AutomationUI
{
	// Token: 0x02000012 RID: 18
	public class SequentialTransmitterDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000029B2 File Offset: 0x00000BB2
		public SequentialTransmitterDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029C1 File Offset: 0x00000BC1
		public void Awake()
		{
			this._text = SpecialStrings.RowStarter + this._loc.T(SequentialTransmitterDescriber.SequentialLocKey);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E3 File Offset: 0x00000BE3
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			yield return EntityDescription.CreateTextSection(this._text, 1000);
			yield break;
		}

		// Token: 0x0400002B RID: 43
		public static readonly string SequentialLocKey = "Buildings.Sequential";

		// Token: 0x0400002C RID: 44
		public readonly ILoc _loc;

		// Token: 0x0400002D RID: 45
		public string _text;
	}
}
