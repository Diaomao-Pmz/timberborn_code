using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DecalSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.TailDecalSystem
{
	// Token: 0x02000007 RID: 7
	public class EnterableTailDecalApplier : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public EnterableTailDecalApplier(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public void Awake()
		{
			this._decalSupplier = base.GetComponent<DecalSupplier>();
			this._decalSupplier.ActiveDecalChanged += this.OnActiveDecalChanged;
			this._enterable = base.GetComponent<Enterable>();
			this._enterable.EntererAdded += delegate(object _, EntererAddedEventArgs e)
			{
				this.UpdateEntererDecal(e.Enterer);
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002163 File Offset: 0x00000363
		public void InitializeEntity()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public void OnActiveDecalChanged(object sender, EventArgs e)
		{
			foreach (Enterer enterer in this._enterable.EnterersInside)
			{
				this.UpdateEntererDecal(enterer);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C8 File Offset: 0x000003C8
		public void UpdateEntererDecal(Enterer enterer)
		{
			enterer.GetComponent<TailDecalApplier>().ApplyDecal(this._decalSupplier.ActiveDecal);
			this._eventBus.Post(new TailDecalAppliedEvent());
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public DecalSupplier _decalSupplier;

		// Token: 0x0400000A RID: 10
		public Enterable _enterable;
	}
}
