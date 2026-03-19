using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.TerrainLevelValidation
{
	// Token: 0x02000007 RID: 7
	public class BottomTerrainLevelValidationConstraint : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public int BottomLevel
		{
			get
			{
				IBottomLevelProvider bottomLevelProvider = this._bottomLevelProvider;
				if (bottomLevelProvider == null)
				{
					return this._blockObject.CoordinatesAtBaseZ.z;
				}
				return bottomLevelProvider.BottomLevel;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002130 File Offset: 0x00000330
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._bottomLevelProvider = this._blockObject.GetComponent<IBottomLevelProvider>();
		}

		// Token: 0x04000008 RID: 8
		public IBottomLevelProvider _bottomLevelProvider;

		// Token: 0x04000009 RID: 9
		public BlockObject _blockObject;
	}
}
