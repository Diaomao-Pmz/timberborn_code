using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000E RID: 14
	public class EntranceMarkerDrawer : BaseComponent, IAwakableComponent, IStartableComponent, ILateUpdatableComponent, ISelectionListener, IPreviewSelectionListener
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000028EA File Offset: 0x00000AEA
		public EntranceMarkerDrawer(MarkerDrawerFactory markerDrawerFactory)
		{
			this._markerDrawerFactory = markerDrawerFactory;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028F9 File Offset: 0x00000AF9
		public void Awake()
		{
			base.DisableComponent();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002901 File Offset: 0x00000B01
		public void Start()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._entranceMarkerMeshDrawer = this._markerDrawerFactory.CreateEntranceMarkerDrawer();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000292C File Offset: 0x00000B2C
		public void LateUpdate()
		{
			BlockObjectModelController blockObjectModelController = this._blockObjectModelController;
			if ((blockObjectModelController == null || blockObjectModelController.IsAnyModelShown) && this._blockObject.HasEntrance)
			{
				this.DrawEntrance();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002955 File Offset: 0x00000B55
		public void OnSelect()
		{
			base.EnableComponent();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028F9 File Offset: 0x00000AF9
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002955 File Offset: 0x00000B55
		public void OnPreviewSelect()
		{
			base.EnableComponent();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028F9 File Offset: 0x00000AF9
		public void OnPreviewUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002960 File Offset: 0x00000B60
		public void DrawEntrance()
		{
			PositionedEntrance positionedEntrance = this._blockObject.PositionedEntrance;
			Vector3Int coordinates = positionedEntrance.Coordinates;
			Quaternion rotation = Quaternion.AngleAxis(positionedEntrance.Direction2D.Across().ToAngle(), Vector3.up);
			this._entranceMarkerMeshDrawer.DrawAtCoordinates(coordinates, EntranceMarkerDrawer.EntranceMarkerYOffset, rotation);
		}

		// Token: 0x04000019 RID: 25
		public static readonly float EntranceMarkerYOffset = 0.2f;

		// Token: 0x0400001A RID: 26
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400001B RID: 27
		public BlockObject _blockObject;

		// Token: 0x0400001C RID: 28
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x0400001D RID: 29
		public MeshDrawer _entranceMarkerMeshDrawer;
	}
}
