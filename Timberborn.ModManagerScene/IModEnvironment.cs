using System;
using JetBrains.Annotations;

namespace Timberborn.ModManagerScene
{
	// Token: 0x02000004 RID: 4
	public interface IModEnvironment
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		[UsedImplicitly]
		string ModPath { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		[UsedImplicitly]
		string OriginPath { get; }
	}
}
