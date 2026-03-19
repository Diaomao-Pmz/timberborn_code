using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x02000032 RID: 50
	public class WaterInputPipe : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00007378 File Offset: 0x00005578
		public WaterInputPipe(EventBus eventBus, PreviewWaterInputPipeBlockerService previewWaterInputPipeBlockerService)
		{
			this._eventBus = eventBus;
			this._previewWaterInputPipeBlockerService = previewWaterInputPipeBlockerService;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000739C File Offset: 0x0000559C
		public void Awake()
		{
			this._waterInputCoordinates = base.GetComponent<WaterInputCoordinates>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._highlightableObject = base.GetComponent<HighlightableObject>();
			this._waterInputPipeSegmentCreator = base.GetComponent<WaterInputPipeSegmentCreator>();
			this._waterInputCoordinates.CoordinatesChanged += this.OnWaterCoordinatesChanged;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000073FC File Offset: 0x000055FC
		public void InitializeEntity()
		{
			this._eventBus.Register(this);
			this.UpdatePipe();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007410 File Offset: 0x00005610
		public void DeleteEntity()
		{
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000741E File Offset: 0x0000561E
		[OnEvent]
		public void OnPreviewPipeBlockingCoordinatesChanged(PreviewBlockingCoordinatesChangedEvent e)
		{
			if (this.ShouldUpdatePipe(e.ChangedCoordinates))
			{
				this.UpdatePipe();
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007434 File Offset: 0x00005634
		public void OnWaterCoordinatesChanged(object sender, Vector3Int coordinates)
		{
			this.UpdatePipe();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000743C File Offset: 0x0000563C
		public bool ShouldUpdatePipe(ReadOnlyList<Vector3Int> changedCoordinates)
		{
			using (List<Vector3Int>.Enumerator enumerator = changedCoordinates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.XY() == this._waterInputCoordinates.Coordinates.XY())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000074A8 File Offset: 0x000056A8
		public void UpdatePipe()
		{
			int i;
			for (i = 0; i < this._waterInputCoordinates.Depth; i++)
			{
				Vector3Int vector3Int;
				vector3Int..ctor(0, 0, this._waterInputCoordinates.Depth - i - 1);
				Vector3Int gridPosition = this._waterInputCoordinates.Coordinates + vector3Int;
				if (!this.CanShowPipeSegment(gridPosition))
				{
					break;
				}
				this.ShowPipeSegment(i, gridPosition);
			}
			this.DisablePipeSegmentsAfter(i);
			this._blockObjectModelController.UpdateAll();
			this._highlightableObject.RefreshHighlight();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007523 File Offset: 0x00005723
		public bool CanShowPipeSegment(Vector3Int gridPosition)
		{
			return this._blockObject.IsPreview || !this._previewWaterInputPipeBlockerService.IsBlocked(gridPosition);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007544 File Offset: 0x00005744
		public void ShowPipeSegment(int index, Vector3Int gridPosition)
		{
			PipeSegment orCreatePipeSegment = this.GetOrCreatePipeSegment(index);
			Vector3 position = CoordinateSystem.GridToWorldCentered(gridPosition);
			if (this.IsEndSegment(index, gridPosition))
			{
				orCreatePipeSegment.ShowEnd(position);
				return;
			}
			orCreatePipeSegment.ShowMiddle(position);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000757C File Offset: 0x0000577C
		public PipeSegment GetOrCreatePipeSegment(int index)
		{
			if (index >= this._pipeSegments.Count)
			{
				PipeSegment item = this._blockObject.IsFinished ? this._waterInputPipeSegmentCreator.CreateFinished() : this._waterInputPipeSegmentCreator.CreateUnfinished();
				this._pipeSegments.Add(item);
			}
			return this._pipeSegments[index];
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000075D8 File Offset: 0x000057D8
		public bool IsEndSegment(int index, Vector3Int gridPosition)
		{
			Vector3Int coordinates = gridPosition - new Vector3Int(0, 0, 1);
			return this._previewWaterInputPipeBlockerService.IsBlocked(coordinates) || index == this._waterInputCoordinates.Depth - 1;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007614 File Offset: 0x00005814
		public void DisablePipeSegmentsAfter(int index)
		{
			for (int i = index; i < this._pipeSegments.Count; i++)
			{
				this._pipeSegments[i].Hide();
			}
		}

		// Token: 0x040000E4 RID: 228
		public readonly EventBus _eventBus;

		// Token: 0x040000E5 RID: 229
		public readonly PreviewWaterInputPipeBlockerService _previewWaterInputPipeBlockerService;

		// Token: 0x040000E6 RID: 230
		public WaterInputCoordinates _waterInputCoordinates;

		// Token: 0x040000E7 RID: 231
		public BlockObject _blockObject;

		// Token: 0x040000E8 RID: 232
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x040000E9 RID: 233
		public HighlightableObject _highlightableObject;

		// Token: 0x040000EA RID: 234
		public WaterInputPipeSegmentCreator _waterInputPipeSegmentCreator;

		// Token: 0x040000EB RID: 235
		public readonly List<PipeSegment> _pipeSegments = new List<PipeSegment>();
	}
}
