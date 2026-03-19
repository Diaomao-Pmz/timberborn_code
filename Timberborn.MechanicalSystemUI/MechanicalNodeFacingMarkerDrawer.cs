using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200001C RID: 28
	public class MechanicalNodeFacingMarkerDrawer : BaseComponent, IAwakableComponent, IUpdatableComponent, IPreviewSelectionListener, IPostPlacementChangeListener
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003434 File Offset: 0x00001634
		public MechanicalNodeFacingMarkerDrawer(MarkerDrawerFactory markerDrawerFactory, IBlockService blockService, MarkerMatrix4x4Calculator markerMatrix4X4Calculator)
		{
			this._markerDrawerFactory = markerDrawerFactory;
			this._blockService = blockService;
			this._markerMatrix4X4Calculator = markerMatrix4X4Calculator;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003468 File Offset: 0x00001668
		public void Awake()
		{
			base.DisableComponent();
			this._blockObject = base.GetComponent<BlockObject>();
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._transputProviderSpec = base.GetComponent<TransputProviderSpec>();
			this._preview = base.GetComponent<Preview>();
			this._meshDrawer = this._markerDrawerFactory.CreateMechanicalInputMarkerDrawer(MechanicalNodeFacingMarkerDrawer.MarkerColor);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034C1 File Offset: 0x000016C1
		public void Update()
		{
			this._meshDrawer.DrawMultiple(this._markers);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034D4 File Offset: 0x000016D4
		public void OnPreviewSelect()
		{
			if (this.ShouldEnable(this._preview.PreviewState.IsSingle))
			{
				base.EnableComponent();
				this.FindFacingTransputs();
				return;
			}
			base.DisableComponent();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000350F File Offset: 0x0000170F
		public void OnPreviewUnselect()
		{
			base.DisableComponent();
			this._markers.Clear();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003522 File Offset: 0x00001722
		public void OnPostPlacementChanged()
		{
			if (base.Enabled)
			{
				this.FindFacingTransputs();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003532 File Offset: 0x00001732
		public bool ShouldEnable(bool isSingleShownPreview)
		{
			return isSingleShownPreview && (this._mechanicalNode.IsGenerator || this._mechanicalNode.IsShaft || this._mechanicalNode.IsIntermediary);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003560 File Offset: 0x00001760
		public void FindFacingTransputs()
		{
			this._markers.Clear();
			foreach (TransputSpec transputSpec in this._transputProviderSpec.Transputs)
			{
				this.FindFacingTransput(transputSpec);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000035A8 File Offset: 0x000017A8
		public void FindFacingTransput(TransputSpec transputSpec)
		{
			foreach (Direction3D direction in transputSpec.Directions.GetEnumerator())
			{
				Transput transput = new Transput(null, transputSpec, direction, this._blockObject);
				BlockOccupations blockOccupations = BlockOccupations.Bottom | BlockOccupations.Middle;
				if (transput.Direction == Direction3D.Bottom)
				{
					blockOccupations |= BlockOccupations.Top;
				}
				Vector3Int vector3Int = transput.Direction.ToOffset();
				for (int i = 1; i <= MechanicalNodeFacingMarkerDrawer.MaxDistance; i++)
				{
					Vector3Int vector3Int2 = vector3Int * i;
					Vector3Int coordinates = transput.Coordinates + vector3Int2;
					if (this.HasConnectableOccupation(coordinates, blockOccupations))
					{
						this.AddMarker(coordinates, transput);
						break;
					}
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003653 File Offset: 0x00001853
		public bool HasConnectableOccupation(Vector3Int coordinates, BlockOccupations occupations)
		{
			this._blockService.GetIntersectingObjectsAt(coordinates, occupations, this._blockObjectCache);
			bool result = this.HasNonOverridableBlockObject();
			this._blockObjectCache.Clear();
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000367C File Offset: 0x0000187C
		public bool HasNonOverridableBlockObject()
		{
			using (List<BlockObject>.Enumerator enumerator = this._blockObjectCache.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Overridable)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036D8 File Offset: 0x000018D8
		public void AddMarker(Vector3Int coordinates, Transput transput)
		{
			Transput transput2 = MechanicalNodeFacingMarkerDrawer.GetTransput(this._blockService.GetBottomObjectComponentAt<MechanicalNode>(coordinates), coordinates, transput);
			if (transput2 != null)
			{
				this._markers.Add(this._markerMatrix4X4Calculator.CalculateMatrixFrom(transput2));
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003714 File Offset: 0x00001914
		public static Transput GetTransput(MechanicalNode otherNode, Vector3Int coordinates, Transput transput)
		{
			if (otherNode && !otherNode.IsShaft)
			{
				return otherNode.Transputs.SingleOrDefault((Transput otherTransput) => otherTransput.Coordinates == coordinates && otherTransput.Direction.Across() == transput.Direction);
			}
			return null;
		}

		// Token: 0x04000053 RID: 83
		public static readonly Color MarkerColor = new Color(0.3f, 0.5f, 0.8f, 0.75f);

		// Token: 0x04000054 RID: 84
		public static readonly int MaxDistance = 20;

		// Token: 0x04000055 RID: 85
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x04000056 RID: 86
		public readonly IBlockService _blockService;

		// Token: 0x04000057 RID: 87
		public readonly MarkerMatrix4x4Calculator _markerMatrix4X4Calculator;

		// Token: 0x04000058 RID: 88
		public MeshDrawer _meshDrawer;

		// Token: 0x04000059 RID: 89
		public BlockObject _blockObject;

		// Token: 0x0400005A RID: 90
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400005B RID: 91
		public TransputProviderSpec _transputProviderSpec;

		// Token: 0x0400005C RID: 92
		public Preview _preview;

		// Token: 0x0400005D RID: 93
		public readonly List<Matrix4x4> _markers = new List<Matrix4x4>();

		// Token: 0x0400005E RID: 94
		public readonly List<BlockObject> _blockObjectCache = new List<BlockObject>();
	}
}
