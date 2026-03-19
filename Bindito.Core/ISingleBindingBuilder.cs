using System;

namespace Bindito.Core
{
	// Token: 0x0200007B RID: 123
	public interface ISingleBindingBuilder<TBound> : IBindingBuilder<TBound>, IScopeAssignee where TBound : class
	{
	}
}
