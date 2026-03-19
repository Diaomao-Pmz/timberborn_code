using System;
using System.Collections.Generic;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000016 RID: 22
	public interface IEntityDescriber
	{
		// Token: 0x060000A5 RID: 165
		IEnumerable<EntityDescription> DescribeEntity();
	}
}
