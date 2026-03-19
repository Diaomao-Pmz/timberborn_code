using System;
using Timberborn.Navigation;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000008 RID: 8
	public class DestinationValueSerializer : IValueSerializer<IDestination>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021B5 File Offset: 0x000003B5
		public DestinationValueSerializer(ReferenceSerializer referenceSerializer, PositionDestinationFactory positionDestinationFactory)
		{
			this._positionDestinationFactory = positionDestinationFactory;
			this._referenceSerializer = referenceSerializer;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021CC File Offset: 0x000003CC
		public void Serialize(IDestination value, IValueSaver valueSaver)
		{
			IObjectSaver objectSaver = valueSaver.AsObject();
			PositionDestination positionDestination = value as PositionDestination;
			if (positionDestination != null)
			{
				DestinationValueSerializer.ConvertPositionDestination(positionDestination, objectSaver);
				return;
			}
			AccessibleDestination accessibleDestination = value as AccessibleDestination;
			if (accessibleDestination == null)
			{
				throw new ArgumentOutOfRangeException("Unknown IDestination implementation");
			}
			this.ConvertAccessibleDestination(accessibleDestination, objectSaver);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002214 File Offset: 0x00000414
		public Obsoletable<IDestination> Deserialize(IValueLoader valueLoader)
		{
			IObjectLoader objectLoader = valueLoader.AsObject();
			string a = objectLoader.Get(DestinationValueSerializer.ImplementationTypeKey);
			if (a == "PositionDestination")
			{
				return this.DeconvertPositionDestination(objectLoader);
			}
			if (!(a == "AccessibleDestination"))
			{
				throw new ArgumentOutOfRangeException("Unknown IDestination implementation");
			}
			return this.DeconvertAccessibleDestination(objectLoader);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000226A File Offset: 0x0000046A
		public static void ConvertPositionDestination(PositionDestination positionDestination, IObjectSaver objectSaver)
		{
			objectSaver.Set(DestinationValueSerializer.ImplementationTypeKey, "PositionDestination");
			objectSaver.Set(DestinationValueSerializer.PositionKey, positionDestination.Destination);
			objectSaver.Set(DestinationValueSerializer.StoppingDistanceKey, positionDestination.StoppingDistance);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A0 File Offset: 0x000004A0
		public Obsoletable<IDestination> DeconvertPositionDestination(IObjectLoader objectLoader)
		{
			Vector3 position = objectLoader.Get(DestinationValueSerializer.PositionKey);
			float stoppingDistance = objectLoader.Get(DestinationValueSerializer.StoppingDistanceKey);
			return this._positionDestinationFactory.Create(position, stoppingDistance);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D7 File Offset: 0x000004D7
		public void ConvertAccessibleDestination(AccessibleDestination accessibleDestination, IObjectSaver objectSaver)
		{
			objectSaver.Set(DestinationValueSerializer.ImplementationTypeKey, "AccessibleDestination");
			if (accessibleDestination.Accessible)
			{
				objectSaver.Set<Accessible>(DestinationValueSerializer.AccessibleKey, accessibleDestination.Accessible, this._referenceSerializer.Of<Accessible>());
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002314 File Offset: 0x00000514
		public Obsoletable<IDestination> DeconvertAccessibleDestination(IObjectLoader objectLoader)
		{
			Accessible accessible;
			if (objectLoader.Has<Accessible>(DestinationValueSerializer.AccessibleKey) && objectLoader.GetObsoletable<Accessible>(DestinationValueSerializer.AccessibleKey, this._referenceSerializer.Of<Accessible>(), out accessible) && accessible)
			{
				return new AccessibleDestination(accessible);
			}
			return null;
		}

		// Token: 0x04000009 RID: 9
		public static readonly PropertyKey<string> ImplementationTypeKey = new PropertyKey<string>("ImplementationType");

		// Token: 0x0400000A RID: 10
		public static readonly PropertyKey<Vector3> PositionKey = new PropertyKey<Vector3>("Position");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<float> StoppingDistanceKey = new PropertyKey<float>("StoppingDistance");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<Accessible> AccessibleKey = new PropertyKey<Accessible>("Accessible");

		// Token: 0x0400000D RID: 13
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x0400000E RID: 14
		public readonly PositionDestinationFactory _positionDestinationFactory;
	}
}
