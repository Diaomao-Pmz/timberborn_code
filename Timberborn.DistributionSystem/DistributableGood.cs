using System;

namespace Timberborn.DistributionSystem
{
	// Token: 0x02000007 RID: 7
	public readonly struct DistributableGood : IComparable<DistributableGood>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public int Stock { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public int Capacity { get; }

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public DistributableGood(int stock, int capacity, GoodDistributionSetting goodDistributionSetting)
		{
			this.Stock = stock;
			this.Capacity = capacity;
			this._goodDistributionSetting = goodDistributionSetting;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002127 File Offset: 0x00000327
		public float FillRate
		{
			get
			{
				if (this.Capacity != 0)
				{
					return (float)this.Stock / (float)this.Capacity;
				}
				if (this.Stock != 0)
				{
					return 1f;
				}
				return 0f;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		public bool CanExport
		{
			get
			{
				return this._goodDistributionSetting.ExportThreshold < 1f && this.ExportRate > 0f;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002177 File Offset: 0x00000377
		public float MaxExportAmount
		{
			get
			{
				return this.ExportRate * (float)this.Capacity;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002187 File Offset: 0x00000387
		public int FreeCapacity
		{
			get
			{
				return this.Capacity - this.Stock;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002196 File Offset: 0x00000396
		public string GoodId
		{
			get
			{
				return this._goodDistributionSetting.GoodId;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A3 File Offset: 0x000003A3
		public void UpdateLastImportTimestamp(float timestamp)
		{
			this._goodDistributionSetting.LastImportTimestamp = timestamp;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B4 File Offset: 0x000003B4
		public int CompareTo(DistributableGood other)
		{
			int num = this.FillRate.CompareTo(other.FillRate);
			if (num != 0)
			{
				return num;
			}
			return this._goodDistributionSetting.LastImportTimestamp.CompareTo(other._goodDistributionSetting.LastImportTimestamp);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021FA File Offset: 0x000003FA
		public float ExportRate
		{
			get
			{
				return this.FillRate - this._goodDistributionSetting.ExportThreshold;
			}
		}

		// Token: 0x0400000A RID: 10
		public readonly GoodDistributionSetting _goodDistributionSetting;
	}
}
