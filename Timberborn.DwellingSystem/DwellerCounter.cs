using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.PopulationStatisticsSystem;

namespace Timberborn.DwellingSystem
{
	// Token: 0x0200000A RID: 10
	public class DwellerCounter : BaseComponent, IAwakableComponent
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000031 RID: 49 RVA: 0x000026AC File Offset: 0x000008AC
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x000026E4 File Offset: 0x000008E4
		public event EventHandler<DwellingChangedEventArgs> DwellerCountChanged;

		// Token: 0x06000033 RID: 51 RVA: 0x0000271C File Offset: 0x0000091C
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._dwelling = base.GetComponent<Dwelling>();
			this._blockableObject.ObjectBlocked += this.OnDwellingChanged;
			this._blockableObject.ObjectUnblocked += this.OnDwellingChanged;
			this._dwelling.NumberOfDwellersChanged += this.OnDwellingChanged;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002788 File Offset: 0x00000988
		public DwellingStatistics GetCurrentDwellingStatistics()
		{
			DwellingStatistics value = this._currentDwellingStatistics.GetValueOrDefault();
			if (this._currentDwellingStatistics == null)
			{
				value = this.GetDwellingStatistics();
				this._currentDwellingStatistics = new DwellingStatistics?(value);
			}
			return this._currentDwellingStatistics.Value;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027CC File Offset: 0x000009CC
		public void OnDwellingChanged(object sender, EventArgs e)
		{
			DwellingStatistics dwellingStatistics = this.GetDwellingStatistics();
			EventHandler<DwellingChangedEventArgs> dwellerCountChanged = this.DwellerCountChanged;
			if (dwellerCountChanged != null)
			{
				dwellerCountChanged(this, new DwellingChangedEventArgs(this.GetCurrentDwellingStatistics(), dwellingStatistics));
			}
			this._currentDwellingStatistics = new DwellingStatistics?(dwellingStatistics);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000280C File Offset: 0x00000A0C
		public DwellingStatistics GetDwellingStatistics()
		{
			if (this._blockableObject.IsUnblocked)
			{
				int numberOfDwellers = this._dwelling.NumberOfDwellers;
				return new DwellingStatistics(numberOfDwellers, this._dwelling.MaxBeavers - numberOfDwellers);
			}
			return new DwellingStatistics(0, 0);
		}

		// Token: 0x04000014 RID: 20
		public BlockableObject _blockableObject;

		// Token: 0x04000015 RID: 21
		public Dwelling _dwelling;

		// Token: 0x04000016 RID: 22
		public DwellingStatistics? _currentDwellingStatistics;
	}
}
