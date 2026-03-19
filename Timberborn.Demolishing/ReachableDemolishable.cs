using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectAccesses;
using Timberborn.BlockSystem;
using Timberborn.BuildingsReachability;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.Demolishing
{
	// Token: 0x0200001E RID: 30
	public class ReachableDemolishable : BaseComponent, IAwakableComponent, IStartableComponent, IUnreachableEntity
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00003954 File Offset: 0x00001B54
		public ReachableDemolishable(IDistrictService districtService)
		{
			this._districtService = districtService;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003963 File Offset: 0x00001B63
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._demolishable = base.GetComponent<Demolishable>();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003980 File Offset: 0x00001B80
		public void Start()
		{
			BlockObjectAccessible component = base.GetComponent<BlockObjectAccessible>();
			if (component != null)
			{
				this._accessible = component.Accessible;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000039A3 File Offset: 0x00001BA3
		public bool IsUnreachable()
		{
			return this._demolishable.IsMarked && !this.IsReachable();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000039C0 File Offset: 0x00001BC0
		public bool IsReachable(Accessible start, out float distance)
		{
			Vector3 vector;
			if (this._accessible && start.FindRoadToTerrainPath(this._accessible, out vector, out distance))
			{
				return true;
			}
			Vector3 worldCenterGrounded = this._blockObjectCenter.WorldCenterGrounded;
			if (start.FindRoadToTerrainPath(worldCenterGrounded, out distance))
			{
				return true;
			}
			distance = float.MaxValue;
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003A0D File Offset: 0x00001C0D
		public bool IsReachable()
		{
			if (this._accessible)
			{
				return this._districtService.IsOnInstantDistrictRoadSpill(this._accessible);
			}
			return this._districtService.IsOnInstantDistrictRoadSpill(this._blockObjectCenter.WorldCenterGrounded);
		}

		// Token: 0x04000042 RID: 66
		public readonly IDistrictService _districtService;

		// Token: 0x04000043 RID: 67
		public Accessible _accessible;

		// Token: 0x04000044 RID: 68
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000045 RID: 69
		public Demolishable _demolishable;
	}
}
