using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x02000080 RID: 128
	public class BindingAnalysis
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002F36 File Offset: 0x00001136
		public bool HasCyclicDependency { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00002F3E File Offset: 0x0000113E
		public bool HasMissingDependency { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002F46 File Offset: 0x00001146
		public IReadOnlyList<Type> DependencyChain { get; }

		// Token: 0x06000122 RID: 290 RVA: 0x00002F4E File Offset: 0x0000114E
		private BindingAnalysis(bool hasCyclicDependency, bool hasMissingDependency, IEnumerable<Type> dependencyChain)
		{
			this.HasCyclicDependency = hasCyclicDependency;
			this.HasMissingDependency = hasMissingDependency;
			this.DependencyChain = dependencyChain.ToList<Type>().AsReadOnly();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00002F75 File Offset: 0x00001175
		public static BindingAnalysis Ok()
		{
			return new BindingAnalysis(false, false, Enumerable.Empty<Type>());
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00002F83 File Offset: 0x00001183
		public static BindingAnalysis CyclicDependency(IEnumerable<Type> dependencyChain)
		{
			return new BindingAnalysis(true, false, dependencyChain);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00002F8D File Offset: 0x0000118D
		public static BindingAnalysis MissingDependency(IEnumerable<Type> dependencyChain)
		{
			return new BindingAnalysis(false, true, dependencyChain);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00002F97 File Offset: 0x00001197
		public bool IsOk
		{
			get
			{
				return !this.HasCyclicDependency && !this.HasMissingDependency;
			}
		}
	}
}
