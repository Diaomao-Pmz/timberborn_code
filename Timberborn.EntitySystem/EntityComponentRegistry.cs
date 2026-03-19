using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000009 RID: 9
	public class EntityComponentRegistry
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002408 File Offset: 0x00000608
		public EntityComponentRegistry(RegisteredComponentService registeredComponentService)
		{
			this._registeredComponentService = registeredComponentService;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002424 File Offset: 0x00000624
		public IEnumerable<T> GetAll<T>() where T : class, IRegisteredComponent
		{
			List<IRegisteredComponent> source;
			if (!this._registeredComponents.TryGetValue(typeof(T), out source))
			{
				return Enumerable.Empty<T>();
			}
			return source.Cast<T>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002456 File Offset: 0x00000656
		public IEnumerable<T> GetEnabled<T>() where T : BaseComponent, IRegisteredComponent
		{
			return from component in this.GetAll<T>()
			where component.Enabled
			select component;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002484 File Offset: 0x00000684
		public void Register(EntityComponent entityComponent)
		{
			foreach (IRegisteredComponent registeredComponent in entityComponent.RegisteredComponents)
			{
				this.Register(registeredComponent);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D8 File Offset: 0x000006D8
		public void Unregister(EntityComponent entityComponent)
		{
			foreach (IRegisteredComponent registeredComponent in entityComponent.RegisteredComponents)
			{
				this.Unregister(registeredComponent);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000252C File Offset: 0x0000072C
		public void Register(IRegisteredComponent registeredComponent)
		{
			Type type = registeredComponent.GetType();
			foreach (Type type2 in this._registeredComponentService.GetRegisterableTypes(type))
			{
				this.RegisterAsType(registeredComponent, type2);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002570 File Offset: 0x00000770
		public void Unregister(IRegisteredComponent registeredComponent)
		{
			Type type = registeredComponent.GetType();
			foreach (Type type2 in this._registeredComponentService.GetRegisterableTypes(type))
			{
				this.UnregisterAsType(registeredComponent, type2);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025B4 File Offset: 0x000007B4
		public void RegisterAsType(IRegisteredComponent registeredComponent, Type type)
		{
			List<IRegisteredComponent> list;
			if (!this._registeredComponents.TryGetValue(type, out list))
			{
				list = new List<IRegisteredComponent>();
				this._registeredComponents[type] = list;
			}
			list.Add(registeredComponent);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025EB File Offset: 0x000007EB
		public void UnregisterAsType(IRegisteredComponent registeredComponent, Type type)
		{
			this._registeredComponents[type].Remove(registeredComponent);
		}

		// Token: 0x04000013 RID: 19
		public readonly RegisteredComponentService _registeredComponentService;

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<Type, List<IRegisteredComponent>> _registeredComponents = new Dictionary<Type, List<IRegisteredComponent>>();
	}
}
