using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BatchControl;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Characters;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.CharactersBatchControl
{
	// Token: 0x02000005 RID: 5
	public class CharacterBatchControlTab : BatchControlTab
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000217F File Offset: 0x0000037F
		public CharacterBatchControlTab(VisualElementLoader visualElementLoader, BatchControlDistrict batchControlDistrict, CharacterBatchControlRowFactory characterBatchControlRowFactory, BatchControlRowGroupFactory batchControlRowGroupFactory, EventBus eventBus) : base(visualElementLoader, batchControlDistrict, eventBus)
		{
			this._characterBatchControlRowFactory = characterBatchControlRowFactory;
			this._batchControlRowGroupFactory = batchControlRowGroupFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021B0 File Offset: 0x000003B0
		public override string TabNameLocKey
		{
			get
			{
				return "BatchControl.Population";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021B7 File Offset: 0x000003B7
		public override string TabImage
		{
			get
			{
				return "Characters";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021BE File Offset: 0x000003BE
		public override string BindingKey
		{
			get
			{
				return "CharactersTab";
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C8 File Offset: 0x000003C8
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			EntityComponent entity = entityInitializedEvent.Entity;
			if (entity && entity.GetComponent<Character>())
			{
				this.CreateOrScheduleCreation(entity);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F8 File Offset: 0x000003F8
		[OnEvent]
		public void OnEntityDeletedEvent(EntityDeletedEvent entityDeletedEvent)
		{
			EntityComponent entity = entityDeletedEvent.Entity;
			if (entity)
			{
				this._entitiesScheduledToAdd.Remove(entity);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00000424
		[OnEvent]
		public void OnContaminableContaminationChangedEvent(ContaminableContaminationChangedEvent contaminableContaminationChangedEvent)
		{
			if (this._isBatchControlBoxVisible)
			{
				EntityComponent component = contaminableContaminationChangedEvent.Contaminable.GetComponent<EntityComponent>();
				this.RemoveAllEntityRows(component);
				this.CreateOrScheduleCreation(component);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002253 File Offset: 0x00000453
		[OnEvent]
		public void OnBatchControlBoxShownEvent(BatchControlBoxShownEvent batchControlBoxShownEvent)
		{
			this._isBatchControlBoxVisible = true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		[OnEvent]
		public void OnBatchControlBoxHiddenEvent(BatchControlBoxHiddenEvent batchControlBoxHiddenEvent)
		{
			this._isBatchControlBoxVisible = false;
			this.ClearCachedElements();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000226B File Offset: 0x0000046B
		public override void Show()
		{
			this._isTabVisible = true;
			this.AddScheduledCharacters();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000227A File Offset: 0x0000047A
		public override void Hide()
		{
			this._isTabVisible = false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002283 File Offset: 0x00000483
		public override IEnumerable<BatchControlRowGroup> GetRowGroups(IEnumerable<EntityComponent> entities)
		{
			this.ClearCachedElements();
			IEnumerable<IGrouping<string, EntityComponent>> enumerable = (from entity in entities
			where entity.GetComponent<Character>()
			select entity).GroupBy(new Func<EntityComponent, string>(CharacterBatchControlTab.GetGroupingKey));
			foreach (IGrouping<string, EntityComponent> grouping in enumerable)
			{
				string key = grouping.Key;
				BatchControlRowGroup batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(key, CharacterBatchControlTab.GetSortingKey(key));
				foreach (EntityComponent entity2 in grouping)
				{
					batchControlRowGroup.AddRow(this._characterBatchControlRowFactory.Create(entity2));
				}
				this._characterGroups.Add(key, batchControlRowGroup);
				yield return batchControlRowGroup;
			}
			IEnumerator<IGrouping<string, EntityComponent>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000229A File Offset: 0x0000049A
		public void CreateOrScheduleCreation(EntityComponent entity)
		{
			if (this._isTabVisible)
			{
				this.CreateNewRow(entity);
				base.UpdateRowsVisibility();
				return;
			}
			if (this._isBatchControlBoxVisible)
			{
				this._entitiesScheduledToAdd.Add(entity);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C8 File Offset: 0x000004C8
		public void RemoveAllEntityRows(EntityComponent entityComponent)
		{
			foreach (KeyValuePair<string, BatchControlRowGroup> keyValuePair in this._characterGroups)
			{
				string text;
				BatchControlRowGroup batchControlRowGroup;
				keyValuePair.Deconstruct(ref text, ref batchControlRowGroup);
				BatchControlRowGroup batchControlRowGroup2 = batchControlRowGroup;
				foreach (BatchControlRow batchControlRow in batchControlRowGroup2.GetEntityRows(entityComponent).ToImmutableArray<BatchControlRow>())
				{
					batchControlRowGroup2.RemoveRow(batchControlRow);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002358 File Offset: 0x00000558
		public void CreateNewRow(EntityComponent entity)
		{
			string groupingKey = CharacterBatchControlTab.GetGroupingKey(entity);
			BatchControlRow batchControlRow = this._characterBatchControlRowFactory.Create(entity);
			BatchControlRowGroup batchControlRowGroup;
			if (!this._characterGroups.TryGetValue(groupingKey, out batchControlRowGroup))
			{
				batchControlRowGroup = this._batchControlRowGroupFactory.CreateSortedWithTextHeader(groupingKey, CharacterBatchControlTab.GetSortingKey(groupingKey));
				this._characterGroups.Add(groupingKey, batchControlRowGroup);
				base.AddGroup(batchControlRowGroup);
			}
			batchControlRowGroup.AddRow(batchControlRow);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023B8 File Offset: 0x000005B8
		public void AddScheduledCharacters()
		{
			foreach (EntityComponent entity in this._entitiesScheduledToAdd)
			{
				this.CreateNewRow(entity);
			}
			base.UpdateRowsVisibility();
			this._entitiesScheduledToAdd.Clear();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000241C File Offset: 0x0000061C
		public void ClearCachedElements()
		{
			this._characterGroups.Clear();
			this._entitiesScheduledToAdd.Clear();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002434 File Offset: 0x00000634
		public static string GetGroupingKey(EntityComponent entityComponent)
		{
			Contaminable component = entityComponent.GetComponent<Contaminable>();
			if (component != null && component.IsContaminated)
			{
				return CharacterBatchControlTab.ContaminatedLocKey;
			}
			return entityComponent.GetComponent<SimpleLabeledEntitySpec>().EntityNameLocKey;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002464 File Offset: 0x00000664
		public static string GetSortingKey(string locKey)
		{
			if (locKey == CharacterBatchControlTab.AdultLocKey)
			{
				return "1";
			}
			if (locKey == CharacterBatchControlTab.ChildLocKey)
			{
				return "2";
			}
			if (locKey == CharacterBatchControlTab.ContaminatedLocKey)
			{
				return "3";
			}
			if (locKey == CharacterBatchControlTab.BotLocKey)
			{
				return "4";
			}
			throw new ArgumentOutOfRangeException("locKey", locKey, null);
		}

		// Token: 0x0400000D RID: 13
		public static readonly string AdultLocKey = "Beaver.Adult.TemplateName";

		// Token: 0x0400000E RID: 14
		public static readonly string ChildLocKey = "Beaver.Child.TemplateName";

		// Token: 0x0400000F RID: 15
		public static readonly string ContaminatedLocKey = "Beaver.Population.Contaminated";

		// Token: 0x04000010 RID: 16
		public static readonly string BotLocKey = "Bot.TemplateName";

		// Token: 0x04000011 RID: 17
		public readonly CharacterBatchControlRowFactory _characterBatchControlRowFactory;

		// Token: 0x04000012 RID: 18
		public readonly BatchControlRowGroupFactory _batchControlRowGroupFactory;

		// Token: 0x04000013 RID: 19
		public readonly Dictionary<string, BatchControlRowGroup> _characterGroups = new Dictionary<string, BatchControlRowGroup>();

		// Token: 0x04000014 RID: 20
		public readonly HashSet<EntityComponent> _entitiesScheduledToAdd = new HashSet<EntityComponent>();

		// Token: 0x04000015 RID: 21
		public bool _isBatchControlBoxVisible;

		// Token: 0x04000016 RID: 22
		public bool _isTabVisible;
	}
}
