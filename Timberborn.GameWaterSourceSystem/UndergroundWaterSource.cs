using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x02000009 RID: 9
	public class UndergroundWaterSource : BaseComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002257 File Offset: 0x00000457
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002265 File Offset: 0x00000465
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002273 File Offset: 0x00000473
		public float GetStrengthModifier()
		{
			return (float)(this._isOccupied ? 1 : 0);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002282 File Offset: 0x00000482
		public void SetOccupied()
		{
			this._isOccupied = true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000228B File Offset: 0x0000048B
		public void SetUnoccupied()
		{
			this._isOccupied = false;
		}

		// Token: 0x0400000D RID: 13
		public WaterSource _waterSource;

		// Token: 0x0400000E RID: 14
		public bool _isOccupied;
	}
}
