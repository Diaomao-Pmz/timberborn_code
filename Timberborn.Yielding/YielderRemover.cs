using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Carrying;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.ReservableSystem;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Yielding
{
	// Token: 0x02000014 RID: 20
	public class YielderRemover : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity, IPostInitializableEntity
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000079 RID: 121 RVA: 0x00003238 File Offset: 0x00001438
		// (remove) Token: 0x0600007A RID: 122 RVA: 0x00003270 File Offset: 0x00001470
		public event EventHandler<YieldReservationCompletedEventArgs> YieldReservationCompleted;

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000032A5 File Offset: 0x000014A5
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000032AD File Offset: 0x000014AD
		public Yielder ReservedYielder { get; private set; }

		// Token: 0x0600007D RID: 125 RVA: 0x000032B6 File Offset: 0x000014B6
		public YielderRemover(GoodAmountSerializer goodAmountSerializer, ReferenceSerializer referenceSerializer)
		{
			this._goodAmountSerializer = goodAmountSerializer;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000032CC File Offset: 0x000014CC
		public bool HasReservedYielder
		{
			get
			{
				return this.ReservedYielder;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032D9 File Offset: 0x000014D9
		public void Awake()
		{
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			base.GetComponent<Worker>().GotUnemployed += delegate(object _, EventArgs _)
			{
				this.Unreserve();
			};
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000032FE File Offset: 0x000014FE
		public void PostInitializeEntity()
		{
			this.ResolveLoadedReservation();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003306 File Offset: 0x00001506
		public void DeleteEntity()
		{
			this.Unreserve();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000330E File Offset: 0x0000150E
		public void ReserveForRemoval(Yielder yielder, GoodAmount yield)
		{
			this.Unreserve();
			yielder.GetComponent<Reservable>().Reserve();
			this.ReservedYielder = yielder;
			this._reservedYield = yield;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000332F File Offset: 0x0000152F
		public void Unreserve()
		{
			if (this.HasReservedYielder)
			{
				this.ReservedYielder.GetComponent<Reservable>().Unreserve();
			}
			this.ReservedYielder = null;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003350 File Offset: 0x00001550
		public void CompleteReservation()
		{
			if (this._reservedYield.Amount > 0)
			{
				this._goodCarrier.PutGoodsInHands(this._reservedYield, true);
			}
			this.ReservedYielder.DecreaseYield(this._reservedYield);
			this.Unreserve();
			EventHandler<YieldReservationCompletedEventArgs> yieldReservationCompleted = this.YieldReservationCompleted;
			if (yieldReservationCompleted == null)
			{
				return;
			}
			yieldReservationCompleted(this, new YieldReservationCompletedEventArgs(this._reservedYield));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033B0 File Offset: 0x000015B0
		public void Save(IEntitySaver entitySaver)
		{
			if (this.HasReservedYielder)
			{
				IObjectSaver component = entitySaver.GetComponent(YielderRemover.YielderRemoverKey);
				component.Set<Yielder>(YielderRemover.ReservedYielderKey, this.ReservedYielder, this._referenceSerializer.Of<Yielder>());
				component.Set<GoodAmount>(YielderRemover.ReservedYieldKey, this._reservedYield, this._goodAmountSerializer);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003404 File Offset: 0x00001604
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			GoodAmount yield;
			Yielder yielder;
			if (entityLoader.TryGetComponent(YielderRemover.YielderRemoverKey, out objectLoader) && objectLoader.GetObsoletable<GoodAmount>(YielderRemover.ReservedYieldKey, this._goodAmountSerializer, out yield) && objectLoader.GetObsoletable<Yielder>(YielderRemover.ReservedYielderKey, this._referenceSerializer.Of<Yielder>(), out yielder))
			{
				this.ReserveForRemoval(yielder, yield);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003458 File Offset: 0x00001658
		public void ResolveLoadedReservation()
		{
			if (this.ReservedYielder)
			{
				GoodAmount yield = this.ReservedYielder.Yield;
				int amount = yield.Amount;
				if (this._reservedYield.Amount > amount)
				{
					Debug.LogWarning(string.Format("Reducing {0}'s reservation of {1} ", base.Name, this.ReservedYielder) + string.Format("from {0} to {1}", this._reservedYield.Amount, amount));
					GoodAmount reservedYield = new GoodAmount(yield.GoodId, amount);
					this._reservedYield = reservedYield;
				}
				this.ReserveForRemoval(this.ReservedYielder, this._reservedYield);
			}
		}

		// Token: 0x04000036 RID: 54
		public static readonly ComponentKey YielderRemoverKey = new ComponentKey("YielderRemover");

		// Token: 0x04000037 RID: 55
		public static readonly PropertyKey<Yielder> ReservedYielderKey = new PropertyKey<Yielder>("ReservedYielder");

		// Token: 0x04000038 RID: 56
		public static readonly PropertyKey<GoodAmount> ReservedYieldKey = new PropertyKey<GoodAmount>("ReservedYield");

		// Token: 0x0400003B RID: 59
		public readonly GoodAmountSerializer _goodAmountSerializer;

		// Token: 0x0400003C RID: 60
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400003D RID: 61
		public GoodAmount _reservedYield;

		// Token: 0x0400003E RID: 62
		public GoodCarrier _goodCarrier;
	}
}
