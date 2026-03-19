using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.Debugging
{
	// Token: 0x0200000D RID: 13
	public class DevModuleDefinition
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023C0 File Offset: 0x000005C0
		public ImmutableArray<DevMethod> Methods { get; }

		// Token: 0x06000027 RID: 39 RVA: 0x000023C8 File Offset: 0x000005C8
		public DevModuleDefinition(IEnumerable<DevMethod> methods)
		{
			this.Methods = methods.ToImmutableArray<DevMethod>();
		}

		// Token: 0x0200000E RID: 14
		public class Builder
		{
			// Token: 0x06000028 RID: 40 RVA: 0x000023DC File Offset: 0x000005DC
			public DevModuleDefinition.Builder AddMethod(DevMethod devMethod)
			{
				this._methods.Add(devMethod);
				return this;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000023EB File Offset: 0x000005EB
			public DevModuleDefinition Build()
			{
				return new DevModuleDefinition(this._methods);
			}

			// Token: 0x04000017 RID: 23
			public readonly List<DevMethod> _methods = new List<DevMethod>();
		}
	}
}
