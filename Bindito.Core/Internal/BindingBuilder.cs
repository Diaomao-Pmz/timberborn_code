using System;

namespace Bindito.Core.Internal
{
	// Token: 0x02000081 RID: 129
	public class BindingBuilder<TBound> : IBindingBuilder, ISingleBindingBuilder<TBound>, IBindingBuilder<TBound>, IScopeAssignee, IMultiBindingBuilder<TBound>, IExportAssignee where TBound : class
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00002FAC File Offset: 0x000011AC
		public IScopeAssignee To<TImplementation>() where TImplementation : class, TBound
		{
			this._provisionBinding = ProvisionBinding.CreateToType(typeof(TImplementation));
			return this;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00002FC4 File Offset: 0x000011C4
		public IScopeAssignee ToProvider<TProvider>() where TProvider : IProvider<TBound>
		{
			this._provisionBinding = ProvisionBinding.CreateToProviderType(typeof(TProvider));
			return this;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00002FDC File Offset: 0x000011DC
		public IScopeAssignee ToProvider(IProvider<TBound> provider)
		{
			this._provisionBinding = ProvisionBinding.CreateToProviderInstance(provider);
			return this;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002FEB File Offset: 0x000011EB
		public IScopeAssignee ToProvider(Func<TBound> provider)
		{
			this._provisionBinding = ProvisionBinding.CreateToProvidingMethod(provider);
			return this;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002FFA File Offset: 0x000011FA
		public IExportAssignee ToInstance(TBound instance)
		{
			this._provisionBinding = ProvisionBinding.CreateToInstance(instance);
			this._scope = new Scope?(Scope.Singleton);
			return this;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000301A File Offset: 0x0000121A
		public IExportAssignee ToExisting<TExisting>() where TExisting : TBound
		{
			this._provisionBinding = ProvisionBinding.CreateToExisting(typeof(TExisting));
			this._scope = new Scope?(Scope.Singleton);
			return this;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000303E File Offset: 0x0000123E
		public IExportAssignee AsSingleton()
		{
			this._scope = new Scope?(Scope.Singleton);
			return this;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000304D File Offset: 0x0000124D
		public IExportAssignee AsTransient()
		{
			this._scope = new Scope?(Scope.Transient);
			return this;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000305C File Offset: 0x0000125C
		public void AsExported()
		{
			this._exported = true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003068 File Offset: 0x00001268
		public Binding Build()
		{
			if (this._scope == null)
			{
				throw new BinditoException(TypeFormatting.Format(typeof(TBound)) + " binding has unspecified scope.");
			}
			return new Binding(this._provisionBinding, this._scope.Value, this._exported);
		}

		// Token: 0x04000079 RID: 121
		private ProvisionBinding _provisionBinding = ProvisionBinding.CreateToType(typeof(TBound));

		// Token: 0x0400007A RID: 122
		private Scope? _scope;

		// Token: 0x0400007B RID: 123
		private bool _exported;
	}
}
