using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.GameWaterSourceSystem
{
	// Token: 0x0200000D RID: 13
	public class WaterSourceActivator : BaseComponent, IActivableComponent, IAwakableComponent, IInitializableEntity, IWaterStrengthModifier
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002518 File Offset: 0x00000718
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002526 File Offset: 0x00000726
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002534 File Offset: 0x00000734
		public float GetStrengthModifier()
		{
			if (!this._isActive && !this._forcedActive)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002551 File Offset: 0x00000751
		public void Deactivate()
		{
			this._isActive = false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000255A File Offset: 0x0000075A
		public void Activate()
		{
			this._isActive = true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002563 File Offset: 0x00000763
		public void ForceActive()
		{
			this._forcedActive = true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000256C File Offset: 0x0000076C
		public void DisableForceActive()
		{
			this._forcedActive = false;
		}

		// Token: 0x04000017 RID: 23
		public WaterSource _waterSource;

		// Token: 0x04000018 RID: 24
		public bool _isActive = true;

		// Token: 0x04000019 RID: 25
		public bool _forcedActive;
	}
}
