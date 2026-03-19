using System;
using UnityEngine;

namespace Timberborn.PrefabOptimization
{
	// Token: 0x02000018 RID: 24
	public class MaterialPropertyProvider
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000408C File Offset: 0x0000228C
		public IMaterialProperties GetProperties(Material material)
		{
			string name = material.shader.name;
			IMaterialProperties result;
			if (!(name == "Shader Graphs/EnvironmentURP"))
			{
				if (!(name == "Shader Graphs/VegetationURP"))
				{
					result = null;
				}
				else
				{
					result = VegetationMaterialProperties.FromMaterial(material);
				}
			}
			else
			{
				result = EnvironmentMaterialProperties.FromMaterial(material);
			}
			return result;
		}
	}
}
