using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x02000021 RID: 33
	public class MechanicalNodeSelfMarkerDrawer : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener, IPreviewSelectionListener
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003A4B File Offset: 0x00001C4B
		public MechanicalNodeSelfMarkerDrawer(MarkerDrawerFactory markerDrawerFactory, MarkerMatrix4x4Calculator markerMatrix4X4Calculator)
		{
			this._markerDrawerFactory = markerDrawerFactory;
			this._markerMatrix4X4Calculator = markerMatrix4X4Calculator;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A6C File Offset: 0x00001C6C
		public void Awake()
		{
			base.DisableComponent();
			this._blockObject = base.GetComponent<BlockObject>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._transputProviderSpec = base.GetComponent<TransputProviderSpec>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._meshDrawer = (this._mechanicalNode.IsGenerator ? this._markerDrawerFactory.CreateMechanicalOutputMarkerDrawer(MechanicalNodeSelfMarkerDrawer.MarkerColor) : this._markerDrawerFactory.CreateMechanicalInputMarkerDrawer(MechanicalNodeSelfMarkerDrawer.MarkerColor));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public void Update()
		{
			if (this._blockObjectModelController.IsAnyModelShown)
			{
				if (new ImmutableArray<Transput>?(this._mechanicalNode.Transputs) == null)
				{
					this.GetMarkersFromSpec();
				}
				this._meshDrawer.DrawMultiple(this._markers);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B38 File Offset: 0x00001D38
		public void OnSelect()
		{
			this.Enable();
			if (base.Enabled && new ImmutableArray<Transput>?(this._mechanicalNode.Transputs) != null)
			{
				this.GetMarkersFromMechanicalNode();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B79 File Offset: 0x00001D79
		public void OnUnselect()
		{
			base.DisableComponent();
			this._markers.Clear();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003B8C File Offset: 0x00001D8C
		public void OnPreviewSelect()
		{
			this.Enable();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B94 File Offset: 0x00001D94
		public void OnPreviewUnselect()
		{
			this.OnUnselect();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B9C File Offset: 0x00001D9C
		public void Enable()
		{
			if (this._transputProviderSpec != null && this._mechanicalNode.IsGenerator)
			{
				base.EnableComponent();
				return;
			}
			base.DisableComponent();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public void GetMarkersFromSpec()
		{
			this._markers.Clear();
			foreach (TransputSpec transputSpec in this._transputProviderSpec.Transputs)
			{
				foreach (Direction3D direction in transputSpec.Directions.GetEnumerator())
				{
					Transput transput = new Transput(null, transputSpec, direction, this._blockObject);
					this._markers.Add(this._markerMatrix4X4Calculator.CalculateMatrixFrom(transput));
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C5C File Offset: 0x00001E5C
		public void GetMarkersFromMechanicalNode()
		{
			foreach (Transput transput in this._mechanicalNode.Transputs)
			{
				if (!transput.Connected)
				{
					this.AddMarker(transput);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C9F File Offset: 0x00001E9F
		public void AddMarker(Transput transput)
		{
			this._markers.Add(this._markerMatrix4X4Calculator.CalculateMatrixFrom(transput));
		}

		// Token: 0x0400006A RID: 106
		public static readonly Color MarkerColor = new Color(0.3f, 0.5f, 0.8f, 0.75f);

		// Token: 0x0400006B RID: 107
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400006C RID: 108
		public readonly MarkerMatrix4x4Calculator _markerMatrix4X4Calculator;

		// Token: 0x0400006D RID: 109
		public MeshDrawer _meshDrawer;

		// Token: 0x0400006E RID: 110
		public BlockObject _blockObject;

		// Token: 0x0400006F RID: 111
		public MechanicalNode _mechanicalNode;

		// Token: 0x04000070 RID: 112
		public TransputProviderSpec _transputProviderSpec;

		// Token: 0x04000071 RID: 113
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x04000072 RID: 114
		public readonly List<Matrix4x4> _markers = new List<Matrix4x4>();
	}
}
