using System;
using System.Linq;
using System.Reflection;

namespace Bindito.Core.Internal
{
	// Token: 0x020000AF RID: 175
	public class ParameterProvider : IParameterProvider
	{
		// Token: 0x060001CC RID: 460 RVA: 0x000044F6 File Offset: 0x000026F6
		public ParameterProvider(IInstanceBank instanceBank, IMultiBindingService multiBindingService)
		{
			this._instanceBank = instanceBank;
			this._multiBindingService = multiBindingService;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000450C File Offset: 0x0000270C
		public object[] GetParameters(MethodBase method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			object[] array = new object[parameters.Length];
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object obj;
				if (!this.TryGetParameter(parameterInfo.ParameterType, out obj))
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Can't get parameter ",
						TypeFormatting.Format(parameterInfo.ParameterType),
						" of method ",
						TypeFormatting.Format(method.DeclaringType),
						".",
						method.Name,
						"."
					}));
				}
				array[num++] = obj;
			}
			return array;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000045B8 File Offset: 0x000027B8
		private bool TryGetParameter(Type parameterType, out object parameter)
		{
			Type type;
			if (this._multiBindingService.IsMultiBound(parameterType, out type))
			{
				parameter = this.ReturnMultiBoundParameter(type);
				return true;
			}
			return this._instanceBank.TryGetInstance(parameterType, out parameter);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000045F0 File Offset: 0x000027F0
		private object ReturnMultiBoundParameter(Type type)
		{
			object[] array = this._instanceBank.GetInstances(type).ToArray<object>();
			Array array2 = Array.CreateInstance(type, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				array2.SetValue(array[i], i);
			}
			return array2;
		}

		// Token: 0x040000AF RID: 175
		private readonly IInstanceBank _instanceBank;

		// Token: 0x040000B0 RID: 176
		private readonly IMultiBindingService _multiBindingService;
	}
}
