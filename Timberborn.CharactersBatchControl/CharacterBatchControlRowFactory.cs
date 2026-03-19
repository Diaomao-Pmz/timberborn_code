using System;
using Timberborn.BatchControl;
using Timberborn.BeaversUI;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using Timberborn.DeteriorationSystemUI;
using Timberborn.EntitySystem;
using Timberborn.StatusSystemUI;
using Timberborn.WellbeingUI;

namespace Timberborn.CharactersBatchControl
{
	// Token: 0x02000004 RID: 4
	public class CharacterBatchControlRowFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CharacterBatchControlRowFactory(VisualElementLoader visualElementLoader, CharacterBatchControlRowItemFactory characterBatchControlRowItemFactory, BeaverBuildingsBatchControlRowItemFactory beaverBuildingsBatchControlRowItemFactory, DeteriorableBatchControlRowItemFactory deteriorableBatchControlRowItemFactory, AdulthoodBatchControlRowItemFactory adulthoodBatchControlRowItemFactory, WellbeingBatchControlRowItemFactory wellbeingBatchControlRowItemFactory, StatusBatchControlRowItemFactory statusBatchControlRowItemFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._characterBatchControlRowItemFactory = characterBatchControlRowItemFactory;
			this._beaverBuildingsBatchControlRowItemFactory = beaverBuildingsBatchControlRowItemFactory;
			this._deteriorableBatchControlRowItemFactory = deteriorableBatchControlRowItemFactory;
			this._adulthoodBatchControlRowItemFactory = adulthoodBatchControlRowItemFactory;
			this._wellbeingBatchControlRowItemFactory = wellbeingBatchControlRowItemFactory;
			this._statusBatchControlRowItemFactory = statusBatchControlRowItemFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public BatchControlRow Create(EntityComponent entity)
		{
			return new BatchControlRow(this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRow"), entity, new IBatchControlRowItem[]
			{
				this._characterBatchControlRowItemFactory.Create(entity),
				this._beaverBuildingsBatchControlRowItemFactory.Create(entity),
				this._deteriorableBatchControlRowItemFactory.Create(entity),
				this._adulthoodBatchControlRowItemFactory.Create(entity),
				this._wellbeingBatchControlRowItemFactory.Create(entity),
				this._statusBatchControlRowItemFactory.Create(entity)
			});
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000007 RID: 7
		public readonly CharacterBatchControlRowItemFactory _characterBatchControlRowItemFactory;

		// Token: 0x04000008 RID: 8
		public readonly BeaverBuildingsBatchControlRowItemFactory _beaverBuildingsBatchControlRowItemFactory;

		// Token: 0x04000009 RID: 9
		public readonly DeteriorableBatchControlRowItemFactory _deteriorableBatchControlRowItemFactory;

		// Token: 0x0400000A RID: 10
		public readonly AdulthoodBatchControlRowItemFactory _adulthoodBatchControlRowItemFactory;

		// Token: 0x0400000B RID: 11
		public readonly WellbeingBatchControlRowItemFactory _wellbeingBatchControlRowItemFactory;

		// Token: 0x0400000C RID: 12
		public readonly StatusBatchControlRowItemFactory _statusBatchControlRowItemFactory;
	}
}
