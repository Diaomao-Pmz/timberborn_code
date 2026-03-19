using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.LevelVisibilitySystem;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x02000023 RID: 35
	public class UncoveredModelSwitcher : BaseComponent, IAwakableComponent, IInitializableEntity, IPostPlacementChangeListener, IDeletableEntity, IPrePreviewShownListener
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00004467 File Offset: 0x00002667
		public UncoveredModelSwitcher(ILevelVisibilityService levelVisibilityService)
		{
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000448C File Offset: 0x0000268C
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this.CollectModels();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000044A0 File Offset: 0x000026A0
		public void InitializeEntity()
		{
			this._levelVisibilityService.MaxVisibleLevelChanged += this.OnMaxVisibleLevelChanged;
			this.UpdateVisibility();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000044BF File Offset: 0x000026BF
		public void OnPostPlacementChanged()
		{
			this.UpdateVisibility();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000044C7 File Offset: 0x000026C7
		public void DeleteEntity()
		{
			this._levelVisibilityService.MaxVisibleLevelChanged -= this.OnMaxVisibleLevelChanged;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000044BF File Offset: 0x000026BF
		public void OnPrePreviewShown()
		{
			this.UpdateVisibility();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000044E0 File Offset: 0x000026E0
		public void CollectModels()
		{
			UncoveredModelSwitcherSpec component = base.GetComponent<UncoveredModelSwitcherSpec>();
			BuildingModel component2 = base.GetComponent<BuildingModel>();
			this.CollectModelsFromChildren(component2.UnfinishedModel, component);
			this.CollectModelsFromChildren(component2.FinishedModel, component);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000044BF File Offset: 0x000026BF
		public void OnMaxVisibleLevelChanged(object sender, int level)
		{
			this.UpdateVisibility();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004518 File Offset: 0x00002718
		public void UpdateVisibility()
		{
			int maxVisibleLevel = this._levelVisibilityService.MaxVisibleLevel;
			int z = this._blockObject.CoordinatesAtBaseZ.z;
			int num = z + this._blockObject.Blocks.Size.z - 1;
			bool flag = maxVisibleLevel >= z && maxVisibleLevel <= num;
			foreach (GameObject gameObject in this._fullModels)
			{
				gameObject.SetActive(!flag);
			}
			foreach (GameObject gameObject2 in this._uncoveredModels)
			{
				gameObject2.SetActive(flag);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000045FC File Offset: 0x000027FC
		public void CollectModelsFromChildren(GameObject model, UncoveredModelSwitcherSpec uncoverModelSwitcherSpec)
		{
			foreach (GameObject gameObject in model.GetAllChildren())
			{
				if (gameObject.name == uncoverModelSwitcherSpec.FullModelName)
				{
					this._fullModels.Add(gameObject.gameObject);
				}
				else if (gameObject.name == uncoverModelSwitcherSpec.UncoveredModelName)
				{
					this._uncoveredModels.Add(gameObject.gameObject);
				}
			}
		}

		// Token: 0x04000061 RID: 97
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000062 RID: 98
		public readonly List<GameObject> _fullModels = new List<GameObject>();

		// Token: 0x04000063 RID: 99
		public readonly List<GameObject> _uncoveredModels = new List<GameObject>();

		// Token: 0x04000064 RID: 100
		public BlockObject _blockObject;
	}
}
