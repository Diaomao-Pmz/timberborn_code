using System;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.GameDistrictsMigrationBatchControl;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000010 RID: 16
	public class DistrictConnectionDrawingService : ILoadableSingleton
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000028FA File Offset: 0x00000AFA
		public DistrictConnectionDrawingService(ISpecService specService, DistrictConnectionLineRenderer districtConnectionLineRenderer, EventBus eventBus, Highlighter highlighter, ManualMigrationDistrictSetter manualMigrationDistrictSetter)
		{
			this._specService = specService;
			this._districtConnectionLineRenderer = districtConnectionLineRenderer;
			this._eventBus = eventBus;
			this._highlighter = highlighter;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002927 File Offset: 0x00000B27
		public void Load()
		{
			this._eventBus.Register(this);
			this._connectionHighlightColor = this._specService.GetSingleSpec<DistrictConnectionDrawingServiceSpec>().ConnectionHighlight;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000294B File Offset: 0x00000B4B
		[OnEvent]
		public void OnManualMigrationPanelOpened(ManualMigrationPanelOpenedEvent manualMigrationPanelOpenedEvent)
		{
			this._enabled = true;
			this.DrawOrClearConnection();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000295A File Offset: 0x00000B5A
		[OnEvent]
		public void OnManualMigrationPanelClosed(ManualMigrationPanelClosedEvent manualMigrationPanelClosedEvent)
		{
			this._enabled = false;
			this.Clear();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002969 File Offset: 0x00000B69
		[OnEvent]
		public void OnManualMigrationBlockingStateChanged(ManualMigrationBlockingStateChangedEvent manualMigrationBlockingStateChangedEvent)
		{
			this._districtsConnected = manualMigrationBlockingStateChangedEvent.IsEnabled;
			this.DrawOrClearConnection();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000297D File Offset: 0x00000B7D
		public bool ShouldDraw
		{
			get
			{
				return this._enabled && this._districtsConnected && this._manualMigrationDistrictSetter.AreDistrictsSet;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000299C File Offset: 0x00000B9C
		public void DrawOrClearConnection()
		{
			if (this.ShouldDraw)
			{
				this.DrawConnection();
				this.HighlightDistricts();
				return;
			}
			this.Clear();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029BC File Offset: 0x00000BBC
		public void DrawConnection()
		{
			Vector3 connectionPoint = DistrictConnectionDrawingService.GetConnectionPoint(this._manualMigrationDistrictSetter.LeftDistrict);
			Vector3 connectionPoint2 = DistrictConnectionDrawingService.GetConnectionPoint(this._manualMigrationDistrictSetter.RightDistrict);
			this._districtConnectionLineRenderer.BuildMesh(connectionPoint, connectionPoint2);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029F8 File Offset: 0x00000BF8
		public static Vector3 GetConnectionPoint(DistrictCenter districtCenter)
		{
			Vector3 coordinates = CoordinateSystem.WorldToGrid(districtCenter.GetComponent<ConnectionAnchorPointSpec>().Position);
			return CoordinateSystem.GridToWorld(districtCenter.GetComponent<BlockObject>().TransformCoordinates(coordinates));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A28 File Offset: 0x00000C28
		public void HighlightDistricts()
		{
			this._highlighter.UnhighlightAllSecondary();
			this._highlighter.HighlightSecondary(this._manualMigrationDistrictSetter.LeftDistrict, this._connectionHighlightColor);
			this._highlighter.HighlightSecondary(this._manualMigrationDistrictSetter.RightDistrict, this._connectionHighlightColor);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A78 File Offset: 0x00000C78
		public void Clear()
		{
			this._districtConnectionLineRenderer.Clear();
			this._highlighter.UnhighlightAllSecondary();
		}

		// Token: 0x04000028 RID: 40
		public readonly ISpecService _specService;

		// Token: 0x04000029 RID: 41
		public readonly DistrictConnectionLineRenderer _districtConnectionLineRenderer;

		// Token: 0x0400002A RID: 42
		public readonly EventBus _eventBus;

		// Token: 0x0400002B RID: 43
		public readonly Highlighter _highlighter;

		// Token: 0x0400002C RID: 44
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x0400002D RID: 45
		public bool _enabled;

		// Token: 0x0400002E RID: 46
		public bool _districtsConnected;

		// Token: 0x0400002F RID: 47
		public Color _connectionHighlightColor;
	}
}
