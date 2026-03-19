using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.Demolishing
{
	// Token: 0x02000007 RID: 7
	public class AccessibleDemolishableReacher : DemolishableReacher, IPostInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public override IDestination Destination
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		public void PostInitializeEntity()
		{
			this._destination = new AccessibleDestination(base.GetEnabledComponent<Accessible>());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211C File Offset: 0x0000031C
		public override void NotifyReservableReached(BaseComponent agent)
		{
			BlockObjectCenter component = base.GetComponent<BlockObjectCenter>();
			Vector3 target = component ? component.WorldCenterGrounded : base.Transform.position;
			agent.GetComponent<CharacterModel>().LookToward(target);
		}

		// Token: 0x04000008 RID: 8
		public IDestination _destination;
	}
}
