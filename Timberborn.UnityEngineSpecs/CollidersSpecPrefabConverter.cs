using System;
using Timberborn.BlueprintPrefabSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x0200000D RID: 13
	public class CollidersSpecPrefabConverter : ISpecToPrefabConverter
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003009 File Offset: 0x00001209
		public bool CanConvert(ComponentSpec spec)
		{
			return spec is CollidersSpec;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003014 File Offset: 0x00001214
		public void Convert(GameObject owner, ComponentSpec spec)
		{
			CollidersSpec collidersSpec = (CollidersSpec)spec;
			foreach (BoxColliderSpec boxColliderSpec in collidersSpec.BoxColliders)
			{
				BoxCollider boxCollider = owner.AddComponent<BoxCollider>();
				boxCollider.center = boxColliderSpec.Center;
				boxCollider.size = boxColliderSpec.Size;
			}
			foreach (SphereColliderSpec sphereColliderSpec in collidersSpec.SphereColliders)
			{
				SphereCollider sphereCollider = owner.AddComponent<SphereCollider>();
				sphereCollider.center = sphereColliderSpec.Center;
				sphereCollider.radius = sphereColliderSpec.Radius;
			}
			foreach (CapsuleColliderSpec capsuleColliderSpec in collidersSpec.CapsuleColliders)
			{
				CapsuleCollider capsuleCollider = owner.AddComponent<CapsuleCollider>();
				capsuleCollider.center = capsuleColliderSpec.Center;
				capsuleCollider.radius = capsuleColliderSpec.Radius;
				capsuleCollider.height = capsuleColliderSpec.Height;
				capsuleCollider.direction = CollidersSpecPrefabConverter.GetDirection(capsuleColliderSpec.Axis);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003110 File Offset: 0x00001310
		public static int GetDirection(Axis axis)
		{
			int result;
			switch (axis)
			{
			case Axis.X:
				result = 0;
				break;
			case Axis.Y:
				result = 1;
				break;
			case Axis.Z:
				result = 2;
				break;
			default:
				throw new ArgumentOutOfRangeException("axis", axis, null);
			}
			return result;
		}
	}
}
