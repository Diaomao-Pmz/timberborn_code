using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.WorldPersistence;

namespace Timberborn.MapEditorPlacementRandomizing
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectPlacementRandomizer : BaseComponent, IPersistentEntity, IPostInitializableEntity
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public BlockObjectPlacementRandomizer(IRandomNumberGenerator randomNumberGenerator, BlockObjectPlacementRandomizingService blockObjectPlacementRandomizingService)
		{
			this._randomNumberGenerator = randomNumberGenerator;
			this._blockObjectPlacementRandomizingService = blockObjectPlacementRandomizingService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002114 File Offset: 0x00000314
		public void PostInitializeEntity()
		{
			if (!this._wasLoaded && this._blockObjectPlacementRandomizingService.Randomize)
			{
				BlockObject component = base.GetComponent<BlockObject>();
				component.Reposition(new Placement(component.Placement.Coordinates, this.GetRandomOrientation(), this.GetRandomFlipMode()));
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		public void Save(IEntitySaver entitySaver)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002162 File Offset: 0x00000362
		public void Load(IEntityLoader entityLoader)
		{
			this._wasLoaded = true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216B File Offset: 0x0000036B
		public Orientation GetRandomOrientation()
		{
			return (Orientation)this._randomNumberGenerator.Range(0, 4);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217A File Offset: 0x0000037A
		public FlipMode GetRandomFlipMode()
		{
			return new FlipMode(this._randomNumberGenerator.Range(0f, 1f) > 0.5f);
		}

		// Token: 0x04000008 RID: 8
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000009 RID: 9
		public readonly BlockObjectPlacementRandomizingService _blockObjectPlacementRandomizingService;

		// Token: 0x0400000A RID: 10
		public bool _wasLoaded;
	}
}
