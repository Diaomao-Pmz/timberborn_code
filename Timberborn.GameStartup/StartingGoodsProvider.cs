using System;
using System.Collections.Generic;
using Timberborn.Buildings;
using Timberborn.GameSceneLoading;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.NewGameConfigurationSystem;
using Timberborn.SceneLoading;
using Timberborn.SimpleOutputBuildings;

namespace Timberborn.GameStartup
{
	// Token: 0x02000016 RID: 22
	public class StartingGoodsProvider
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002B52 File Offset: 0x00000D52
		public StartingGoodsProvider(ISceneLoader sceneLoader)
		{
			this._sceneLoader = sceneLoader;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B61 File Offset: 0x00000D61
		public void AddStartingInventory(Building startingBuilding)
		{
			this.ModifyStartingInventory(startingBuilding, delegate(Inventory inventory, GoodAmount initialGood)
			{
				inventory.GiveIgnoringCapacity(initialGood);
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B89 File Offset: 0x00000D89
		public void RemoveStartingInventory(Building startingBuilding)
		{
			this.ModifyStartingInventory(startingBuilding, delegate(Inventory inventory, GoodAmount initialGood)
			{
				inventory.Take(initialGood);
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public void ModifyStartingInventory(Building startingBuilding, Action<Inventory, GoodAmount> modifyAction)
		{
			if (startingBuilding != null)
			{
				Inventory inventory = startingBuilding.GetComponent<SimpleOutputInventory>().Inventory;
				foreach (GoodAmount arg in this.InitialGoods())
				{
					modifyAction(inventory, arg);
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C14 File Offset: 0x00000E14
		public IEnumerable<GoodAmount> InitialGoods()
		{
			GameModeSpec gameMode = this._sceneLoader.GetSceneParameters<GameSceneParameters>().NewGameConfiguration.GameMode;
			yield return new GoodAmount("Berries", gameMode.StartingFood);
			yield return new GoodAmount("Water", gameMode.StartingWater);
			yield break;
		}

		// Token: 0x04000046 RID: 70
		public readonly ISceneLoader _sceneLoader;
	}
}
