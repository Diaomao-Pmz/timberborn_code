using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.EntityNaming
{
	// Token: 0x0200000B RID: 11
	public class NamedEntity : BaseComponent, IAwakableComponent, IPersistentEntity, IInitializableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x000021F4 File Offset: 0x000003F4
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x0000222C File Offset: 0x0000042C
		public event EventHandler EntityNameChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002261 File Offset: 0x00000461
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002269 File Offset: 0x00000469
		public string EntityName { get; private set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002272 File Offset: 0x00000472
		public NamedEntity(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002281 File Offset: 0x00000481
		public bool IsEditable
		{
			get
			{
				NamedEntitySpec namedEntitySpec = this._namedEntitySpec;
				return namedEntitySpec != null && namedEntitySpec.IsEditable;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002294 File Offset: 0x00000494
		public NamedEntitySortingKey SortingKey
		{
			get
			{
				string sortableName;
				if ((sortableName = this._sortableEntityName) == null)
				{
					sortableName = (this._sortableEntityName = this.GenerateSortableEntityName());
				}
				return new NamedEntitySortingKey(sortableName, this._entityComponent.EntityId);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022CA File Offset: 0x000004CA
		public void Awake()
		{
			this._entityComponent = base.GetComponent<EntityComponent>();
			this._namedEntitySpec = base.GetComponent<NamedEntitySpec>();
			this._entityNamers = base.GetComponentsAllocating<IEntityNamer>();
			if (this._entityNamers.IsEmpty<IEntityNamer>())
			{
				throw new Exception("A NamedEntity needs at least one IEntityNamer.");
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002308 File Offset: 0x00000508
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsEditable)
			{
				if (this.EntityName == null)
				{
					string format = "Entity {0} is editable but has no name.";
					EntityComponent entityComponent = this._entityComponent;
					Debug.LogWarning(string.Format(format, (entityComponent != null) ? new Guid?(entityComponent.EntityId) : null));
					return;
				}
				entitySaver.GetComponent(NamedEntity.ComponentKey).Set(NamedEntity.EntityNameKey, this.EntityName);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002374 File Offset: 0x00000574
		[BackwardCompatible(2026, 2, 3, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			if (this.IsEditable)
			{
				PropertyKey<string> key = new PropertyKey<string>("Name");
				PropertyKey<string> key2 = new PropertyKey<string>("DistrictName");
				IObjectLoader objectLoader;
				if (entityLoader.TryGetComponent(NamedEntity.ComponentKey, out objectLoader) && objectLoader.Has<string>(NamedEntity.EntityNameKey))
				{
					this.SetEntityNameSilently(objectLoader.Get(NamedEntity.EntityNameKey));
					return;
				}
				if (entityLoader.TryGetComponent(new ComponentKey("Character"), out objectLoader) && objectLoader.Has<string>(key))
				{
					this.SetEntityNameSilently(objectLoader.Get(key));
					return;
				}
				if (entityLoader.TryGetComponent(new ComponentKey("DistrictCenter"), out objectLoader) && objectLoader.Has<string>(key2))
				{
					this.SetEntityNameSilently(objectLoader.Get(key2));
					return;
				}
				if (entityLoader.TryGetComponent(new ComponentKey("Automator"), out objectLoader) && objectLoader.Has<string>(key))
				{
					this.SetEntityNameSilently(objectLoader.Get(key));
					return;
				}
				Debug.LogWarning("Editable NamedEntity '" + base.Name + "' was loaded without a name.");
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000246D File Offset: 0x0000066D
		public void InitializeEntity()
		{
			if (string.IsNullOrEmpty(this.EntityName))
			{
				this.SetEntityName(this.GetHighestPriorityNamer().GenerateEntityName());
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002490 File Offset: 0x00000690
		public void SetEntityName(string entityName)
		{
			if (!string.Equals(this.EntityName, entityName))
			{
				this.SetEntityNameSilently(entityName);
				EventHandler entityNameChanged = this.EntityNameChanged;
				if (entityNameChanged != null)
				{
					entityNameChanged(this, EventArgs.Empty);
				}
				this._eventBus.Post(new EntityNameChangedEvent(this._entityComponent));
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024DF File Offset: 0x000006DF
		public void SetEntityNameSilently(string entityName)
		{
			this.EntityName = entityName;
			this._sortableEntityName = null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024EF File Offset: 0x000006EF
		public string GenerateSortableEntityName()
		{
			return NamedEntity.DigitsRegex.Replace(this.EntityName, (Match digits) => digits.Value.PadLeft(5, '0'));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002520 File Offset: 0x00000720
		public IEntityNamer GetHighestPriorityNamer()
		{
			IEntityNamer entityNamer = this._entityNamers[0];
			for (int i = 1; i < this._entityNamers.Count; i++)
			{
				IEntityNamer entityNamer2 = this._entityNamers[i];
				if (entityNamer2.EntityNamerPriority > entityNamer.EntityNamerPriority)
				{
					entityNamer = entityNamer2;
				}
			}
			return entityNamer;
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey ComponentKey = new ComponentKey("NamedEntity");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<string> EntityNameKey = new PropertyKey<string>("EntityName");

		// Token: 0x0400000C RID: 12
		public static readonly Regex DigitsRegex = new Regex("\\d+", RegexOptions.Compiled);

		// Token: 0x0400000F RID: 15
		public EntityComponent _entityComponent;

		// Token: 0x04000010 RID: 16
		public NamedEntitySpec _namedEntitySpec;

		// Token: 0x04000011 RID: 17
		public readonly EventBus _eventBus;

		// Token: 0x04000012 RID: 18
		public List<IEntityNamer> _entityNamers;

		// Token: 0x04000013 RID: 19
		public string _sortableEntityName;
	}
}
