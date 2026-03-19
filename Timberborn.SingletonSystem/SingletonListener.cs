using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Bindito.Core;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000016 RID: 22
	public class SingletonListener : IInjectionListener, IProvisionListener
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027CD File Offset: 0x000009CD
		public IEnumerable<object> Collect()
		{
			if (!this._collected)
			{
				this._allSingletons = this._providedSingletons.Concat(this._injectedSingletons).Distinct<object>().ToImmutableArray<object>();
				this._collected = true;
			}
			return this._allSingletons;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000280A File Offset: 0x00000A0A
		public void Listen(object injectee)
		{
			if (this.ObjectIsSingleton(injectee))
			{
				this.ThrowIfCollected(injectee);
				this._providedSingletons.Add(injectee);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002828 File Offset: 0x00000A28
		public void Listen(object injectee)
		{
			if (this.ObjectIsSingleton(injectee))
			{
				this.ThrowIfCollected(injectee);
				this._injectedSingletons.Add(injectee);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002848 File Offset: 0x00000A48
		public bool ObjectIsSingleton(object target)
		{
			Type type = target.GetType();
			bool result;
			if (this._typeCache.TryGetValue(type, out result))
			{
				return result;
			}
			bool flag = SingletonListener.TypeIsSingleton(type);
			this._typeCache.Add(type, flag);
			return flag;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002884 File Offset: 0x00000A84
		public static bool TypeIsSingleton(Type type)
		{
			if (type.GetCustomAttribute(typeof(SingletonAttribute), true) != null)
			{
				return true;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (interfaces[i].GetCustomAttribute(typeof(SingletonAttribute), true) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028D3 File Offset: 0x00000AD3
		public void ThrowIfCollected(object injectee)
		{
			if (this._collected)
			{
				throw new InvalidOperationException(string.Format("Attempting to add an instance of {0} after collecting.", injectee.GetType()));
			}
		}

		// Token: 0x0400001E RID: 30
		public readonly List<object> _providedSingletons = new List<object>();

		// Token: 0x0400001F RID: 31
		public readonly List<object> _injectedSingletons = new List<object>();

		// Token: 0x04000020 RID: 32
		public bool _collected;

		// Token: 0x04000021 RID: 33
		public ImmutableArray<object> _allSingletons;

		// Token: 0x04000022 RID: 34
		public readonly Dictionary<Type, bool> _typeCache = new Dictionary<Type, bool>();
	}
}
