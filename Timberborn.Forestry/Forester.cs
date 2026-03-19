using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.DuplicationSystem;
using Timberborn.Persistence;
using Timberborn.Planting;
using Timberborn.WorldPersistence;

namespace Timberborn.Forestry
{
	// Token: 0x02000008 RID: 8
	public class Forester : BaseComponent, IPersistentEntity, IDuplicable<Forester>, IDuplicable, IPlantingSpotValidator
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021C8 File Offset: 0x000003C8
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool ReplantDeadTrees { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x000021D9 File Offset: 0x000003D9
		public Forester(TreeCuttingArea treeCuttingArea)
		{
			this._treeCuttingArea = treeCuttingArea;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021E8 File Offset: 0x000003E8
		public void SetReplantDeadTrees(bool replantDeadTrees)
		{
			this.ReplantDeadTrees = replantDeadTrees;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F1 File Offset: 0x000003F1
		public void Save(IEntitySaver entitySaver)
		{
			entitySaver.GetComponent(Forester.ForesterKey).Set(Forester.ReplantDeadTreesKey, this.ReplantDeadTrees);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002210 File Offset: 0x00000410
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Forester.ForesterKey);
			this.ReplantDeadTrees = component.Get(Forester.ReplantDeadTreesKey);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000223A File Offset: 0x0000043A
		public void DuplicateFrom(Forester source)
		{
			this.SetReplantDeadTrees(source.ReplantDeadTrees);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002248 File Offset: 0x00000448
		public bool Validate(PlantingSpot spot)
		{
			BlockObject plantingBlocker = spot.PlantingBlocker;
			if (plantingBlocker)
			{
				if (this.ReplantDeadTrees)
				{
					TreeComponent component = plantingBlocker.GetComponent<TreeComponent>();
					if (component != null && component.CanBeReplaced)
					{
						return !this._treeCuttingArea.IsInCuttingArea(spot.Coordinates);
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x04000008 RID: 8
		public static readonly ComponentKey ForesterKey = new ComponentKey("Forester");

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<bool> ReplantDeadTreesKey = new PropertyKey<bool>("ReplantDeadTrees");

		// Token: 0x0400000B RID: 11
		public readonly TreeCuttingArea _treeCuttingArea;
	}
}
