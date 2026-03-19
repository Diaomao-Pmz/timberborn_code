using System;

namespace Bindito.Core.Internal
{
	// Token: 0x020000B3 RID: 179
	public class Scoper : IScoper
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000048F7 File Offset: 0x00002AF7
		public Func<object> PlaceInScope(Func<object> provider, Scope scope)
		{
			if (!Scoper.IsSingleton(scope))
			{
				return provider;
			}
			return Scoper.WrapInInstanceCacher(provider);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004909 File Offset: 0x00002B09
		private static bool IsSingleton(Scope scope)
		{
			return scope == Scope.Singleton;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000490F File Offset: 0x00002B0F
		private static Func<object> WrapInInstanceCacher(Func<object> provider)
		{
			object instance = provider();
			return () => instance;
		}
	}
}
