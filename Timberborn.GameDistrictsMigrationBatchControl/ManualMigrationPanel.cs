using System;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;
using Timberborn.GameDistricts;
using Timberborn.GameDistrictsMigration;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.GameDistrictsMigrationBatchControl
{
	// Token: 0x0200000F RID: 15
	public class ManualMigrationPanel
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027B3 File Offset: 0x000009B3
		public VisualElement Root { get; }

		// Token: 0x06000032 RID: 50 RVA: 0x000027BB File Offset: 0x000009BB
		public ManualMigrationPanel(EventBus eventBus, ManualMigrationDistrictSetter manualMigrationDistrictSetter, VisualElement root, ManualMigrationDistrictColumn manualMigrationDistrictColumnLeft, ManualMigrationDistrictColumn manualMigrationDistrictColumnRight)
		{
			this._eventBus = eventBus;
			this._manualMigrationDistrictSetter = manualMigrationDistrictSetter;
			this.Root = root;
			this._manualMigrationDistrictColumnLeft = manualMigrationDistrictColumnLeft;
			this._manualMigrationDistrictColumnRight = manualMigrationDistrictColumnRight;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027E8 File Offset: 0x000009E8
		public void Show()
		{
			this._manualMigrationDistrictSetter.DifferentiateDistricts();
			this.SetDistricts();
			this._eventBus.Register(this);
			this._manualMigrationDistrictColumnLeft.Show();
			this._manualMigrationDistrictColumnRight.Show();
			this._eventBus.Post(new ManualMigrationPanelOpenedEvent());
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002838 File Offset: 0x00000A38
		public void Update()
		{
			if (this._manualMigrationDistrictSetter.AreDistrictsSet)
			{
				this._manualMigrationDistrictColumnLeft.Update();
				this._manualMigrationDistrictColumnRight.Update();
				this.Root.ToggleDisplayStyle(true);
				return;
			}
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002876 File Offset: 0x00000A76
		public void Hide()
		{
			this._eventBus.Post(new ManualMigrationPanelClosedEvent());
			this._eventBus.Unregister(this);
			this._manualMigrationDistrictSetter.ResetRightDistrictChangeCheck();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000289F File Offset: 0x00000A9F
		[OnEvent]
		public void OnMigrationDistrictChangedEvent(MigrationDistrictChangedEvent migrationDistrictChangedEvent)
		{
			this.SetDistricts();
			if (migrationDistrictChangedEvent.HighlightLeftDistrict)
			{
				this._manualMigrationDistrictColumnLeft.Highlight();
			}
			if (migrationDistrictChangedEvent.HighlightRightDistrict)
			{
				this._manualMigrationDistrictColumnRight.Highlight();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028CD File Offset: 0x00000ACD
		[OnEvent]
		public void OnEntityNameChanged(EntityNameChangedEvent entityNameChangedEvent)
		{
			if (entityNameChangedEvent.Entity.GetComponent<DistrictCenter>())
			{
				this.SetDistricts();
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void SetDistricts()
		{
			if (this._manualMigrationDistrictSetter.AreDistrictsSet)
			{
				DistrictCenter leftDistrict = this._manualMigrationDistrictSetter.LeftDistrict;
				DistrictCenter rightDistrict = this._manualMigrationDistrictSetter.RightDistrict;
				this._manualMigrationDistrictColumnLeft.SetDistricts(leftDistrict, rightDistrict);
				this._manualMigrationDistrictColumnRight.SetDistricts(rightDistrict, leftDistrict);
			}
		}

		// Token: 0x04000029 RID: 41
		public readonly EventBus _eventBus;

		// Token: 0x0400002A RID: 42
		public readonly ManualMigrationDistrictSetter _manualMigrationDistrictSetter;

		// Token: 0x0400002B RID: 43
		public readonly ManualMigrationDistrictColumn _manualMigrationDistrictColumnLeft;

		// Token: 0x0400002C RID: 44
		public readonly ManualMigrationDistrictColumn _manualMigrationDistrictColumnRight;
	}
}
