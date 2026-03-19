using System;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A8 RID: 168
	public interface IParameterProvider
	{
		// Token: 0x060001B9 RID: 441
		object[] GetParameters(MethodBase method);
	}
}
