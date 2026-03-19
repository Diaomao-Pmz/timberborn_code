using System;
using System.Collections.Generic;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;

namespace Timberborn.BlockObjectPickingSystem
{
	// Token: 0x02000005 RID: 5
	public class BlockObjectModelBlockadeIgnorer
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020F0 File Offset: 0x000002F0
		public void IgnoreModelBlockades(IEnumerable<BlockObject> blockObjects)
		{
			foreach (BlockObject blockObject in blockObjects)
			{
				BlockObjectModelController component = blockObject.GetComponent<BlockObjectModelController>();
				if (component)
				{
					component.IgnoreModelBlockade();
					this._blockObjectModelControllers.Add(component);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002150 File Offset: 0x00000350
		public void UnignoreModelBlockades()
		{
			foreach (BlockObjectModelController blockObjectModelController in this._blockObjectModelControllers)
			{
				blockObjectModelController.UnignoreModelBlockade();
			}
			this.Clear();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Clear()
		{
			this._blockObjectModelControllers.Clear();
		}

		// Token: 0x04000009 RID: 9
		public readonly List<BlockObjectModelController> _blockObjectModelControllers = new List<BlockObjectModelController>();
	}
}
