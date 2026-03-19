using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TemplateInstantiation;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x02000023 RID: 35
	public class PreviewFactory : ILoadableSingleton
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003768 File Offset: 0x00001968
		public PreviewFactory(TemplateInstantiator templateInstantiator, RootObjectProvider rootObjectProvider)
		{
			this._templateInstantiator = templateInstantiator;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000377E File Offset: 0x0000197E
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("PreviewFactory").transform;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000379C File Offset: 0x0000199C
		public Preview Create(PlaceableBlockObjectSpec template)
		{
			GameObject gameObject = this._templateInstantiator.Instantiate(template.Blueprint, this._parent, null);
			gameObject.SetActive(false);
			gameObject.name = gameObject.name.Replace("(Clone)", "Preview");
			Preview componentSlow = gameObject.GetComponentSlow<Preview>();
			componentSlow.GetComponent<BlockObject>().MarkAsPreviewAndInitialize();
			return componentSlow;
		}

		// Token: 0x04000069 RID: 105
		public readonly TemplateInstantiator _templateInstantiator;

		// Token: 0x0400006A RID: 106
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400006B RID: 107
		public Transform _parent;
	}
}
