using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000019 RID: 25
	public abstract class BatchControlTab : ILoadableSingleton
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000036B0 File Offset: 0x000018B0
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000036B8 File Offset: 0x000018B8
		public bool IsDirty { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x000036C1 File Offset: 0x000018C1
		public BatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._batchControlDistrict = batchControlDistrict;
			this._eventBus = eventBus;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000081 RID: 129
		public abstract string TabNameLocKey { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000082 RID: 130
		public abstract string TabImage { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000083 RID: 131
		public abstract string BindingKey { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002200 File Offset: 0x00000400
		public virtual bool IgnoreDistrictSelection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002FFC File Offset: 0x000011FC
		public virtual bool MiddleRowVisible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036E9 File Offset: 0x000018E9
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036F8 File Offset: 0x000018F8
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			EntityComponent component = enteredFinishedStateEvent.BlockObject.GetComponent<EntityComponent>();
			this.UpdateEntityControlsFinishedState(component, true);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000371C File Offset: 0x0000191C
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			EntityComponent component = enteredUnfinishedStateEvent.BlockObject.GetComponent<EntityComponent>();
			this.UpdateEntityControlsFinishedState(component, false);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003740 File Offset: 0x00001940
		public VisualElement GetContent(IEnumerable<EntityComponent> entities)
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlTab");
			this._empty = UQueryExtensions.Q<Label>(this._root, "EmptyText", null);
			VisualElement header = this.GetHeader();
			if (header != null)
			{
				UQueryExtensions.Q<VisualElement>(this._root, "Header", null).Add(header);
			}
			this._rowsLabel = UQueryExtensions.Q<Label>(this._root, "RowsLabel", null);
			this._rowsLabel.text = this.GetRowsLabel();
			this._rowGroupsContainer = UQueryExtensions.Q<ScrollView>(this._root, "RowsGroups", null);
			foreach (BatchControlRowGroup rowGroup in this.GetRowGroups(entities))
			{
				this.AddGroup(rowGroup);
			}
			this.IsDirty = false;
			return this._root;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003828 File Offset: 0x00001A28
		public void UpdateRowsVisibility()
		{
			bool flag = false;
			foreach (BatchControlRowGroup batchControlRowGroup in this._rowGroups)
			{
				DistrictCenter selectedDistrict = this.IgnoreDistrictSelection ? null : this._batchControlDistrict.SelectedDistrict;
				bool flag2 = batchControlRowGroup.UpdateVisibleRows(selectedDistrict);
				flag = (flag || flag2);
			}
			this._empty.ToggleDisplayStyle(!flag);
			this._rowsLabel.ToggleDisplayStyle(!string.IsNullOrEmpty(this._rowsLabel.text) && flag);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000038C8 File Offset: 0x00001AC8
		public void ShowTab()
		{
			this.Show();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000038D0 File Offset: 0x00001AD0
		public void UpdateContent()
		{
			this.Update();
			ValueTuple<float, float> bounds = this.GetBounds();
			float item = bounds.Item1;
			float item2 = bounds.Item2;
			foreach (BatchControlRowGroup batchControlRowGroup in this._rowGroups)
			{
				batchControlRowGroup.UpdateContent(item, item2);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000393C File Offset: 0x00001B3C
		public void HideTab()
		{
			this.Hide();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003944 File Offset: 0x00001B44
		public void Clear()
		{
			foreach (BatchControlRowGroup batchControlRowGroup in this._rowGroups)
			{
				batchControlRowGroup.Clear();
			}
			this._rowGroups.Clear();
			this._rowGroupsContainer = null;
			this._empty = null;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039B0 File Offset: 0x00001BB0
		public IEnumerable<BatchControlRow> GetEntityRows(EntityComponent entity)
		{
			return this._rowGroups.SelectMany((BatchControlRowGroup group) => group.GetEntityRows(entity));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039E4 File Offset: 0x00001BE4
		public void RemoveEntityRows(EntityComponent entity)
		{
			for (int i = this._rowGroups.Count - 1; i >= 0; i--)
			{
				BatchControlRowGroup rowGroup = this._rowGroups[i];
				BatchControlTab.RemoveEntityFromGroup(entity, rowGroup);
				this.RemoveGroupIfNeeded(rowGroup);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002200 File Offset: 0x00000400
		public virtual bool RemoveEmptyRowGroups
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A24 File Offset: 0x00001C24
		public void AddGroup(BatchControlRowGroup rowGroup)
		{
			this._rowGroups.Add(rowGroup);
			this._rowGroups.Sort((BatchControlRowGroup x, BatchControlRowGroup y) => string.CompareOrdinal(x.SortingKey, y.SortingKey));
			this._rowGroupsContainer.Insert(this._rowGroups.IndexOf(rowGroup), rowGroup.Root);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A84 File Offset: 0x00001C84
		public virtual void Show()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A84 File Offset: 0x00001C84
		public virtual void Update()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A84 File Offset: 0x00001C84
		public virtual void Hide()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A86 File Offset: 0x00001C86
		public virtual VisualElement GetHeader()
		{
			return null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A89 File Offset: 0x00001C89
		public virtual string GetRowsLabel()
		{
			return string.Empty;
		}

		// Token: 0x06000098 RID: 152
		public abstract IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities);

		// Token: 0x06000099 RID: 153 RVA: 0x00003A90 File Offset: 0x00001C90
		public void HideContent()
		{
			ScrollView rowGroupsContainer = this._rowGroupsContainer;
			if (rowGroupsContainer == null)
			{
				return;
			}
			rowGroupsContainer.ToggleDisplayStyle(false);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public void UpdateEntityControlsFinishedState(EntityComponent entity, bool isFinished)
		{
			foreach (BatchControlRowGroup batchControlRowGroup in this._rowGroups)
			{
				foreach (BatchControlRow batchControlRow in batchControlRowGroup.GetEntityRows(entity))
				{
					batchControlRow.SetEntityBatchControlsFinishedState(isFinished);
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B2C File Offset: 0x00001D2C
		public static void RemoveEntityFromGroup(EntityComponent entity, BatchControlRowGroup rowGroup)
		{
			foreach (BatchControlRow batchControlRow in rowGroup.GetEntityRows(entity).ToImmutableArray<BatchControlRow>())
			{
				rowGroup.RemoveRow(batchControlRow);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003B68 File Offset: 0x00001D68
		public void RemoveGroupIfNeeded(BatchControlRowGroup rowGroup)
		{
			if (this.RemoveEmptyRowGroups && rowGroup.IsEmpty)
			{
				this._rowGroups.Remove(rowGroup);
				this._rowGroupsContainer.Remove(rowGroup.Root);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003B98 File Offset: 0x00001D98
		[return: TupleElementNames(new string[]
		{
			"top",
			"bottom"
		})]
		public ValueTuple<float, float> GetBounds()
		{
			Rect worldBound = this._rowGroupsContainer.contentViewport.worldBound;
			float y = worldBound.y;
			return new ValueTuple<float, float>(y + worldBound.height, y);
		}

		// Token: 0x04000056 RID: 86
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000057 RID: 87
		public readonly BatchControlDistrict _batchControlDistrict;

		// Token: 0x04000058 RID: 88
		public readonly EventBus _eventBus;

		// Token: 0x04000059 RID: 89
		public VisualElement _root;

		// Token: 0x0400005A RID: 90
		public Label _empty;

		// Token: 0x0400005B RID: 91
		public Label _rowsLabel;

		// Token: 0x0400005C RID: 92
		public ScrollView _rowGroupsContainer;

		// Token: 0x0400005D RID: 93
		public readonly List<BatchControlRowGroup> _rowGroups = new List<BatchControlRowGroup>();
	}
}
