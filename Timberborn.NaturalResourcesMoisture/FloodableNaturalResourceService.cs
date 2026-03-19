using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Timberborn.NaturalResourcesMoisture
{
	// Token: 0x02000007 RID: 7
	public class FloodableNaturalResourceService : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public FloodableNaturalResourceService(IThreadSafeWaterMap threadSafeWaterMap, TemplateService templateService)
		{
			this._threadSafeWaterMap = threadSafeWaterMap;
			this._templateService = templateService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public void Load()
		{
			foreach (FloodableNaturalResourceSpec floodableNaturalResourceSpec in this._templateService.GetAll<FloodableNaturalResourceSpec>())
			{
				string templateName = floodableNaturalResourceSpec.GetSpec<TemplateSpec>().TemplateName;
				this._specs[templateName] = floodableNaturalResourceSpec;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		public bool IsFloodableNaturalResource(string resourceName)
		{
			return this._specs.ContainsKey(resourceName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00000394
		public bool ConditionsAreMet(string resourceName, Vector3Int coordinates)
		{
			int num = this.WaterDepth(coordinates);
			FloodableNaturalResourceSpec floodableNaturalResourceSpec = this._specs[resourceName];
			return num >= floodableNaturalResourceSpec.MinWaterHeight && num <= floodableNaturalResourceSpec.MaxWaterHeight;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D0 File Offset: 0x000003D0
		public int WaterDepth(Vector3Int coordinates)
		{
			int num = this._threadSafeWaterMap.CeiledWaterHeight(coordinates);
			if (num <= 0)
			{
				return 0;
			}
			return num - coordinates.z;
		}

		// Token: 0x04000008 RID: 8
		public readonly IThreadSafeWaterMap _threadSafeWaterMap;

		// Token: 0x04000009 RID: 9
		public readonly TemplateService _templateService;

		// Token: 0x0400000A RID: 10
		public readonly Dictionary<string, FloodableNaturalResourceSpec> _specs = new Dictionary<string, FloodableNaturalResourceSpec>();
	}
}
