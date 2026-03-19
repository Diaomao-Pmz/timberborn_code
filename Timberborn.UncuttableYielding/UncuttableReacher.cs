using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.Demolishing;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.UncuttableYielding
{
	// Token: 0x02000004 RID: 4
	public class UncuttableReacher : DemolishableReacher, IInitializableEntity
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public UncuttableReacher(PositionDestinationFactory positionDestinationFactory)
		{
			this._positionDestinationFactory = positionDestinationFactory;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CD File Offset: 0x000002CD
		public override IDestination Destination
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D5 File Offset: 0x000002D5
		public void InitializeEntity()
		{
			this._center = base.GetComponent<NaturalResourceCenterProvider>().GetWorldCenter();
			this._destination = this._positionDestinationFactory.Create(this._center, 0.5f);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002104 File Offset: 0x00000304
		public override void NotifyReservableReached(BaseComponent agent)
		{
			agent.GetComponent<CharacterModel>().LookToward(this._center);
		}

		// Token: 0x04000006 RID: 6
		public readonly PositionDestinationFactory _positionDestinationFactory;

		// Token: 0x04000007 RID: 7
		public IDestination _destination;

		// Token: 0x04000008 RID: 8
		public Vector3 _center;
	}
}
