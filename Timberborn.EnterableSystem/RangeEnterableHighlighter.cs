using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.BlockSystemUI;
using Timberborn.BlueprintSystem;
using Timberborn.BuildingRange;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.EnterableSystem
{
	// Token: 0x0200001B RID: 27
	public class RangeEnterableHighlighter : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener, IPreviewSelectionListener, IPostPlacementChangeListener
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x000036B4 File Offset: 0x000018B4
		public RangeEnterableHighlighter(BlockObjectBoundsDrawerFactory blockObjectBoundsDrawerFactory, EntityComponentRegistry entityComponentRegistry, ISpecService specService)
		{
			this._blockObjectBoundsDrawerFactory = blockObjectBoundsDrawerFactory;
			this._entityComponentRegistry = entityComponentRegistry;
			this._specService = specService;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000036DC File Offset: 0x000018DC
		public void Awake()
		{
			this._buildingWithRange = base.GetComponent<IBuildingWithRange>();
			RangeEnterableHighlighterSpec singleSpec = this._specService.GetSingleSpec<RangeEnterableHighlighterSpec>();
			this._blockObjectBoundsDrawer = this._blockObjectBoundsDrawerFactory.Create(singleSpec.BuildingInRange);
			base.DisableComponent();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000371E File Offset: 0x0000191E
		public void Update()
		{
			this.HighlightBuildings();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003726 File Offset: 0x00001926
		public void OnSelect()
		{
			this.RecalculateBlocks();
			base.EnableComponent();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003734 File Offset: 0x00001934
		public void OnUnselect()
		{
			this._blocks.Clear();
			base.DisableComponent();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000371E File Offset: 0x0000191E
		public void OnPreviewSelect()
		{
			this.HighlightBuildings();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003747 File Offset: 0x00001947
		public void OnPreviewUnselect()
		{
			this.OnUnselect();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000374F File Offset: 0x0000194F
		public void OnPostPlacementChanged()
		{
			this.RecalculateBlocks();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003757 File Offset: 0x00001957
		public void RecalculateBlocks()
		{
			this._blocks.Clear();
			this._blocks.AddRange(this._buildingWithRange.GetBlocksInRange().XY());
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003780 File Offset: 0x00001980
		public void HighlightBuildings()
		{
			foreach (Enterable enterable in this._entityComponentRegistry.GetEnabled<Enterable>())
			{
				BlockObject component = enterable.GetComponent<BlockObject>();
				if (component.PositionedBlocks.GetAllCoordinates().Any((Vector3Int coordinate) => this._blocks.Contains(coordinate.XY())))
				{
					this._blockObjectBoundsDrawer.DrawBounds(component);
				}
			}
		}

		// Token: 0x04000038 RID: 56
		public readonly BlockObjectBoundsDrawerFactory _blockObjectBoundsDrawerFactory;

		// Token: 0x04000039 RID: 57
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400003A RID: 58
		public readonly ISpecService _specService;

		// Token: 0x0400003B RID: 59
		public IBuildingWithRange _buildingWithRange;

		// Token: 0x0400003C RID: 60
		public BlockObjectBoundsDrawer _blockObjectBoundsDrawer;

		// Token: 0x0400003D RID: 61
		public readonly HashSet<Vector2Int> _blocks = new HashSet<Vector2Int>();
	}
}
