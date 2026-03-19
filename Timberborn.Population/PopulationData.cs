using System;

namespace Timberborn.Population
{
	// Token: 0x02000008 RID: 8
	public class PopulationData
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002243 File Offset: 0x00000443
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000224B File Offset: 0x0000044B
		public int NumberOfAdults { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000225C File Offset: 0x0000045C
		public int NumberOfChildren { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002265 File Offset: 0x00000465
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000226D File Offset: 0x0000046D
		public int NumberOfBots { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002276 File Offset: 0x00000476
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000227E File Offset: 0x0000047E
		public WorkforceData BeaverWorkforceData { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002287 File Offset: 0x00000487
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000228F File Offset: 0x0000048F
		public WorkforceData BotWorkforceData { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000022A0 File Offset: 0x000004A0
		public BedData BedData { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022A9 File Offset: 0x000004A9
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000022B1 File Offset: 0x000004B1
		public WorkplaceData BeaverWorkplaceData { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000022C2 File Offset: 0x000004C2
		public WorkplaceData BotWorkplaceData { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022CB File Offset: 0x000004CB
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022D3 File Offset: 0x000004D3
		public ContaminationData ContaminationData { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022DC File Offset: 0x000004DC
		public int NumberOfBeavers
		{
			get
			{
				return this.NumberOfAdults + this.NumberOfChildren;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000022EC File Offset: 0x000004EC
		public int NumberOfHealthyAdults
		{
			get
			{
				return this.NumberOfAdults - this.ContaminationData.ContaminatedAdults;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002310 File Offset: 0x00000510
		public int NumberOfHealthyChildren
		{
			get
			{
				return this.NumberOfChildren - this.ContaminationData.ContaminatedChildren;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002332 File Offset: 0x00000532
		public int TotalPopulation
		{
			get
			{
				return this.NumberOfBeavers + this.NumberOfBots;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002344 File Offset: 0x00000544
		public void Update(int numberOfAdults, int numberOfChildren, int numberOfBots, WorkforceData beaverWorkforceData, WorkforceData botWorkforceData, BedData bedData, WorkplaceData beaverWorkplaceData, WorkplaceData botWorkplaceData, ContaminationData contaminationData)
		{
			this.NumberOfAdults = numberOfAdults;
			this.NumberOfChildren = numberOfChildren;
			this.NumberOfBots = numberOfBots;
			this.BeaverWorkforceData = beaverWorkforceData;
			this.BotWorkforceData = botWorkforceData;
			this.BedData = bedData;
			this.BeaverWorkplaceData = beaverWorkplaceData;
			this.BotWorkplaceData = botWorkplaceData;
			this.ContaminationData = contaminationData;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002398 File Offset: 0x00000598
		public void CopyFrom(PopulationData other)
		{
			this.NumberOfAdults = other.NumberOfAdults;
			this.NumberOfChildren = other.NumberOfChildren;
			this.NumberOfBots = other.NumberOfBots;
			this.BeaverWorkforceData = other.BeaverWorkforceData;
			this.BotWorkforceData = other.BotWorkforceData;
			this.BedData = other.BedData;
			this.BeaverWorkplaceData = other.BeaverWorkplaceData;
			this.BotWorkplaceData = other.BotWorkplaceData;
			this.ContaminationData = other.ContaminationData;
		}
	}
}
