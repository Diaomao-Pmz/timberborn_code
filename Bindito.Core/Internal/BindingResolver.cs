using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x02000083 RID: 131
	public class BindingResolver : IBindingResolver
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000032B4 File Offset: 0x000014B4
		public BindingResolver(IMultiBindingService multiBindingService, IBinder ownBinder, IBinder parentBinder = null)
		{
			this._multiBindingService = multiBindingService;
			this._ownBinder = ownBinder;
			this._parentBinder = parentBinder;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000032D4 File Offset: 0x000014D4
		public bool ResolveBindings(Type type, out IEnumerable<Binding> ownBindings)
		{
			Type type2;
			if (this._multiBindingService.IsMultiBound(type, out type2))
			{
				ownBindings = this._ownBinder.GetMultiBindings(type2);
				return true;
			}
			if (this.TryGetBinding(type, out ownBindings))
			{
				return true;
			}
			ownBindings = Enumerable.Empty<Binding>();
			return false;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003318 File Offset: 0x00001518
		private bool TryGetBinding(Type type, out IEnumerable<Binding> ownBindings)
		{
			Binding element;
			if (this._ownBinder.TryGetBinding(type, out element))
			{
				ownBindings = Enumerable.Repeat<Binding>(element, 1);
				return true;
			}
			Binding binding;
			if (this._parentBinder != null && this._parentBinder.TryGetExportedBinding(type, out binding))
			{
				ownBindings = Enumerable.Empty<Binding>();
				return true;
			}
			ownBindings = Enumerable.Empty<Binding>();
			return false;
		}

		// Token: 0x0400007F RID: 127
		private readonly IMultiBindingService _multiBindingService;

		// Token: 0x04000080 RID: 128
		private readonly IBinder _ownBinder;

		// Token: 0x04000081 RID: 129
		private readonly IBinder _parentBinder;
	}
}
