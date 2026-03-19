using System;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.WaterSourceRendering
{
	// Token: 0x02000007 RID: 7
	public class RendererMaskItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public ImmutableArray<Vector3Int> Coordinates { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public bool IsVisible { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public RendererMaskItem(WaterSourceRenderer renderer)
		{
			this._renderer = renderer;
			this.Coordinates = (from coordinate in this._renderer.GetComponent<BlockObject>().PositionedBlocks.GetFoundationCoordinates()
			orderby coordinate.x, coordinate.y
			select coordinate).ToImmutableArray<Vector3Int>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		public bool UpdateVisibility(bool hasFullyVisibleWaterSurfaceAbove)
		{
			bool isVisible = this.IsVisible;
			this.IsVisible = (this._renderer.CanBeRendered && hasFullyVisibleWaterSurfaceAbove);
			return this.IsVisible != isVisible;
		}

		// Token: 0x0400000A RID: 10
		public readonly WaterSourceRenderer _renderer;
	}
}
