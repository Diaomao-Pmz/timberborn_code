using System;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A2 RID: 162
	public class InstanceCreator : IInstanceCreator
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00003CFF File Offset: 0x00001EFF
		public InstanceCreator(IParameterProvider parameterProvider, IConstructorRetriever constructorRetriever)
		{
			this._parameterProvider = parameterProvider;
			this._constructorRetriever = constructorRetriever;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003D15 File Offset: 0x00001F15
		public object CreateInstance(Type type)
		{
			return this.CreateUsingEligibleConstructor(type);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003D20 File Offset: 0x00001F20
		private object CreateUsingEligibleConstructor(Type type)
		{
			ConstructorInfo eligibleConstructor = this._constructorRetriever.GetEligibleConstructor(type);
			if (eligibleConstructor == null)
			{
				throw new BinditoException("No eligible constructor found for type " + TypeFormatting.Format(type) + ".");
			}
			object[] parameters = this._parameterProvider.GetParameters(eligibleConstructor);
			return eligibleConstructor.Invoke(parameters);
		}

		// Token: 0x04000096 RID: 150
		private readonly IConstructorRetriever _constructorRetriever;

		// Token: 0x04000097 RID: 151
		private readonly IParameterProvider _parameterProvider;
	}
}
