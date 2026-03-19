using System;
using System.Collections.Generic;
using Timberborn.EntitySystem;
using Timberborn.GameDistricts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200000A RID: 10
	public class ManualMigrationDistrictColumn
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002302 File Offset: 0x00000502
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000230A File Offset: 0x0000050A
		public DistrictCenter DistrictCenter { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002313 File Offset: 0x00000513
		public ManualMigrationDistrictColumn(ManualMigrationDistrictDropdown manualMigrationDistrictDropdown, IReadOnlyList<ManualMigrationPopulationRow> manualMigrationPopulationRows, Image icon, VisualElement parent)
		{
			this._manualMigrationDistrictDropdown = manualMigrationDistrictDropdown;
			this._manualMigrationPopulationRows = manualMigrationPopulationRows;
			this._icon = icon;
			this._parent = parent;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002338 File Offset: 0x00000538
		public void Show()
		{
			this.Unhighlight();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002340 File Offset: 0x00000540
		public void SetDistricts(DistrictCenter source, DistrictCenter target)
		{
			this.DistrictCenter = source;
			this._icon.sprite = source.GetComponent<LabeledEntity>().Image;
			this._manualMigrationDistrictDropdown.SetDistrict(source);
			foreach (ManualMigrationPopulationRow manualMigrationPopulationRow in this._manualMigrationPopulationRows)
			{
				manualMigrationPopulationRow.SetDistricts(source, target);
			}
			this.Update();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023BC File Offset: 0x000005BC
		public void Update()
		{
			foreach (ManualMigrationPopulationRow manualMigrationPopulationRow in this._manualMigrationPopulationRows)
			{
				manualMigrationPopulationRow.UpdateRow();
			}
			if (this._highlightTimer > 0f)
			{
				this._highlightTimer -= Time.unscaledDeltaTime;
				return;
			}
			this.Unhighlight();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000242C File Offset: 0x0000062C
		public void Highlight()
		{
			this._parent.EnableInClassList(ManualMigrationDistrictColumn.DistrictHighlightClass, true);
			this.ResetTimer();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002445 File Offset: 0x00000645
		public void ResetTimer()
		{
			this._highlightTimer = ManualMigrationDistrictColumn.HighlightTime;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002452 File Offset: 0x00000652
		public void Unhighlight()
		{
			this._parent.EnableInClassList(ManualMigrationDistrictColumn.DistrictHighlightClass, false);
		}

		// Token: 0x0400000F RID: 15
		public static readonly string DistrictHighlightClass = "manual-migration-row__highlight--on";

		// Token: 0x04000010 RID: 16
		public static readonly float HighlightTime = 0.3f;

		// Token: 0x04000012 RID: 18
		public readonly ManualMigrationDistrictDropdown _manualMigrationDistrictDropdown;

		// Token: 0x04000013 RID: 19
		public readonly IReadOnlyList<ManualMigrationPopulationRow> _manualMigrationPopulationRows;

		// Token: 0x04000014 RID: 20
		public readonly Image _icon;

		// Token: 0x04000015 RID: 21
		public readonly VisualElement _parent;

		// Token: 0x04000016 RID: 22
		public float _highlightTimer;
	}
}
