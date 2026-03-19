using System;
using System.Reflection;
using Timberborn.BaseComponentSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.DuplicationSystem
{
	// Token: 0x02000006 RID: 6
	public class Duplicator
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020E8 File Offset: 0x000002E8
		public void Duplicate(BaseComponent sourceEntity, BaseComponent targetEntity)
		{
			if (sourceEntity && targetEntity)
			{
				foreach (object sourceComponent in sourceEntity.AllComponents)
				{
					Duplicator.DuplicateComponent(sourceComponent, targetEntity);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public static void DuplicateComponent(object sourceComponent, BaseComponent targetEntity)
		{
			typeof(Duplicator).GetMethod("DuplicateComponentGeneric", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
			{
				sourceComponent.GetType()
			}).Invoke(null, new object[]
			{
				sourceComponent,
				targetEntity
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public static void DuplicateComponentGeneric<T>(T sourceComponent, BaseComponent targetEntity)
		{
			if (sourceComponent is IDuplicable<T>)
			{
				foreach (object obj in targetEntity.AllComponents)
				{
					if (obj.GetType() == sourceComponent.GetType() && Duplicator.NamesMatch(sourceComponent, obj))
					{
						((IDuplicable<T>)obj).DuplicateFrom(sourceComponent);
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002228 File Offset: 0x00000428
		public static bool NamesMatch(object sourceComponent, object targetComponent)
		{
			INamedComponent namedComponent = sourceComponent as INamedComponent;
			if (namedComponent != null)
			{
				INamedComponent namedComponent2 = targetComponent as INamedComponent;
				return namedComponent2 != null && namedComponent2.ComponentName == namedComponent.ComponentName;
			}
			return true;
		}
	}
}
