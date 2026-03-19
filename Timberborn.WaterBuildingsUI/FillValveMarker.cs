using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using Timberborn.WaterBuildings;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000008 RID: 8
	public class FillValveMarker : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002653 File Offset: 0x00000853
		public FillValveMarker(MarkerDrawerFactory markerDrawerFactory)
		{
			this._markerDrawerFactory = markerDrawerFactory;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002664 File Offset: 0x00000864
		public void Awake()
		{
			this._fillValve = base.GetComponent<FillValve>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._activeMarkerDrawer = this._markerDrawerFactory.CreateTileDrawer(FillValveMarker.ActiveMarkerColor);
			this._inactiveMarkerDrawer = this._markerDrawerFactory.CreateTileDrawer(FillValveMarker.InactiveMarkerColor);
			base.DisableComponent();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C8 File Offset: 0x000008C8
		public void Update()
		{
			Vector3Int outputCoordinates = this._fillValve.OutputCoordinates;
			Vector3Int coordinates;
			coordinates..ctor(outputCoordinates.x, outputCoordinates.y, 0);
			if (this._fillValve.TargetHeightEnabled)
			{
				((!this._fillValve.IsAutomated || !this._fillValve.IsInputOn) ? this._activeMarkerDrawer : this._inactiveMarkerDrawer).DrawAtCoordinates(coordinates, this._fillValve.ClampedTargetHeight + FillValveMarker.MarkerYOffset);
			}
			if (this._fillValve.IsAutomated && this._fillValve.AutomationTargetHeightEnabled)
			{
				(this._fillValve.IsInputOn ? this._activeMarkerDrawer : this._inactiveMarkerDrawer).DrawAtCoordinates(coordinates, this._fillValve.ClampedAutomationTargetHeight + FillValveMarker.MarkerYOffset);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000278F File Offset: 0x0000098F
		public void OnSelect()
		{
			if (!this._blockObject.IsPreview)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027A4 File Offset: 0x000009A4
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x04000019 RID: 25
		public static readonly Color32 ActiveMarkerColor = Color.blue;

		// Token: 0x0400001A RID: 26
		public static readonly Color32 InactiveMarkerColor = Color.blue * 0.65f;

		// Token: 0x0400001B RID: 27
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x0400001C RID: 28
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400001D RID: 29
		public FillValve _fillValve;

		// Token: 0x0400001E RID: 30
		public MeshDrawer _activeMarkerDrawer;

		// Token: 0x0400001F RID: 31
		public MeshDrawer _inactiveMarkerDrawer;

		// Token: 0x04000020 RID: 32
		public BlockObject _blockObject;
	}
}
