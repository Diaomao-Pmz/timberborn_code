using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Effects;
using Timberborn.Navigation;
using Timberborn.NeedSystem;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.SoakedEffects
{
	// Token: 0x02000008 RID: 8
	public class SoakedEffectApplier : TickableComponent, IAwakableComponent
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002256 File Offset: 0x00000456
		public SoakedEffectApplier(IThreadSafeWaterMap threadSafeWaterMap, SoakedEffectService soakedEffectService)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._soakedEffectService = soakedEffectService;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000226C File Offset: 0x0000046C
		public void Awake()
		{
			this._needManager = base.GetComponent<NeedManager>();
			this._waterResistor = base.GetComponent<IWaterResistor>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002288 File Offset: 0x00000488
		public override void Tick()
		{
			IWaterResistor waterResistor = this._waterResistor;
			if (waterResistor == null || !waterResistor.IsWaterResistant)
			{
				Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(base.Transform.position);
				if (this._threadSafeWaterMap.CellIsUnderwater(coordinates))
				{
					foreach (InstantEffect instantEffect in this._soakedEffectService.Effects)
					{
						this._needManager.ApplyEffect(instantEffect);
					}
				}
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x0400000A RID: 10
		public readonly SoakedEffectService _soakedEffectService;

		// Token: 0x0400000B RID: 11
		public NeedManager _needManager;

		// Token: 0x0400000C RID: 12
		public IWaterResistor _waterResistor;
	}
}
