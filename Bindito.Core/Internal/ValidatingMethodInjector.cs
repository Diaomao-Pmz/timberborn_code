using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x020000B5 RID: 181
	public class ValidatingMethodInjector : IValidatingMethodInjector
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00004A29 File Offset: 0x00002C29
		public ValidatingMethodInjector(IMethodInjector methodInjector, IBindingValidator bindingValidator)
		{
			this._methodInjector = methodInjector;
			this._bindingValidator = bindingValidator;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004A4A File Offset: 0x00002C4A
		public void Inject(object instance)
		{
			this.Validate(instance);
			this._methodInjector.Inject(instance);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004A60 File Offset: 0x00002C60
		private void Validate(object instance)
		{
			Type type = instance.GetType();
			if (!this._validatedTypesOfInstances.Contains(type))
			{
				ProvisionBinding provisionBinding = ProvisionBinding.CreateToInstance(instance);
				this._bindingValidator.Validate(type, provisionBinding);
				this._validatedTypesOfInstances.Add(type);
			}
		}

		// Token: 0x040000BB RID: 187
		private readonly IMethodInjector _methodInjector;

		// Token: 0x040000BC RID: 188
		private readonly IBindingValidator _bindingValidator;

		// Token: 0x040000BD RID: 189
		private readonly HashSet<Type> _validatedTypesOfInstances = new HashSet<Type>();
	}
}
