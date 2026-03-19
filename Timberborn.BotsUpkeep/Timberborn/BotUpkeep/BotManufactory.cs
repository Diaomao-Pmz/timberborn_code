using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Bots;
using Timberborn.Buildings;
using Timberborn.CharactersGame;
using Timberborn.EnterableSystem;
using Timberborn.GameDistricts;
using Timberborn.SingletonSystem;
using Timberborn.Workshops;

namespace Timberborn.BotUpkeep
{
	// Token: 0x02000007 RID: 7
	public class BotManufactory : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BotManufactory(EventBus eventBus, BotFactory botFactory)
		{
			this._eventBus = eventBus;
			this._botFactory = botFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void Awake()
		{
			this._buildingAccessible = base.GetComponent<BuildingAccessible>();
			this._manufactory = base.GetComponent<Manufactory>();
			this._enterable = base.GetComponent<Enterable>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213A File Offset: 0x0000033A
		public void Start()
		{
			this._manufactory.ProductionFinished += this.OnProductionFinished;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		public void OnProductionFinished(object sender, EventArgs e)
		{
			Bot bot = this._botFactory.Create(this._buildingAccessible.CalculateAccessFromLocalAccess(), this._enterable.ExitWorldSpaceRotation);
			DistrictCenter district = base.GetComponent<DistrictBuilding>().District;
			if (district)
			{
				bot.GetComponent<Citizen>().AssignInitialDistrict(district);
			}
			bot.GetComponent<CharacterBirthNotifier>().EnableNotification();
			this._eventBus.Post(new BotManufacturedEvent());
		}

		// Token: 0x04000008 RID: 8
		public readonly EventBus _eventBus;

		// Token: 0x04000009 RID: 9
		public readonly BotFactory _botFactory;

		// Token: 0x0400000A RID: 10
		public Manufactory _manufactory;

		// Token: 0x0400000B RID: 11
		public BuildingAccessible _buildingAccessible;

		// Token: 0x0400000C RID: 12
		public Enterable _enterable;
	}
}
