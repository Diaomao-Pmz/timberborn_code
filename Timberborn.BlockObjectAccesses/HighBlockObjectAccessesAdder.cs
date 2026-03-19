using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.BlockObjectAccesses
{
	// Token: 0x02000011 RID: 17
	public class HighBlockObjectAccessesAdder : BaseComponent, IStartableComponent
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003174 File Offset: 0x00001374
		public void Start()
		{
			this.AddAccessesAboveGround();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000317C File Offset: 0x0000137C
		public void AddAccessesAboveGround()
		{
			int z = base.GetComponent<BlockObject>().Blocks.Size.z;
			base.GetComponent<BlockObjectAccessible>().SetNumberOfAccessLevelsAboveGround(z);
		}
	}
}
