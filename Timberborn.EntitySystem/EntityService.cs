using System;
using Bindito.Core;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using UnityEngine;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000010 RID: 16
	public class EntityService
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002745 File Offset: 0x00000945
		public EntityService(TemplateInstantiator templateInstantiator, IContainer container, EntityRegistry entityRegistry, EventBus eventBus)
		{
			this._templateInstantiator = templateInstantiator;
			this._container = container;
			this._entityRegistry = entityRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000276A File Offset: 0x0000096A
		public EntityComponent Instantiate(Blueprint template)
		{
			return this.Instantiate(template, Guid.NewGuid());
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002778 File Offset: 0x00000978
		public EntityComponent Instantiate(Blueprint template, Guid id)
		{
			EntityComponent instance = this._container.GetInstance<EntityComponent>();
			instance.SetEntityId(id);
			this._templateInstantiator.Instantiate(template, null, instance);
			this._entityRegistry.AddEntity(instance);
			this._eventBus.Post(new EntityCreatedEvent(instance));
			return instance;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027C8 File Offset: 0x000009C8
		public void Delete(BaseComponent entity)
		{
			EntityComponent component = entity.GetComponent<EntityComponent>();
			component.Delete();
			this._entityRegistry.RemoveEntity(component);
			Object.Destroy(component.GameObject);
		}

		// Token: 0x0400001E RID: 30
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x0400001F RID: 31
		public readonly IContainer _container;

		// Token: 0x04000020 RID: 32
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000021 RID: 33
		public readonly EventBus _eventBus;
	}
}
