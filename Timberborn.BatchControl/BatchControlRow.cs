using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000011 RID: 17
	public class BatchControlRow
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D30 File Offset: 0x00000F30
		public VisualElement Root { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D38 File Offset: 0x00000F38
		public EntityComponent Entity { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D40 File Offset: 0x00000F40
		public Func<bool> VisibilityGetter { get; } = () => true;

		// Token: 0x0600004D RID: 77 RVA: 0x00002D48 File Offset: 0x00000F48
		public BatchControlRow(VisualElement root, params IBatchControlRowItem[] batchControlRowItems)
		{
			this.Root = root;
			foreach (IBatchControlRowItem batchControlRowItem in batchControlRowItems)
			{
				this.AddItem(batchControlRowItem);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DCE File Offset: 0x00000FCE
		public BatchControlRow(VisualElement root, EntityComponent entity, params IBatchControlRowItem[] batchControlRowItems) : this(root, batchControlRowItems)
		{
			this.Entity = entity;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DDF File Offset: 0x00000FDF
		public BatchControlRow(VisualElement root, EntityComponent entity, Func<bool> visibilityGetter, params IBatchControlRowItem[] batchControlRowItems) : this(root, batchControlRowItems)
		{
			this.Entity = entity;
			this.VisibilityGetter = visibilityGetter;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public void UpdateItems()
		{
			this.LoadItems();
			foreach (IUpdatableBatchControlRowItem updatableBatchControlRowItem in this._updatableBatchControlRowItems)
			{
				updatableBatchControlRowItem.UpdateRowItem();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E50 File Offset: 0x00001050
		public void ClearItems()
		{
			if (this._loaded)
			{
				foreach (IClearableBatchControlRowItem clearableBatchControlRowItem in this._clearableBatchControlRowItems)
				{
					clearableBatchControlRowItem.ClearRowItem();
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EA8 File Offset: 0x000010A8
		public void SetEntityBatchControlsFinishedState(bool isFinished)
		{
			foreach (IFinishableBatchControlRowItem finishableBatchControlRowItem in this._finishableBatchControlRowItems)
			{
				finishableBatchControlRowItem.SetFinishedState(isFinished);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EFC File Offset: 0x000010FC
		public void AddItem(IBatchControlRowItem batchControlRowItem)
		{
			if (batchControlRowItem != null)
			{
				this._batchControlRowItems.Add(batchControlRowItem);
				IUpdatableBatchControlRowItem updatableBatchControlRowItem = batchControlRowItem as IUpdatableBatchControlRowItem;
				if (updatableBatchControlRowItem != null)
				{
					this._updatableBatchControlRowItems.Add(updatableBatchControlRowItem);
				}
				IClearableBatchControlRowItem clearableBatchControlRowItem = batchControlRowItem as IClearableBatchControlRowItem;
				if (clearableBatchControlRowItem != null)
				{
					this._clearableBatchControlRowItems.Add(clearableBatchControlRowItem);
				}
				IFinishableBatchControlRowItem finishableBatchControlRowItem = batchControlRowItem as IFinishableBatchControlRowItem;
				if (finishableBatchControlRowItem != null)
				{
					this._finishableBatchControlRowItems.Add(finishableBatchControlRowItem);
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F5C File Offset: 0x0000115C
		public void LoadItems()
		{
			if (!this._loaded)
			{
				foreach (IBatchControlRowItem batchControlRowItem in this._batchControlRowItems)
				{
					this.Root.Add(batchControlRowItem.Root);
				}
				if (this.Entity)
				{
					BlockObject component = this.Entity.GetComponent<BlockObject>();
					if (component != null)
					{
						this.SetEntityBatchControlsFinishedState(component.IsFinished);
					}
				}
				this._loaded = true;
			}
		}

		// Token: 0x0400003C RID: 60
		public readonly List<IBatchControlRowItem> _batchControlRowItems = new List<IBatchControlRowItem>();

		// Token: 0x0400003D RID: 61
		public readonly List<IUpdatableBatchControlRowItem> _updatableBatchControlRowItems = new List<IUpdatableBatchControlRowItem>();

		// Token: 0x0400003E RID: 62
		public readonly List<IClearableBatchControlRowItem> _clearableBatchControlRowItems = new List<IClearableBatchControlRowItem>();

		// Token: 0x0400003F RID: 63
		public readonly List<IFinishableBatchControlRowItem> _finishableBatchControlRowItems = new List<IFinishableBatchControlRowItem>();

		// Token: 0x04000040 RID: 64
		public bool _loaded;
	}
}
