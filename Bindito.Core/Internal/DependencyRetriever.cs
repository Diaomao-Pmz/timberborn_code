using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x0200008B RID: 139
	public class DependencyRetriever : IDependencyRetriever
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00003A6D File Offset: 0x00001C6D
		public DependencyRetriever(IConstructorRetriever constructorRetriever, IMethodRetriever methodRetriever)
		{
			this._constructorRetriever = constructorRetriever;
			this._methodRetriever = methodRetriever;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003A84 File Offset: 0x00001C84
		public IEnumerable<Type> GetDependencies(ProvisionBinding provisionBinding)
		{
			Type type = provisionBinding.Type;
			if (type != null)
			{
				return this.GetParametersOfEligibleConstructor(type).Concat(this.GetParametersOfInjectedMethods(type));
			}
			object instance = provisionBinding.Instance;
			if (instance != null)
			{
				return this.GetParametersOfInjectedMethods(instance.GetType());
			}
			Type providerType = provisionBinding.ProviderType;
			if (providerType != null)
			{
				return this.GetParametersOfEligibleConstructor(providerType).Concat(this.GetParametersOfInjectedMethods(providerType));
			}
			Type existingType = provisionBinding.ExistingType;
			if (existingType != null)
			{
				return Enumerable.Repeat<Type>(existingType, 1);
			}
			return Enumerable.Empty<Type>();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003B0D File Offset: 0x00001D0D
		private IEnumerable<Type> GetParametersOfInjectedMethods(Type type)
		{
			return this._methodRetriever.GetInjectedMethods(type).SelectMany(new Func<MethodInfo, IEnumerable<Type>>(DependencyRetriever.GetParameterTypes));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003B2C File Offset: 0x00001D2C
		private IEnumerable<Type> GetParametersOfEligibleConstructor(Type type)
		{
			ConstructorInfo eligibleConstructor = this._constructorRetriever.GetEligibleConstructor(type);
			if (!(eligibleConstructor != null))
			{
				return Enumerable.Empty<Type>();
			}
			return DependencyRetriever.GetParameterTypes(eligibleConstructor);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003B5B File Offset: 0x00001D5B
		private static IEnumerable<Type> GetParameterTypes(MethodBase methodBase)
		{
			return from parameter in methodBase.GetParameters()
			select parameter.ParameterType;
		}

		// Token: 0x04000092 RID: 146
		private readonly IConstructorRetriever _constructorRetriever;

		// Token: 0x04000093 RID: 147
		private readonly IMethodRetriever _methodRetriever;
	}
}
