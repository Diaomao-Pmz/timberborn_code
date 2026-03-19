using System;
using UnityEngine;

namespace Timberborn.NaturalResources
{
	// Token: 0x02000008 RID: 8
	public interface ISpawnValidator
	{
		// Token: 0x0600000F RID: 15
		bool CanSpawn(Vector3Int coordinates, string resourceTemplateName);
	}
}
