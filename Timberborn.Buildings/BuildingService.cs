using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.Buildings
{
	// Token: 0x02000015 RID: 21
	public class BuildingService : ILoadableSingleton
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000034F6 File Offset: 0x000016F6
		public BuildingService(TemplateService templateService)
		{
			this._templateService = templateService;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003510 File Offset: 0x00001710
		public ReadOnlyList<BuildingSpec> Buildings
		{
			get
			{
				return this._buildings.AsReadOnlyList<BuildingSpec>();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000351D File Offset: 0x0000171D
		public void Load()
		{
			this._buildings.AddRange(this._templateService.GetAll<BuildingSpec>());
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003535 File Offset: 0x00001735
		public string GetTemplateName(BuildingSpec buildingSpec)
		{
			return buildingSpec.GetSpec<TemplateSpec>().TemplateName;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003544 File Offset: 0x00001744
		public BuildingSpec GetBuildingTemplate(string templateName)
		{
			BuildingSpec buildingSpec = this._buildings.SingleOrDefault((BuildingSpec building) => BuildingService.IsBuildingNamedExactly(templateName, building));
			if (buildingSpec == null)
			{
				throw new ArgumentException("Building not found: " + templateName + ".");
			}
			return buildingSpec;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003594 File Offset: 0x00001794
		public static bool IsBuildingNamedExactly(string templateName, BuildingSpec buildingSpec)
		{
			return buildingSpec.GetSpec<TemplateSpec>().IsNamedExactly(templateName);
		}

		// Token: 0x04000032 RID: 50
		public readonly TemplateService _templateService;

		// Token: 0x04000033 RID: 51
		public readonly List<BuildingSpec> _buildings = new List<BuildingSpec>();
	}
}
