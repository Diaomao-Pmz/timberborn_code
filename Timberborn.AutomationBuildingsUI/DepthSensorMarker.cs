using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200000E RID: 14
	public class DepthSensorMarker : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002D61 File Offset: 0x00000F61
		public DepthSensorMarker(MarkerDrawerFactory markerDrawerFactory)
		{
			this._markerDrawerFactory = markerDrawerFactory;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D70 File Offset: 0x00000F70
		public void Awake()
		{
			this._depthSensor = base.GetComponent<DepthSensor>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._markerDrawer = this._markerDrawerFactory.CreateTileDrawer(DepthSensorMarker.MarkerColor);
			base.DisableComponent();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void Update()
		{
			Vector3Int sensorCoordinates = this._depthSensor.SensorCoordinates;
			this._markerDrawer.DrawAtCoordinates(new Vector3Int(sensorCoordinates.x, sensorCoordinates.y, 0), this._depthSensor.Threshold + DepthSensorMarker.MarkerYOffset);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public void OnSelect()
		{
			if (!this._blockObject.IsPreview)
			{
				base.EnableComponent();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E0A File Offset: 0x0000100A
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x04000047 RID: 71
		public static readonly Color32 MarkerColor = Color.blue;

		// Token: 0x04000048 RID: 72
		public static readonly float MarkerYOffset = 0.02f;

		// Token: 0x04000049 RID: 73
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400004A RID: 74
		public DepthSensor _depthSensor;

		// Token: 0x0400004B RID: 75
		public MeshDrawer _markerDrawer;

		// Token: 0x0400004C RID: 76
		public BlockObject _blockObject;
	}
}
