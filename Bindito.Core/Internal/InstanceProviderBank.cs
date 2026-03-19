using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x020000A4 RID: 164
	public class InstanceProviderBank : IInstanceProviderBank
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003E29 File Offset: 0x00002029
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00003E31 File Offset: 0x00002031
		public IInstanceProviderFactory InstanceProviderFactory { private get; set; }

		// Token: 0x0600019D RID: 413 RVA: 0x00003E3A File Offset: 0x0000203A
		public InstanceProviderBank(IBinder binder, IInstanceProviderBank parent)
		{
			this._binder = binder;
			this._parent = parent;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003E66 File Offset: 0x00002066
		public bool TryGetInstanceProvider(Type type, out InstanceProvider instanceProvider)
		{
			return this._singleInstanceProviders.TryGetValue(type, out instanceProvider) || this.TryGetInstanceProviderFromParent(type, out instanceProvider) || this.TryInitializeInstanceProvider(type, out instanceProvider);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003E8B File Offset: 0x0000208B
		public bool TryGetExportedInstanceProvider(Type type, out InstanceProvider instanceProvider)
		{
			if (this.TryGetInstanceProvider(type, out instanceProvider) && instanceProvider.Exported)
			{
				return true;
			}
			instanceProvider = null;
			return false;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003EA8 File Offset: 0x000020A8
		public IEnumerable<InstanceProvider> GetInstanceProviders(Type type)
		{
			List<InstanceProvider> list;
			IEnumerable<InstanceProvider> first;
			if (!this._multiInstanceProviders.TryGetValue(type, out list))
			{
				first = this.InitializeMultiInstanceProviders(type);
			}
			else
			{
				IEnumerable<InstanceProvider> enumerable = list;
				first = enumerable;
			}
			return first.Concat(this.GetInstanceProvidersFromParent(type));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00003EDD File Offset: 0x000020DD
		public IEnumerable<InstanceProvider> GetExportedInstanceProviders(Type type)
		{
			return from instanceProvider in this.GetInstanceProviders(type)
			where instanceProvider.Exported
			select instanceProvider;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003F0A File Offset: 0x0000210A
		private bool TryGetInstanceProviderFromParent(Type type, out InstanceProvider instanceProvider)
		{
			if (this._parent != null && this._parent.TryGetExportedInstanceProvider(type, out instanceProvider))
			{
				return true;
			}
			instanceProvider = null;
			return false;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00003F29 File Offset: 0x00002129
		private IEnumerable<InstanceProvider> GetInstanceProvidersFromParent(Type type)
		{
			if (this._parent == null)
			{
				return Enumerable.Empty<InstanceProvider>();
			}
			return this._parent.GetExportedInstanceProviders(type);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00003F48 File Offset: 0x00002148
		private bool TryInitializeInstanceProvider(Type type, out InstanceProvider instanceProvider)
		{
			Binding binding;
			if (this._binder.TryGetBinding(type, out binding))
			{
				instanceProvider = this.InstanceProviderFactory.CreateInstanceProvider(binding);
				this._singleInstanceProviders[type] = instanceProvider;
				return true;
			}
			instanceProvider = null;
			return false;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00003F88 File Offset: 0x00002188
		private IEnumerable<InstanceProvider> InitializeMultiInstanceProviders(Type type)
		{
			List<InstanceProvider> list = (from binding in this._binder.GetMultiBindings(type)
			select this.InstanceProviderFactory.CreateInstanceProvider(binding)).ToList<InstanceProvider>();
			this._multiInstanceProviders[type] = list;
			return list;
		}

		// Token: 0x0400009B RID: 155
		private readonly IBinder _binder;

		// Token: 0x0400009C RID: 156
		private readonly IInstanceProviderBank _parent;

		// Token: 0x0400009D RID: 157
		private readonly Dictionary<Type, InstanceProvider> _singleInstanceProviders = new Dictionary<Type, InstanceProvider>();

		// Token: 0x0400009E RID: 158
		private readonly Dictionary<Type, List<InstanceProvider>> _multiInstanceProviders = new Dictionary<Type, List<InstanceProvider>>();
	}
}
