using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Explosions;
using Timberborn.Localization;
using Timberborn.UIFormatters;

namespace Timberborn.ExplosionsUI
{
	// Token: 0x02000004 RID: 4
	public class DynamiteDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public DynamiteDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EE File Offset: 0x000002EE
		public void Awake()
		{
			this._dynamite = base.GetComponent<Dynamite>();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string content = SpecialStrings.RowStarter + this._loc.T<int>(this._explosionDepthPhrase, this._dynamite.Depth);
			yield return EntityDescription.CreateTextSection(content, 40);
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public readonly ILoc _loc;

		// Token: 0x04000007 RID: 7
		public Dynamite _dynamite;

		// Token: 0x04000008 RID: 8
		public readonly Phrase _explosionDepthPhrase = Phrase.New("Building.Dynamite.ExplosionDepth").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
