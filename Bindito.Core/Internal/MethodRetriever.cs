using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x020000AD RID: 173
	public class MethodRetriever : IMethodRetriever
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00004400 File Offset: 0x00002600
		public IEnumerable<MethodInfo> GetInjectedMethods(Type type)
		{
			ReadOnlyCollection<MethodInfo> readOnlyCollection;
			if (!this._injectedMethods.TryGetValue(type, out readOnlyCollection))
			{
				readOnlyCollection = MethodRetriever.GetInjectedMethodsUncached(type).ToList<MethodInfo>().AsReadOnly();
				this._injectedMethods[type] = readOnlyCollection;
			}
			return readOnlyCollection;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000443C File Offset: 0x0000263C
		private static IEnumerable<MethodInfo> GetInjectedMethodsUncached(Type type)
		{
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(new Func<MethodInfo, bool>(MethodRetriever.IsInjectedMethod));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004457 File Offset: 0x00002657
		private static bool IsInjectedMethod(MethodInfo method)
		{
			return MethodRetriever.HasInjectAttribute(method) && MethodRetriever.ReturnsVoid(method);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004469 File Offset: 0x00002669
		private static bool HasInjectAttribute(MethodInfo method)
		{
			return method.IsDefined(MethodRetriever.InjectAttributeType, false);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004477 File Offset: 0x00002677
		private static bool ReturnsVoid(MethodInfo method)
		{
			ParameterInfo returnParameter = method.ReturnParameter;
			return ((returnParameter != null) ? returnParameter.ParameterType : null) == typeof(void);
		}

		// Token: 0x040000AC RID: 172
		private static readonly Type InjectAttributeType = typeof(InjectAttribute);

		// Token: 0x040000AD RID: 173
		private readonly Dictionary<Type, ReadOnlyCollection<MethodInfo>> _injectedMethods = new Dictionary<Type, ReadOnlyCollection<MethodInfo>>();
	}
}
