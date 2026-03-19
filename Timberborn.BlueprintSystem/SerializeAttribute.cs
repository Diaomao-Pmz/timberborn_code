using System;
using JetBrains.Annotations;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Property)]
	[MeansImplicitUse]
	public class SerializeAttribute : Attribute
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004329 File Offset: 0x00002529
		public string SourceName { get; }

		// Token: 0x060000FE RID: 254 RVA: 0x00002050 File Offset: 0x00000250
		public SerializeAttribute()
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004331 File Offset: 0x00002531
		public SerializeAttribute(string sourceName)
		{
			this.SourceName = sourceName;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004340 File Offset: 0x00002540
		public bool HasSource
		{
			get
			{
				return this.SourceName != null;
			}
		}
	}
}
