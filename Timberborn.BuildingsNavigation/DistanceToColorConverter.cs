using System;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.Navigation;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000013 RID: 19
	public class DistanceToColorConverter : ILoadableSingleton
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003406 File Offset: 0x00001606
		public DistanceToColorConverter(NavigationDistance navigationDistance, ISpecService specService)
		{
			this._navigationDistance = navigationDistance;
			this._specService = specService;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000341C File Offset: 0x0000161C
		public void Load()
		{
			GradientColorKey[] colorKeys = (from point in this._specService.GetSingleSpec<DistanceToColorConverterSpec>().DistanceGradient
			select new GradientColorKey(point.Color, point.Time)).ToArray<GradientColorKey>();
			this._distanceGradient = new Gradient
			{
				colorKeys = colorKeys
			};
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003478 File Offset: 0x00001678
		public Color DistanceToColor(float distance)
		{
			float num = Mathf.InverseLerp(0f, this._navigationDistance.LargeDistrictThreshold, distance);
			return this._distanceGradient.Evaluate(num);
		}

		// Token: 0x04000045 RID: 69
		public readonly NavigationDistance _navigationDistance;

		// Token: 0x04000046 RID: 70
		public readonly ISpecService _specService;

		// Token: 0x04000047 RID: 71
		public Gradient _distanceGradient;
	}
}
