using System;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000024 RID: 36
	public class WaterOutputParticleColors : ILoadableSingleton
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005429 File Offset: 0x00003629
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00005431 File Offset: 0x00003631
		public Gradient WaterContaminationParticleGradient { get; private set; }

		// Token: 0x060000DB RID: 219 RVA: 0x0000543A File Offset: 0x0000363A
		public WaterOutputParticleColors(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000544C File Offset: 0x0000364C
		public void Load()
		{
			GradientColorKey[] colorKeys = (from point in this._specService.GetSingleSpec<WaterOutputParticleColorsSpec>().WaterContaminationParticleGradient
			select new GradientColorKey(point.Color, point.Time)).ToArray<GradientColorKey>();
			this.WaterContaminationParticleGradient = new Gradient
			{
				colorKeys = colorKeys
			};
		}

		// Token: 0x040000E3 RID: 227
		public readonly ISpecService _specService;
	}
}
