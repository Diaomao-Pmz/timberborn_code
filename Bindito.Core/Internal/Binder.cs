using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bindito.Core.Internal
{
	// Token: 0x0200007C RID: 124
	public class Binder : IBinder
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000029EC File Offset: 0x00000BEC
		public Binder(IBinder parentBinder)
		{
			this._parentBinder = parentBinder;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002A11 File Offset: 0x00000C11
		public IReadOnlyDictionary<Type, Binding> Bindings
		{
			get
			{
				return new ReadOnlyDictionary<Type, Binding>(this._bindings);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002A20 File Offset: 0x00000C20
		public IReadOnlyDictionary<Type, IReadOnlyList<Binding>> MultiBindings
		{
			get
			{
				Dictionary<Type, IReadOnlyList<Binding>> dictionary = new Dictionary<Type, IReadOnlyList<Binding>>();
				foreach (KeyValuePair<Type, List<Binding>> keyValuePair in this._multiBindings)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value.AsReadOnly();
				}
				return dictionary;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002A8C File Offset: 0x00000C8C
		public void Bind(Type type, Binding binding)
		{
			Binding arg;
			if (this.TryGetBinding(type, out arg))
			{
				throw new BinditoException(string.Format("Can't bind type {0} to {1},", TypeFormatting.Format(type), binding) + string.Format(" it's already bound to {0}.", arg));
			}
			Binding arg2;
			if (this._parentBinder != null && this._parentBinder.TryGetExportedBinding(type, out arg2))
			{
				throw new BinditoException(string.Format("Can't bind type {0} to {1},", TypeFormatting.Format(type), binding) + string.Format(" it's already bound to {0} in parent container.", arg2));
			}
			this._bindings[type] = binding;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002B18 File Offset: 0x00000D18
		public void MultiBind(Type type, Binding binding)
		{
			List<Binding> list;
			if (!this._multiBindings.TryGetValue(type, out list))
			{
				list = new List<Binding>();
				this._multiBindings[type] = list;
			}
			list.Add(binding);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002B4F File Offset: 0x00000D4F
		public bool TryGetBinding(Type type, out Binding binding)
		{
			return this._bindings.TryGetValue(type, out binding);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002B5E File Offset: 0x00000D5E
		public bool TryGetExportedBinding(Type type, out Binding binding)
		{
			if (this.TryGetBinding(type, out binding) && binding.Exported)
			{
				return true;
			}
			binding = null;
			return false;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00002B7C File Offset: 0x00000D7C
		public IEnumerable<Binding> GetMultiBindings(Type type)
		{
			List<Binding> source;
			if (!this._multiBindings.TryGetValue(type, out source))
			{
				return Enumerable.Empty<Binding>();
			}
			return source.AsReadOnlyEnumerable<Binding>();
		}

		// Token: 0x0400006A RID: 106
		private readonly IBinder _parentBinder;

		// Token: 0x0400006B RID: 107
		private readonly Dictionary<Type, Binding> _bindings = new Dictionary<Type, Binding>();

		// Token: 0x0400006C RID: 108
		private readonly Dictionary<Type, List<Binding>> _multiBindings = new Dictionary<Type, List<Binding>>();
	}
}
