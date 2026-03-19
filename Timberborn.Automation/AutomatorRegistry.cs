using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Timberborn.Common;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.SingletonSystem;

namespace Timberborn.Automation
{
	// Token: 0x02000015 RID: 21
	public class AutomatorRegistry : ILoadableSingleton
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x0000432D File Offset: 0x0000252D
		public AutomatorRegistry(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000435D File Offset: 0x0000255D
		public ReadOnlyList<Automator> Automators
		{
			get
			{
				return this._automators.AsReadOnlyList<Automator>();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000436A File Offset: 0x0000256A
		public ReadOnlyList<Automator> Transmitters
		{
			get
			{
				return this._transmitters.AsReadOnlyList<Automator>();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004377 File Offset: 0x00002577
		public ReadOnlyCollection<string> SortedTransmitterIds
		{
			get
			{
				return new ReadOnlyCollection<string>(this._sortedTransmitterIds.Values);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004389 File Offset: 0x00002589
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004397 File Offset: 0x00002597
		public bool AnyTransmitters()
		{
			return this._transmitters.Any<Automator>();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000043A4 File Offset: 0x000025A4
		public Automator FindTransmitterById(Guid entityId)
		{
			return this._transmitters.FirstOrDefault((Automator automator) => automator.GetComponent<EntityComponent>().EntityId == entityId);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000043D8 File Offset: 0x000025D8
		[OnEvent]
		public void OnEntityNameChangedEvent(EntityNameChangedEvent entityNameChangedEvent)
		{
			Automator component = entityNameChangedEvent.Entity.GetComponent<Automator>();
			if (component != null && component.IsTransmitter && this._sortedTransmitterIds.ContainsValue(component.AutomatorId))
			{
				this._sortedTransmitterIds.RemoveAt(this._sortedTransmitterIds.IndexOfValue(component.AutomatorId));
				this._sortedTransmitterIds.Add(component.SortingKey, component.AutomatorId);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004442 File Offset: 0x00002642
		public void Register(Automator automator)
		{
			this._automators.Add(automator);
			if (automator.IsTransmitter)
			{
				this._transmitters.Add(automator);
				this._sortedTransmitterIds.Add(automator.SortingKey, automator.AutomatorId);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000447B File Offset: 0x0000267B
		public void Unregister(Automator automator)
		{
			this._automators.Remove(automator);
			if (automator.IsTransmitter)
			{
				this._transmitters.Remove(automator);
				this._sortedTransmitterIds.Remove(automator.SortingKey);
			}
		}

		// Token: 0x0400005D RID: 93
		public readonly EventBus _eventBus;

		// Token: 0x0400005E RID: 94
		public readonly List<Automator> _automators = new List<Automator>();

		// Token: 0x0400005F RID: 95
		public readonly List<Automator> _transmitters = new List<Automator>();

		// Token: 0x04000060 RID: 96
		public readonly SortedList<NamedEntitySortingKey, string> _sortedTransmitterIds = new SortedList<NamedEntitySortingKey, string>();
	}
}
