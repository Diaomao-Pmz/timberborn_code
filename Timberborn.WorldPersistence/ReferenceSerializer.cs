using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000015 RID: 21
	public class ReferenceSerializer
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000024CA File Offset: 0x000006CA
		public ReferenceSerializer(EntityRegistry entityRegistry)
		{
			this._entityRegistry = entityRegistry;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024E4 File Offset: 0x000006E4
		public IValueSerializer<T> Of<T>() where T : BaseComponent
		{
			return (IValueSerializer<T>)this._typedSerializers.GetOrAdd(typeof(T), () => new ReferenceSerializer.TypedReferenceSerializer<T>(this._entityRegistry));
		}

		// Token: 0x04000010 RID: 16
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000011 RID: 17
		public readonly Dictionary<Type, object> _typedSerializers = new Dictionary<Type, object>();

		// Token: 0x02000016 RID: 22
		public class TypedReferenceSerializer<T> : IValueSerializer<T> where T : BaseComponent
		{
			// Token: 0x06000035 RID: 53 RVA: 0x00002519 File Offset: 0x00000719
			public TypedReferenceSerializer(EntityRegistry entityRegistry)
			{
				this._entityRegistry = entityRegistry;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002528 File Offset: 0x00000728
			public void Serialize(T component, IValueSaver valueSaver)
			{
				EntityComponent component2 = component.GetComponent<EntityComponent>();
				INamedComponent namedComponent = component as INamedComponent;
				if (namedComponent != null)
				{
					valueSaver.AsString(string.Format("{0}:{1}", component2.EntityId, namedComponent.ComponentName));
					return;
				}
				valueSaver.AsString(string.Format("{0}", component2.EntityId));
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002590 File Offset: 0x00000790
			public Obsoletable<T> Deserialize(IValueLoader valueLoader)
			{
				Guid entityId;
				string text;
				ReferenceSerializer.TypedReferenceSerializer<T>.Parse(valueLoader.AsString(), out entityId, out text);
				EntityComponent entity = this._entityRegistry.GetEntity(entityId);
				if (entity != null)
				{
					T t = (text != null) ? ReferenceSerializer.TypedReferenceSerializer<T>.FindNamedComponent(entity, text) : entity.GetComponent<T>();
					if (t != null)
					{
						return t;
					}
				}
				return default(Obsoletable<T>);
			}

			// Token: 0x06000038 RID: 56 RVA: 0x000025E8 File Offset: 0x000007E8
			public static void Parse(string input, out Guid entityId, out string componentName)
			{
				int num = input.IndexOf(":", StringComparison.Ordinal);
				if (num == -1)
				{
					entityId = Guid.Parse(input);
					componentName = null;
					return;
				}
				entityId = Guid.Parse(input.Substring(0, num));
				componentName = input.Substring(num + 1);
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002634 File Offset: 0x00000834
			public static T FindNamedComponent(EntityComponent entity, string componentName)
			{
				return entity.GetComponentsAllocating<T>().SingleOrDefault(delegate(T component)
				{
					INamedComponent namedComponent = component as INamedComponent;
					return namedComponent != null && namedComponent.ComponentName == componentName;
				});
			}

			// Token: 0x04000012 RID: 18
			public readonly EntityRegistry _entityRegistry;
		}
	}
}
