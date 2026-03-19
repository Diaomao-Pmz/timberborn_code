using System;
using System.Collections.Generic;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x02000085 RID: 133
	public class BoundInstanceService : IBoundInstanceService
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000340E File Offset: 0x0000160E
		public BoundInstanceService(IBinder ownBinder)
		{
			this._ownBinder = ownBinder;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000341D File Offset: 0x0000161D
		public IEnumerable<object> GetBoundInstances()
		{
			return (from instance in this.BoundInstances().Concat(this.MultiBoundsInstances())
			where instance != null
			select instance).Distinct<object>();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003459 File Offset: 0x00001659
		private IEnumerable<object> BoundInstances()
		{
			return from binding in this._ownBinder.Bindings.Values
			select binding.ProvisionBinding.Instance;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003490 File Offset: 0x00001690
		private IEnumerable<object> MultiBoundsInstances()
		{
			return from binding in this._ownBinder.MultiBindings.Values.SelectMany((IReadOnlyList<Binding> bindings) => bindings)
			select binding.ProvisionBinding.Instance;
		}

		// Token: 0x04000083 RID: 131
		private readonly IBinder _ownBinder;
	}
}
