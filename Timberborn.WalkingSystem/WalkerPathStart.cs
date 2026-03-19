using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x0200001B RID: 27
	public class WalkerPathStart : BaseComponent, IAwakableComponent
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000039D3 File Offset: 0x00001BD3
		public void Awake()
		{
			base.GetComponents<IPathStartProvider>(this._pathStartProviders);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000039E4 File Offset: 0x00001BE4
		public void GetPathStart(IDestination destination, List<PathCorner> pathCorners, out Vector3 start)
		{
			using (List<IPathStartProvider>.Enumerator enumerator = this._pathStartProviders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryGetPathStart(destination, pathCorners, out start))
					{
						return;
					}
				}
			}
			start = base.Transform.position;
		}

		// Token: 0x04000058 RID: 88
		public readonly List<IPathStartProvider> _pathStartProviders = new List<IPathStartProvider>();
	}
}
