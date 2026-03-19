using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using Timberborn.WaterBuildings;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200000D RID: 13
	public class SluiceMarker : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003787 File Offset: 0x00001987
		public SluiceMarker(MarkerDrawerFactory markerDrawerFactory)
		{
			this._markerDrawerFactory = markerDrawerFactory;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003798 File Offset: 0x00001998
		public void Awake()
		{
			this._sluice = base.GetComponent<Sluice>();
			this._sluiceState = base.GetComponent<SluiceState>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._markerDrawer = this._markerDrawerFactory.CreateTileDrawer(SluiceMarker.MarkerColor);
			base.DisableComponent();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000037EC File Offset: 0x000019EC
		public void Update()
		{
			Vector3Int targetCoordinates = this._sluice.TargetCoordinates;
			Vector3Int coordinates;
			coordinates..ctor(targetCoordinates.x, targetCoordinates.y, this._sluice.MaxHeight);
			this._markerDrawer.DrawAtCoordinates(coordinates, this._sluiceState.OutflowLimit + SluiceMarker.MarkerYOffset);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003842 File Offset: 0x00001A42
		public void OnSelect()
		{
			if (!this._blockObject.IsPreview)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000027A4 File Offset: 0x000009A4
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x0400005D RID: 93
		public static readonly Color32 MarkerColor = Color.blue;

		// Token: 0x0400005E RID: 94
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400005F RID: 95
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000060 RID: 96
		public Sluice _sluice;

		// Token: 0x04000061 RID: 97
		public SluiceState _sluiceState;

		// Token: 0x04000062 RID: 98
		public MeshDrawer _markerDrawer;

		// Token: 0x04000063 RID: 99
		public BlockObject _blockObject;
	}
}
