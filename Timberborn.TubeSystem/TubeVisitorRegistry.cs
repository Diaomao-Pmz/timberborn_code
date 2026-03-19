using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000018 RID: 24
	public class TubeVisitorRegistry
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000031CF File Offset: 0x000013CF
		public ReadOnlyList<TubeVisitor> TubeVisitors
		{
			get
			{
				return this._tubeVisitors.AsReadOnlyList<TubeVisitor>();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000031DC File Offset: 0x000013DC
		public void Register(TubeVisitor tubeVisitor)
		{
			this._tubeVisitors.Add(tubeVisitor);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000031EA File Offset: 0x000013EA
		public void Unregister(TubeVisitor tubeVisitor)
		{
			this._tubeVisitors.Remove(tubeVisitor);
		}

		// Token: 0x0400003B RID: 59
		public readonly List<TubeVisitor> _tubeVisitors = new List<TubeVisitor>();
	}
}
