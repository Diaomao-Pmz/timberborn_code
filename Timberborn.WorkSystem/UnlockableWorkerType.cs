using System;

namespace Timberborn.WorkSystem
{
	// Token: 0x02000011 RID: 17
	public readonly struct UnlockableWorkerType : IEquatable<UnlockableWorkerType>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000029C5 File Offset: 0x00000BC5
		public string WorkplaceTemplateName { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000029CD File Offset: 0x00000BCD
		public string WorkerType { get; }

		// Token: 0x06000049 RID: 73 RVA: 0x000029D5 File Offset: 0x00000BD5
		public UnlockableWorkerType(string workplaceTemplateName, string workerType)
		{
			this.WorkplaceTemplateName = workplaceTemplateName;
			this.WorkerType = workerType;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029E5 File Offset: 0x00000BE5
		public bool Equals(UnlockableWorkerType other)
		{
			return this.WorkplaceTemplateName == other.WorkplaceTemplateName && this.WorkerType == other.WorkerType;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A10 File Offset: 0x00000C10
		public override bool Equals(object obj)
		{
			if (obj is UnlockableWorkerType)
			{
				UnlockableWorkerType other = (UnlockableWorkerType)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A35 File Offset: 0x00000C35
		public override int GetHashCode()
		{
			return HashCode.Combine<string, string>(this.WorkplaceTemplateName, this.WorkerType);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A48 File Offset: 0x00000C48
		public static bool operator ==(UnlockableWorkerType left, UnlockableWorkerType right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A52 File Offset: 0x00000C52
		public static bool operator !=(UnlockableWorkerType left, UnlockableWorkerType right)
		{
			return !left.Equals(right);
		}
	}
}
