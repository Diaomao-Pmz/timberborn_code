using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.EntitySystem;

namespace Timberborn.TickSystem
{
	// Token: 0x02000011 RID: 17
	public class TickableEntity
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000231C File Offset: 0x0000051C
		public TickableEntity(EntityComponent entityComponent, IEnumerable<MeteredTickableComponent> tickableComponents, string originalName)
		{
			this._entityComponent = entityComponent;
			this._tickableComponents = tickableComponents.ToImmutableArray<MeteredTickableComponent>();
			this._originalName = originalName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000233E File Offset: 0x0000053E
		public Guid EntityId
		{
			get
			{
				return this._entityComponent.EntityId;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000234C File Offset: 0x0000054C
		public void Tick()
		{
			try
			{
				if (this._entityComponent.GameObject.activeInHierarchy)
				{
					this.TickTickableComponents();
				}
			}
			catch (Exception innerException)
			{
				string text = string.Format("Exception thrown while ticking entity {0}", this.EntityId);
				if (this._entityComponent)
				{
					text = text + " '" + this._entityComponent.Name + "'";
				}
				else
				{
					text = text + " '" + this._originalName + "' (destroyed)";
				}
				throw new Exception(text, innerException);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023E4 File Offset: 0x000005E4
		public void TickTickableComponents()
		{
			foreach (MeteredTickableComponent meteredTickableComponent in this._tickableComponents)
			{
				if (meteredTickableComponent.Enabled)
				{
					meteredTickableComponent.StartAndTick();
				}
			}
		}

		// Token: 0x04000011 RID: 17
		public readonly EntityComponent _entityComponent;

		// Token: 0x04000012 RID: 18
		public readonly ImmutableArray<MeteredTickableComponent> _tickableComponents;

		// Token: 0x04000013 RID: 19
		public readonly string _originalName;
	}
}
