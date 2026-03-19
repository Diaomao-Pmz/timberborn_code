using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x0200007D RID: 125
	public class BinderValidator
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public BinderValidator(IBindingValidator bindingValidator, IBinder binder)
		{
			this._bindingValidator = bindingValidator;
			this._binder = binder;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002BBB File Offset: 0x00000DBB
		public void Validate()
		{
			this.ValidateBindings();
			this.ValidateMultiBindings();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00002BCC File Offset: 0x00000DCC
		private void ValidateBindings()
		{
			foreach (KeyValuePair<Type, Binding> keyValuePair in this._binder.Bindings)
			{
				this._bindingValidator.Validate(keyValuePair.Key, keyValuePair.Value.ProvisionBinding);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002C38 File Offset: 0x00000E38
		private void ValidateMultiBindings()
		{
			foreach (KeyValuePair<Type, IReadOnlyList<Binding>> keyValuePair in this._binder.MultiBindings)
			{
				foreach (Binding binding in keyValuePair.Value)
				{
					this._bindingValidator.Validate(keyValuePair.Key, binding.ProvisionBinding);
				}
			}
		}

		// Token: 0x0400006D RID: 109
		private readonly IBindingValidator _bindingValidator;

		// Token: 0x0400006E RID: 110
		private readonly IBinder _binder;
	}
}
