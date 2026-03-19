using System;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.EnterableSystem
{
	// Token: 0x02000014 RID: 20
	public class EntererBoundsScaler : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000033B8 File Offset: 0x000015B8
		public void Awake()
		{
			this._entererBoundsScalerSpec = base.GetComponent<EntererBoundsScalerSpec>();
			Enterable component = base.GetComponent<Enterable>();
			component.EntererAdded += delegate(object _, EntererAddedEventArgs e)
			{
				this.ScaleBounds(e.Enterer);
			};
			component.EntererRemoved += delegate(object _, EntererRemovedEventArgs e)
			{
				EntererBoundsScaler.ResetBounds(e.Enterer);
			};
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003410 File Offset: 0x00001610
		public void ScaleBounds(Enterer enterer)
		{
			foreach (MeshRenderer meshRenderer in enterer.GameObject.GetComponentsInChildren<MeshRenderer>(true))
			{
				Bounds localBounds = meshRenderer.localBounds;
				localBounds.size *= this._entererBoundsScalerSpec.Scale;
				meshRenderer.localBounds = localBounds;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003468 File Offset: 0x00001668
		public static void ResetBounds(Enterer enterer)
		{
			MeshRenderer[] componentsInChildren = enterer.GameObject.GetComponentsInChildren<MeshRenderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].ResetLocalBounds();
			}
		}

		// Token: 0x0400002E RID: 46
		public EntererBoundsScalerSpec _entererBoundsScalerSpec;
	}
}
