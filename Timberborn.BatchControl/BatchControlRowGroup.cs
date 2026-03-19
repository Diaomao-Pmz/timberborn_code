using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000013 RID: 19
	public class BatchControlRowGroup
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002FFF File Offset: 0x000011FF
		public VisualElement Root { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003007 File Offset: 0x00001207
		public string SortingKey { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000300F File Offset: 0x0000120F
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003017 File Offset: 0x00001217
		public int VisibleChildrenCount { get; private set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00003020 File Offset: 0x00001220
		public BatchControlRowGroup(VisualElement root, string sortingKey, BatchControlRow headerRow, IComparer<BatchControlRow> comparer)
		{
			this.Root = root;
			this.SortingKey = sortingKey;
			this._headerRow = headerRow;
			this._comparer = comparer;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003050 File Offset: 0x00001250
		public bool IsEmpty
		{
			get
			{
				return this._rows.Count == 0;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003060 File Offset: 0x00001260
		public void AddRow(BatchControlRow batchControlRow)
		{
			int count;
			if (this._comparer != null)
			{
				this._rows.InsertSorted(batchControlRow, this._comparer, out count);
			}
			else
			{
				count = this._rows.Count;
				this._rows.Add(batchControlRow);
			}
			this.Root.Insert(count + 1, batchControlRow.Root);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030B7 File Offset: 0x000012B7
		public void RemoveRow(BatchControlRow batchControlRow)
		{
			this._rows.Remove(batchControlRow);
			batchControlRow.ClearItems();
			batchControlRow.Root.RemoveFromHierarchy();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000030D8 File Offset: 0x000012D8
		public bool UpdateVisibleRows(DistrictCenter selectedDistrict)
		{
			this.VisibleChildrenCount = 0;
			foreach (BatchControlRow batchControlRow in this._rows)
			{
				EntityComponent entity = batchControlRow.Entity;
				bool flag = (!selectedDistrict || !entity || BatchControlRowGroup.BelongsToDistrict(entity, selectedDistrict)) && batchControlRow.VisibilityGetter();
				batchControlRow.Root.ToggleDisplayStyle(flag);
				if (flag)
				{
					int visibleChildrenCount = this.VisibleChildrenCount;
					this.VisibleChildrenCount = visibleChildrenCount + 1;
				}
			}
			bool flag2 = this.VisibleChildrenCount > 0;
			this._headerRow.Root.ToggleDisplayStyle(flag2);
			return flag2;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000031A0 File Offset: 0x000013A0
		public void UpdateContent(float topBound, float bottomBound)
		{
			BatchControlRowGroup.UpdateContent(topBound, bottomBound, this._headerRow);
			foreach (BatchControlRow row in this._rows)
			{
				BatchControlRowGroup.UpdateContent(topBound, bottomBound, row);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003204 File Offset: 0x00001404
		public void Clear()
		{
			this._headerRow.ClearItems();
			foreach (BatchControlRow batchControlRow in this._rows)
			{
				batchControlRow.ClearItems();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003260 File Offset: 0x00001460
		public IEnumerable<BatchControlRow> GetEntityRows(EntityComponent entity)
		{
			return from row in (from row in this._rows
			select row).Append(this._headerRow)
			where row.Entity == entity
			select row;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032C0 File Offset: 0x000014C0
		public static bool BelongsToDistrict(EntityComponent entity, DistrictCenter district)
		{
			Citizen component = entity.GetComponent<Citizen>();
			if (component != null)
			{
				return component.AssignedDistrict == district;
			}
			DistrictBuilding component2 = entity.GetComponent<DistrictBuilding>();
			if (component2 != null)
			{
				DistrictCenter instantOrConstructionDistrict = component2.GetInstantOrConstructionDistrict();
				if (instantOrConstructionDistrict && instantOrConstructionDistrict == district)
				{
					return true;
				}
				BuildingAccessible component3 = entity.GetComponent<BuildingAccessible>();
				if (component3 != null)
				{
					return district.IsOnPreviewDistrictRoad(component3.CalculateAccess());
				}
			}
			return false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003318 File Offset: 0x00001518
		public static void UpdateContent(float topBound, float bottomBound, BatchControlRow row)
		{
			if (BatchControlRowGroup.Contains(row.Root, topBound, bottomBound))
			{
				row.UpdateItems();
				row.Root.style.visibility = 0;
				return;
			}
			row.Root.style.visibility = 1;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003368 File Offset: 0x00001568
		public static bool Contains(VisualElement element, float topBound, float bottomBound)
		{
			Rect worldBound = element.worldBound;
			return worldBound.y < topBound && worldBound.y + worldBound.height >= bottomBound;
		}

		// Token: 0x04000046 RID: 70
		public readonly BatchControlRow _headerRow;

		// Token: 0x04000047 RID: 71
		public readonly IComparer<BatchControlRow> _comparer;

		// Token: 0x04000048 RID: 72
		public readonly List<BatchControlRow> _rows = new List<BatchControlRow>();
	}
}
