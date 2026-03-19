using System;
using System.Collections.Generic;

namespace Bindito.Core.Internal
{
	// Token: 0x02000082 RID: 130
	public class BindingBuilderRegistry : IBindingBuilderRegistry
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000030DA File Offset: 0x000012DA
		public BindingBuilderRegistry(IBinder binder)
		{
			this._binder = binder;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003100 File Offset: 0x00001300
		public void RegisterBindingBuilder<T>(BindingBuilder<T> bindingBuilder) where T : class
		{
			Type typeFromHandle = typeof(T);
			if (this._boundBindingBuilders.ContainsKey(typeFromHandle))
			{
				throw new BinditoException("Can't bind type " + TypeFormatting.Format(typeFromHandle) + ", it's already bound.");
			}
			this._boundBindingBuilders[typeFromHandle] = bindingBuilder;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003150 File Offset: 0x00001350
		public void RegisterMultiBindingBuilder<T>(BindingBuilder<T> bindingBuilder) where T : class
		{
			Type typeFromHandle = typeof(T);
			List<IBindingBuilder> list;
			if (!this._boundMultiBindingBuilders.TryGetValue(typeFromHandle, out list))
			{
				list = new List<IBindingBuilder>();
				this._boundMultiBindingBuilders[typeFromHandle] = list;
			}
			list.Add(bindingBuilder);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003192 File Offset: 0x00001392
		public void BuildAllBindings()
		{
			this.BuildBindings();
			this.BuildMultiBindings();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000031A0 File Offset: 0x000013A0
		private void BuildBindings()
		{
			foreach (KeyValuePair<Type, IBindingBuilder> keyValuePair in this._boundBindingBuilders)
			{
				Type key = keyValuePair.Key;
				IBindingBuilder value = keyValuePair.Value;
				this._binder.Bind(key, value.Build());
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003210 File Offset: 0x00001410
		private void BuildMultiBindings()
		{
			foreach (KeyValuePair<Type, List<IBindingBuilder>> keyValuePair in this._boundMultiBindingBuilders)
			{
				Type key = keyValuePair.Key;
				foreach (IBindingBuilder bindingBuilder in keyValuePair.Value)
				{
					this._binder.MultiBind(key, bindingBuilder.Build());
				}
			}
		}

		// Token: 0x0400007C RID: 124
		private readonly IBinder _binder;

		// Token: 0x0400007D RID: 125
		private readonly Dictionary<Type, IBindingBuilder> _boundBindingBuilders = new Dictionary<Type, IBindingBuilder>();

		// Token: 0x0400007E RID: 126
		private readonly Dictionary<Type, List<IBindingBuilder>> _boundMultiBindingBuilders = new Dictionary<Type, List<IBindingBuilder>>();
	}
}
