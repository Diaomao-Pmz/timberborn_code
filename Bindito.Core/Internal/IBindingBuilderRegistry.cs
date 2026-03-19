using System;

namespace Bindito.Core.Internal
{
	// Token: 0x02000090 RID: 144
	public interface IBindingBuilderRegistry
	{
		// Token: 0x0600016F RID: 367
		void RegisterBindingBuilder<T>(BindingBuilder<T> bindingBuilder) where T : class;

		// Token: 0x06000170 RID: 368
		void RegisterMultiBindingBuilder<T>(BindingBuilder<T> bindingBuilder) where T : class;
	}
}
