using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BonusSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.GameDistricts;
using Timberborn.Goods;
using Timberborn.Persistence;
using Timberborn.ResourceCountingSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.Carrying
{
	// Token: 0x0200000F RID: 15
	public class GoodCarrier : BaseComponent, IAwakableComponent, IPersistentEntity, IMovementSpeedAffector, IGoodCarrier
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003D RID: 61 RVA: 0x00002B4C File Offset: 0x00000D4C
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002B84 File Offset: 0x00000D84
		public event EventHandler<CarriedGoodsChangedEventArgs> CarriedGoodsChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002BB9 File Offset: 0x00000DB9
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public GoodAmount CarriedGoods { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002BCA File Offset: 0x00000DCA
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public bool CountGoodsAsAvailable { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002BDB File Offset: 0x00000DDB
		public GoodCarrier(GoodAmountSerializer goodAmountSerializer)
		{
			this._goodAmountSerializer = goodAmountSerializer;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002BEC File Offset: 0x00000DEC
		public int LiftingCapacity
		{
			get
			{
				double num = Math.Round((double)this._bonusManager.Multiplier(GoodCarrier.CarryingCapacityBonusId), 2);
				return (int)((double)this._goodCarrierSpec.BaseLiftingCapacity * num);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C20 File Offset: 0x00000E20
		public bool IsCarrying
		{
			get
			{
				return this.CarriedGoods.Amount > 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C3E File Offset: 0x00000E3E
		public bool IsMovementSlowed
		{
			get
			{
				return this.IsCarrying;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C48 File Offset: 0x00000E48
		public void Awake()
		{
			base.GetComponent<Character>().Died += this.OnDied;
			this._bonusManager = base.GetComponent<BonusManager>();
			this._goodCarrierSpec = base.GetComponent<GoodCarrierSpec>();
			this._citizen = base.GetComponent<Citizen>();
			this._citizen.ChangedAssignedDistrict += this.OnChangedAssignedDistrict;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CA7 File Offset: 0x00000EA7
		public void PutGoodsInHands(GoodAmount goods, bool countAsAvailable)
		{
			if (!this.IsCarrying)
			{
				this.SetCarriedGoods(goods);
				this.CountGoodsAsAvailable = countAsAvailable;
				return;
			}
			throw new InvalidOperationException(string.Format("Tried to put {0} in {1}'s hands ", goods, base.Name) + "but they are already carrying something");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CE5 File Offset: 0x00000EE5
		public void EmptyHands()
		{
			this.SetCarriedGoods(new GoodAmount(null, 0));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsCarrying)
			{
				IObjectSaver component = entitySaver.GetComponent(GoodCarrier.GoodCarrierKey);
				component.Set<GoodAmount>(GoodCarrier.CarriedGoodsKey, this.CarriedGoods, this._goodAmountSerializer);
				component.Set(GoodCarrier.CountGoodsAsAvailableKey, this.CountGoodsAsAvailable);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D30 File Offset: 0x00000F30
		[BackwardCompatible(2026, 2, 23, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			GoodAmount goods;
			if (entityLoader.TryGetComponent(GoodCarrier.GoodCarrierKey, out objectLoader) && objectLoader.GetObsoletable<GoodAmount>(GoodCarrier.CarriedGoodsKey, this._goodAmountSerializer, out goods))
			{
				bool countAsAvailable = false;
				if (objectLoader.Has<bool>(GoodCarrier.CountGoodsAsAvailableKey))
				{
					countAsAvailable = objectLoader.Get(GoodCarrier.CountGoodsAsAvailableKey);
				}
				this.PutGoodsInHands(goods, countAsAvailable);
				return;
			}
			this.EmptyHands();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D8C File Offset: 0x00000F8C
		public void OnChangedAssignedDistrict(object sender, ChangeAssignedDistrictEventArgs e)
		{
			if (e.PreviousDistrict)
			{
				e.PreviousDistrict.GetComponent<DistrictResourceCounter>().Remove(this);
			}
			if (e.CurrentDistrict)
			{
				e.CurrentDistrict.GetComponent<DistrictResourceCounter>().Add(this);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public void OnDied(object sender, EventArgs e)
		{
			this.EmptyHands();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public void SetCarriedGoods(GoodAmount goods)
		{
			this.CarriedGoods = goods;
			EventHandler<CarriedGoodsChangedEventArgs> carriedGoodsChanged = this.CarriedGoodsChanged;
			if (carriedGoodsChanged == null)
			{
				return;
			}
			carriedGoodsChanged(this, new CarriedGoodsChangedEventArgs(this.CarriedGoods));
		}

		// Token: 0x0400001F RID: 31
		public static readonly string CarryingCapacityBonusId = "CarryingCapacity";

		// Token: 0x04000020 RID: 32
		public static readonly ComponentKey GoodCarrierKey = new ComponentKey("GoodCarrier");

		// Token: 0x04000021 RID: 33
		public static readonly PropertyKey<GoodAmount> CarriedGoodsKey = new PropertyKey<GoodAmount>("CarriedGoods");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<bool> CountGoodsAsAvailableKey = new PropertyKey<bool>("CountGoodAsAvailable");

		// Token: 0x04000026 RID: 38
		public readonly GoodAmountSerializer _goodAmountSerializer;

		// Token: 0x04000027 RID: 39
		public BonusManager _bonusManager;

		// Token: 0x04000028 RID: 40
		public GoodCarrierSpec _goodCarrierSpec;

		// Token: 0x04000029 RID: 41
		public Citizen _citizen;
	}
}
