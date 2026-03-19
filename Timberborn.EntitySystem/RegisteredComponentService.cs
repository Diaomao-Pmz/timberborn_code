using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Timberborn.EntitySystem
{
	// Token: 0x0200001A RID: 26
	public class RegisteredComponentService
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002B90 File Offset: 0x00000D90
		public ImmutableArray<Type> GetRegisterableTypes(Type type)
		{
			ImmutableArray<Type> immutableArray;
			if (!this._registerableTypes.TryGetValue(type, out immutableArray))
			{
				immutableArray = RegisteredComponentService.FindRegisterableTypes(type);
				this._registerableTypes[type] = immutableArray;
			}
			return immutableArray;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static ImmutableArray<Type> FindRegisterableTypes(Type type)
		{
			List<Type> list = new List<Type>();
			Type type2 = type;
			while (type2 != null)
			{
				if (RegisteredComponentService.TypeIsRegisteredComponent(type2))
				{
					list.Add(type2);
				}
				type2 = type2.BaseType;
			}
			return list.ToImmutableArray<Type>();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C00 File Offset: 0x00000E00
		public static bool TypeIsRegisteredComponent(Type baseType)
		{
			return baseType.GetInterfaces().Contains(RegisteredComponentService.RegisteredComponentType);
		}

		// Token: 0x0400002A RID: 42
		public static readonly Type RegisteredComponentType = typeof(IRegisteredComponent);

		// Token: 0x0400002B RID: 43
		public readonly Dictionary<Type, ImmutableArray<Type>> _registerableTypes = new Dictionary<Type, ImmutableArray<Type>>();
	}
}
