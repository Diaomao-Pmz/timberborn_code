using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorkSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Demolishing
{
	// Token: 0x02000018 RID: 24
	public class Demolisher : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000A0 RID: 160 RVA: 0x000031C4 File Offset: 0x000013C4
		// (remove) Token: 0x060000A1 RID: 161 RVA: 0x000031FC File Offset: 0x000013FC
		public event EventHandler<Demolishable> ReservedDemolishableChanged;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003231 File Offset: 0x00001431
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003239 File Offset: 0x00001439
		public ReservedDemolishable ReservedDemolishable { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003242 File Offset: 0x00001442
		public Demolisher(EntityService entityService, ReservedDemolishableSerializer reservedDemolishableSerializer)
		{
			this._entityService = entityService;
			this._reservedDemolishableSerializer = reservedDemolishableSerializer;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003258 File Offset: 0x00001458
		public Demolishable Demolishable
		{
			get
			{
				return this.ReservedDemolishable.Demolishable;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003265 File Offset: 0x00001465
		public bool HasReservedDemolishable
		{
			get
			{
				return this.ReservedDemolishable != null;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003270 File Offset: 0x00001470
		public void Awake()
		{
			base.GetComponent<Worker>().GotUnemployed += delegate(object _, EventArgs _)
			{
				this.Unreserve();
			};
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003289 File Offset: 0x00001489
		public void DeleteEntity()
		{
			this.Unreserve();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003291 File Offset: 0x00001491
		public bool IsReserved(Demolishable demolishable)
		{
			ReservedDemolishable reservedDemolishable = this.ReservedDemolishable;
			return demolishable == ((reservedDemolishable != null) ? reservedDemolishable.Demolishable : null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000032A8 File Offset: 0x000014A8
		public void Reserve(Demolishable demolishable)
		{
			this.ReserveForDemolition(new ReservedDemolishable(demolishable, false));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000032B7 File Offset: 0x000014B7
		public void ReserveWithForcedDemolition(Demolishable demolishable)
		{
			this.ReserveForDemolition(new ReservedDemolishable(demolishable, true));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000032C8 File Offset: 0x000014C8
		public void Unreserve()
		{
			if (this.HasReservedDemolishable)
			{
				this.ReservedDemolishable.Demolishable.Unmarked -= this.OnDemolishableUnmarked;
				this.ReservedDemolishable.Demolishable.Reservable.Unreserve();
			}
			this.ReservedDemolishable = null;
			EventHandler<Demolishable> reservedDemolishableChanged = this.ReservedDemolishableChanged;
			if (reservedDemolishableChanged == null)
			{
				return;
			}
			reservedDemolishableChanged(this, null);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003327 File Offset: 0x00001527
		public void Demolish()
		{
			if (this.ReservedDemolishable.CanBeDemolished)
			{
				this._entityService.Delete(this.ReservedDemolishable.Demolishable);
			}
			this.Unreserve();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003352 File Offset: 0x00001552
		public void Save(IEntitySaver entitySaver)
		{
			if (this.HasReservedDemolishable)
			{
				entitySaver.GetComponent(Demolisher.DemolisherKey).Set<ReservedDemolishable>(Demolisher.ReservedDemolishableKey, this.ReservedDemolishable, this._reservedDemolishableSerializer);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003380 File Offset: 0x00001580
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			ReservedDemolishable reservedDemolishable;
			if (entityLoader.TryGetComponent(Demolisher.DemolisherKey, out objectLoader) && objectLoader.GetObsoletable<ReservedDemolishable>(Demolisher.ReservedDemolishableKey, this._reservedDemolishableSerializer, out reservedDemolishable))
			{
				this.ReserveForDemolition(reservedDemolishable);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033B8 File Offset: 0x000015B8
		public void ReserveForDemolition(ReservedDemolishable reservedDemolishable)
		{
			this.Unreserve();
			reservedDemolishable.Demolishable.Reservable.Reserve();
			reservedDemolishable.Demolishable.Unmarked += this.OnDemolishableUnmarked;
			this.ReservedDemolishable = reservedDemolishable;
			EventHandler<Demolishable> reservedDemolishableChanged = this.ReservedDemolishableChanged;
			if (reservedDemolishableChanged == null)
			{
				return;
			}
			reservedDemolishableChanged(this, reservedDemolishable.Demolishable);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003289 File Offset: 0x00001489
		public void OnDemolishableUnmarked(object sender, EventArgs e)
		{
			this.Unreserve();
		}

		// Token: 0x04000032 RID: 50
		public static readonly ComponentKey DemolisherKey = new ComponentKey("Demolisher");

		// Token: 0x04000033 RID: 51
		public static readonly PropertyKey<ReservedDemolishable> ReservedDemolishableKey = new PropertyKey<ReservedDemolishable>("ReservedDemolishable");

		// Token: 0x04000036 RID: 54
		public readonly EntityService _entityService;

		// Token: 0x04000037 RID: 55
		public readonly ReservedDemolishableSerializer _reservedDemolishableSerializer;
	}
}
