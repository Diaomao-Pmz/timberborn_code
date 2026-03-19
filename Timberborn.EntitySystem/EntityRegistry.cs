using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.TemplateSystem;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200000F RID: 15
	public class EntityRegistry
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000267A File Offset: 0x0000087A
		public ReadOnlyList<EntityComponent> Entities
		{
			get
			{
				return this._entitiesInInstantiationOrder.AsReadOnlyList<EntityComponent>();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002687 File Offset: 0x00000887
		public void AddEntity(EntityComponent entityComponent)
		{
			this._entities.Add(entityComponent.EntityId, entityComponent);
			this._entitiesInInstantiationOrder.Add(entityComponent);
			this.RegisterInstantiatedTemplate(entityComponent);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026AE File Offset: 0x000008AE
		public void RemoveEntity(EntityComponent entityComponent)
		{
			this._entities.Remove(entityComponent.EntityId);
			this._entitiesInInstantiationOrder.Remove(entityComponent);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026CF File Offset: 0x000008CF
		public EntityComponent GetEntity(Guid entityId)
		{
			return this._entities.GetOrDefault(entityId);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026DD File Offset: 0x000008DD
		public bool WasTemplateInstantiated(string templateName)
		{
			return this._instantiatedEntityTemplates.Contains(templateName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026EC File Offset: 0x000008EC
		public void RegisterInstantiatedTemplate(EntityComponent entityComponent)
		{
			TemplateSpec component = entityComponent.GetComponent<TemplateSpec>();
			string text = (component != null) ? component.TemplateName : null;
			if (text != null)
			{
				this._instantiatedEntityTemplates.Add(text);
			}
		}

		// Token: 0x0400001B RID: 27
		public readonly Dictionary<Guid, EntityComponent> _entities = new Dictionary<Guid, EntityComponent>();

		// Token: 0x0400001C RID: 28
		public readonly List<EntityComponent> _entitiesInInstantiationOrder = new List<EntityComponent>();

		// Token: 0x0400001D RID: 29
		public readonly HashSet<string> _instantiatedEntityTemplates = new HashSet<string>();
	}
}
