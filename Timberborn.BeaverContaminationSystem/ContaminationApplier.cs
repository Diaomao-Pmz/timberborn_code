using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Navigation;
using Timberborn.TickSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.BeaverContaminationSystem
{
	// Token: 0x0200000A RID: 10
	public class ContaminationApplier : TickableComponent, IAwakableComponent
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002464 File Offset: 0x00000664
		public ContaminationApplier(IThreadSafeWaterMap threadSafeWaterMap, IRandomNumberGenerator randomNumberGenerator)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000247A File Offset: 0x0000067A
		public void Awake()
		{
			this._waterResistor = base.GetComponent<IWaterResistor>();
			this._contaminationIncubator = base.GetComponent<ContaminationIncubator>();
			this._contaminable = base.GetComponent<Contaminable>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024A0 File Offset: 0x000006A0
		public override void Tick()
		{
			IWaterResistor waterResistor = this._waterResistor;
			if ((waterResistor == null || !waterResistor.IsWaterResistant) && !this._contaminationIncubator.IsIncubating && !this._contaminable.IsContaminated)
			{
				this.TryApplyContamination();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024E0 File Offset: 0x000006E0
		public void TryApplyContamination()
		{
			Vector3Int coordinates = NavigationCoordinateSystem.WorldToGridInt(base.Transform.position);
			if (this._threadSafeWaterMap.CellIsUnderwater(coordinates))
			{
				float num = this._threadSafeWaterMap.ColumnContamination(coordinates);
				if (num >= ContaminationApplier.MinimumWaterContamination)
				{
					float normalizedProbability = num * ContaminationApplier.ContaminationProbability;
					if (this._randomNumberGenerator.CheckProbability(normalizedProbability))
					{
						this._contaminationIncubator.StartIncubation();
					}
				}
			}
		}

		// Token: 0x04000011 RID: 17
		public static readonly float MinimumWaterContamination = 0.05f;

		// Token: 0x04000012 RID: 18
		public static readonly float ContaminationProbability = 0.01f;

		// Token: 0x04000013 RID: 19
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000014 RID: 20
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000015 RID: 21
		public IWaterResistor _waterResistor;

		// Token: 0x04000016 RID: 22
		public ContaminationIncubator _contaminationIncubator;

		// Token: 0x04000017 RID: 23
		public Contaminable _contaminable;
	}
}
