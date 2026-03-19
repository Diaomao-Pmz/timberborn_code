using System;
using Timberborn.Navigation;

namespace Timberborn.BlockSystemNavigation
{
	// Token: 0x02000015 RID: 21
	public interface IBlockObjectNavMesh
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009E RID: 158
		NavMeshObject NavMeshObject { get; }

		// Token: 0x0600009F RID: 159
		void RecalculateNavMeshObject();
	}
}
