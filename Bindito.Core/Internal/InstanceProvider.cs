using System;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A3 RID: 163
	public class InstanceProvider
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003D72 File Offset: 0x00001F72
		public bool Exported { get; }

		// Token: 0x06000196 RID: 406 RVA: 0x00003D7A File Offset: 0x00001F7A
		public InstanceProvider(Func<object> func, bool exported)
		{
			this._func = func;
			this.Exported = exported;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003D90 File Offset: 0x00001F90
		public object GetInstance()
		{
			return this._func();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003D9D File Offset: 0x00001F9D
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((InstanceProvider)obj)));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003DCC File Offset: 0x00001FCC
		public override int GetHashCode()
		{
			return ((this._func != null) ? this._func.GetHashCode() : 0) * 397 ^ this.Exported.GetHashCode();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003E04 File Offset: 0x00002004
		private bool Equals(InstanceProvider other)
		{
			return object.Equals(this._func, other._func) && this.Exported == other.Exported;
		}

		// Token: 0x04000098 RID: 152
		private readonly Func<object> _func;
	}
}
