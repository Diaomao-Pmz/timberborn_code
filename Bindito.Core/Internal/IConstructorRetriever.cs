using System;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x02000094 RID: 148
	public interface IConstructorRetriever
	{
		// Token: 0x06000174 RID: 372
		ConstructorInfo GetEligibleConstructor(Type type);
	}
}
