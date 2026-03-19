using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateSystem;

namespace Timberborn.EntityUndoSystem
{
	// Token: 0x0200000E RID: 14
	public class UndoableEntityFactory
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000026B9 File Offset: 0x000008B9
		public UndoableEntityFactory(EntityService entityService, EntityRegistry entityRegistry, TemplateNameMapper templateNameMapper, UndoableEntitiesLoader undoableEntitiesLoader)
		{
			this._entityService = entityService;
			this._entityRegistry = entityRegistry;
			this._templateNameMapper = templateNameMapper;
			this._undoableEntitiesLoader = undoableEntitiesLoader;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026E0 File Offset: 0x000008E0
		public UndoableEntity CreateUninitialized(BaseComponent baseComponent)
		{
			EntityComponent component = baseComponent.GetComponent<EntityComponent>();
			return new UndoableEntity(this._entityService, this._entityRegistry, this._templateNameMapper, this._undoableEntitiesLoader, component.EntityId);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002717 File Offset: 0x00000917
		public UndoableEntity CreateInitialized(BaseComponent baseComponent)
		{
			UndoableEntity undoableEntity = this.CreateUninitialized(baseComponent);
			undoableEntity.InitializeUndoableState();
			return undoableEntity;
		}

		// Token: 0x0400001E RID: 30
		public readonly EntityService _entityService;

		// Token: 0x0400001F RID: 31
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000020 RID: 32
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x04000021 RID: 33
		public readonly UndoableEntitiesLoader _undoableEntitiesLoader;
	}
}
