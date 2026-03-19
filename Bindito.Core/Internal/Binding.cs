using System;

namespace Bindito.Core.Internal
{
	// Token: 0x0200007E RID: 126
	public class Binding
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public ProvisionBinding ProvisionBinding { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00002CDC File Offset: 0x00000EDC
		public Scope Scope { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public bool Exported { get; }

		// Token: 0x06000118 RID: 280 RVA: 0x00002CEC File Offset: 0x00000EEC
		public Binding(ProvisionBinding provisionBinding, Scope scope, bool exported)
		{
			this.Scope = scope;
			this.ProvisionBinding = provisionBinding;
			this.Exported = exported;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002D09 File Offset: 0x00000F09
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", this.ProvisionBinding, this.Scope, this.Exported ? "Exported" : "Unexported");
		}
	}
}
