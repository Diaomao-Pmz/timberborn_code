using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200001A RID: 26
	public class WaterDirectionPreviewMarker : BaseComponent, IUpdatableComponent, IAwakableComponent, IPreviewSelectionListener, IPostPlacementChangeListener
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004A86 File Offset: 0x00002C86
		public WaterDirectionPreviewMarker(MarkerDrawerFactory markerDrawerFactory)
		{
			this._markerDrawerFactory = markerDrawerFactory;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A95 File Offset: 0x00002C95
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._meshDrawer = this._markerDrawerFactory.CreateArrowMarkerDrawer();
			base.DisableComponent();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004ABA File Offset: 0x00002CBA
		public void Update()
		{
			this._meshDrawer.Draw(this._marker);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004ACD File Offset: 0x00002CCD
		public void OnPreviewSelect()
		{
			base.EnableComponent();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000027A4 File Offset: 0x000009A4
		public void OnPreviewUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public void OnPostPlacementChanged()
		{
			Quaternion quaternion = Quaternion.AngleAxis(90f + this._blockObject.Orientation.ToAngle(), Vector3.up);
			this._marker = Matrix4x4.TRS(this.GetPosition(), quaternion, Vector3.one);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B20 File Offset: 0x00002D20
		public Vector3 GetPosition()
		{
			return CoordinateSystem.GridToWorld(new Vector3((float)this._blockObject.Coordinates.x + 0.5f, (float)this._blockObject.Coordinates.y + 0.5f, (float)this._blockObject.Coordinates.z + 1.05f));
		}

		// Token: 0x040000B5 RID: 181
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x040000B6 RID: 182
		public MeshDrawer _meshDrawer;

		// Token: 0x040000B7 RID: 183
		public BlockObject _blockObject;

		// Token: 0x040000B8 RID: 184
		public Matrix4x4 _marker;
	}
}
