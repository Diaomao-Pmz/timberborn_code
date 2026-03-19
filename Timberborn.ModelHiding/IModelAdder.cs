using System;
using Timberborn.BlockObjectModelSystem;

namespace Timberborn.ModelHiding
{
	// Token: 0x0200000C RID: 12
	public interface IModelAdder
	{
		// Token: 0x06000029 RID: 41
		void AddModel(BlockObjectModelController model);

		// Token: 0x0600002A RID: 42
		void RemoveModel(BlockObjectModelController model);
	}
}
