using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Beavers;
using Timberborn.Common;
using Timberborn.DwellingSystem;
using Timberborn.EnterableSystem;
using Timberborn.NeedSystem;

namespace Timberborn.Reproduction
{
	// Token: 0x02000011 RID: 17
	public class ProcreationHouse : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002E65 File Offset: 0x00001065
		public ProcreationHouse(IRandomNumberGenerator randomNumberGenerator, NewbornSpawner newbornSpawner)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._newbornSpawner = newbornSpawner;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E7B File Offset: 0x0000107B
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._dwelling = base.GetComponent<Dwelling>();
			this._enterable.EntererAdded += delegate(object _, EntererAddedEventArgs e)
			{
				this.ProcreateAmongDwellers(e.Enterer);
			};
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EAC File Offset: 0x000010AC
		public void ProcreateAmongDwellers(Enterer enterer)
		{
			if (this.HasFreeChildSlot() && ProcreationHouse.IsAdult(enterer) && ProcreationHouse.CanProcreate(enterer) && this.HasDwellerToProcreateWith(enterer) && this.CheckProcreationChance())
			{
				this._newbornSpawner.SpawnChild(this._dwelling);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EE8 File Offset: 0x000010E8
		public bool HasFreeChildSlot()
		{
			return this._dwelling.HasFreeChildSlots && this._dwelling.UnderpopulatedByChildren;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F04 File Offset: 0x00001104
		public static bool IsAdult(Enterer enterer)
		{
			return enterer.HasComponent<AdultSpec>();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F0C File Offset: 0x0000110C
		public static bool CanProcreate(BaseComponent entity)
		{
			return !entity.GetComponent<NeedManager>().AnyNeedIsInCriticalState();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F1C File Offset: 0x0000111C
		public bool HasDwellerToProcreateWith(Enterer enterer)
		{
			foreach (Dweller dweller in this._dwelling.AdultDwellers)
			{
				if (dweller.GameObject != enterer.GameObject && ProcreationHouse.CanProcreate(dweller))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F8C File Offset: 0x0000118C
		public bool CheckProcreationChance()
		{
			return this._randomNumberGenerator.CheckProbability(ProcreationHouse.DailySpawningChance);
		}

		// Token: 0x0400002B RID: 43
		public static readonly float DailySpawningChance = 0.1875f;

		// Token: 0x0400002C RID: 44
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x0400002D RID: 45
		public readonly NewbornSpawner _newbornSpawner;

		// Token: 0x0400002E RID: 46
		public Enterable _enterable;

		// Token: 0x0400002F RID: 47
		public Dwelling _dwelling;
	}
}
