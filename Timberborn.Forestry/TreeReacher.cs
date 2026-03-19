using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.ReservableSystem;
using Timberborn.WalkingSystem;
using UnityEngine;

namespace Timberborn.Forestry
{
	// Token: 0x02000019 RID: 25
	public class TreeReacher : ReservableReacher, IInitializableEntity
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000316D File Offset: 0x0000136D
		public TreeReacher(PositionDestinationFactory positionDestinationFactory)
		{
			this._positionDestinationFactory = positionDestinationFactory;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000317C File Offset: 0x0000137C
		public override IDestination Destination
		{
			get
			{
				return this._destination;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003184 File Offset: 0x00001384
		public void InitializeEntity()
		{
			Vector3 worldCenter = base.GetComponent<NaturalResourceCenterProvider>().GetWorldCenter();
			float diameterScale = base.GetComponent<NaturalResourceModelRandomizer>().DiameterScale;
			float stoppingDistance = base.GetComponent<TreeCuttingRadiusSpec>().Radius * diameterScale + TreeReacher.CuttingPositionOffset;
			this._destination = this._positionDestinationFactory.Create(worldCenter, stoppingDistance);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000031D0 File Offset: 0x000013D0
		public override void NotifyReservableReached(BaseComponent agent)
		{
			CharacterModel component = agent.GetComponent<CharacterModel>();
			component.LookToward(this._destination.Destination);
			Vector3 position = component.Transform.position;
			Vector3 vector = this._destination.Destination - position;
			Vector3 vector2 = vector.normalized * this._destination.StoppingDistance;
			Vector3 vector3 = vector - vector2;
			component.Transform.position = position + vector3;
		}

		// Token: 0x04000032 RID: 50
		public static readonly float CuttingPositionOffset = 0.182f;

		// Token: 0x04000033 RID: 51
		public readonly PositionDestinationFactory _positionDestinationFactory;

		// Token: 0x04000034 RID: 52
		public PositionDestination _destination;
	}
}
