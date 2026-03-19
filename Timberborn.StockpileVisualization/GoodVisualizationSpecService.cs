using System;
using System.Collections.Frozen;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.StockpileVisualization
{
	// Token: 0x02000012 RID: 18
	public class GoodVisualizationSpecService : ILoadableSingleton
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000035F8 File Offset: 0x000017F8
		public GoodVisualizationSpecService(ISpecService specService)
		{
			this._specService = specService;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003608 File Offset: 0x00001808
		public void Load()
		{
			this._goodVisualizationSpecs = this._specService.GetSpecs<GoodVisualizationSpec>().ToFrozenDictionary((GoodVisualizationSpec spec) => spec.Id + spec.Variant, (GoodVisualizationSpec spec) => spec, null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000366A File Offset: 0x0000186A
		public unsafe GoodVisualizationSpec GetVisualization(string visualizationId, string visualizationVariant = "")
		{
			return *this._goodVisualizationSpecs[visualizationId + visualizationVariant];
		}

		// Token: 0x04000045 RID: 69
		public readonly ISpecService _specService;

		// Token: 0x04000046 RID: 70
		public FrozenDictionary<string, GoodVisualizationSpec> _goodVisualizationSpecs;
	}
}
