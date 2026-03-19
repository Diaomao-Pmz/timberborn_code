using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.GoodStackSystem
{
	// Token: 0x02000008 RID: 8
	public class GoodStackAccessible : BaseComponent, IAwakableComponent, IAccessibleNeeder
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022D9 File Offset: 0x000004D9
		public string AccessibleComponentName
		{
			get
			{
				return "GoodStack";
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022E0 File Offset: 0x000004E0
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022EE File Offset: 0x000004EE
		public void SetAccessible(Accessible accessible)
		{
			this._accessible = accessible;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F8 File Offset: 0x000004F8
		public void Enable()
		{
			this._accessible.SetAccesses(Enumerables.One<Vector3>(this._blockObjectCenter.WorldCenterGrounded), null);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002329 File Offset: 0x00000529
		public void Disable()
		{
			this._accessible.ClearAccesses();
		}

		// Token: 0x0400000D RID: 13
		public Accessible _accessible;

		// Token: 0x0400000E RID: 14
		public BlockObjectCenter _blockObjectCenter;
	}
}
