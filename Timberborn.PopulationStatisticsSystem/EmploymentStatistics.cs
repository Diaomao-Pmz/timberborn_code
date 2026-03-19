using System;

namespace Timberborn.PopulationStatisticsSystem
{
	// Token: 0x02000006 RID: 6
	public readonly struct EmploymentStatistics
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002157 File Offset: 0x00000357
		public int EmployedWorkers { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000215F File Offset: 0x0000035F
		public int Vacancies { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002167 File Offset: 0x00000367
		public string WorkerType { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x0000216F File Offset: 0x0000036F
		public EmploymentStatistics(int employedWorkers, int vacancies, string workerType)
		{
			this.EmployedWorkers = employedWorkers;
			this.Vacancies = vacancies;
			this.WorkerType = workerType;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
		public static EmploymentStatistics operator +(EmploymentStatistics left, EmploymentStatistics right)
		{
			if (left.WorkerType != right.WorkerType)
			{
				throw new Exception("Cannot add EmploymentStatistics with different WorkerType");
			}
			return new EmploymentStatistics(left.EmployedWorkers + right.EmployedWorkers, left.Vacancies + right.Vacancies, left.WorkerType);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E0 File Offset: 0x000003E0
		public static EmploymentStatistics operator -(EmploymentStatistics left, EmploymentStatistics right)
		{
			if (left.WorkerType != right.WorkerType)
			{
				throw new Exception("Cannot subtract EmploymentStatistics with different WorkerType");
			}
			return new EmploymentStatistics(left.EmployedWorkers - right.EmployedWorkers, left.Vacancies - right.Vacancies, left.WorkerType);
		}
	}
}
