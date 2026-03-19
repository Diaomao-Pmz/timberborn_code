using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200000F RID: 15
	public class Blueprint
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002959 File Offset: 0x00000B59
		public string Name { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002961 File Offset: 0x00000B61
		public ImmutableArray<Blueprint> Children { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002969 File Offset: 0x00000B69
		public ImmutableArray<ComponentSpec> Specs { get; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002971 File Offset: 0x00000B71
		public Blueprint(string name, IEnumerable<ComponentSpec> specs, ImmutableArray<Blueprint> children)
		{
			this.Name = name;
			this.Specs = this.CopySpecs(specs, null, null);
			this.Children = children;
			this.ValidateChildrenNames();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000299C File Offset: 0x00000B9C
		public Blueprint(Blueprint originalBlueprint, ComponentSpec originalSpec, ComponentSpec newSpec)
		{
			this.Name = ((originalBlueprint != null) ? originalBlueprint.Name : null);
			this.Specs = this.CopySpecs((originalBlueprint != null) ? new ImmutableArray<ComponentSpec>?(originalBlueprint.Specs) : null, originalSpec, newSpec);
			this.Children = Blueprint.CopyChildren(originalBlueprint);
			this.ValidateChildrenNames();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029FF File Offset: 0x00000BFF
		public bool HasSpec<T>()
		{
			return this.GetSpec<T>() != null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A10 File Offset: 0x00000C10
		public T GetSpec<T>()
		{
			foreach (ComponentSpec componentSpec in this.Specs)
			{
				if (componentSpec is T)
				{
					return componentSpec as T;
				}
			}
			return default(T);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A60 File Offset: 0x00000C60
		public object GetSpec(Type componentType)
		{
			foreach (ComponentSpec componentSpec in this.Specs)
			{
				if (componentSpec.GetType() == componentType)
				{
					return componentSpec;
				}
			}
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void GetSpecs<T>(List<T> specs)
		{
			foreach (ComponentSpec componentSpec in this.Specs)
			{
				if (componentSpec is T)
				{
					T item = componentSpec as T;
					specs.Add(item);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AEC File Offset: 0x00000CEC
		public bool IsAllowedByFeatureToggles()
		{
			RequiredFeatureToggleSpec spec = this.GetSpec<RequiredFeatureToggleSpec>();
			DisablingFeatureToggleSpec spec2 = this.GetSpec<DisablingFeatureToggleSpec>();
			return (spec == null || !spec.Disabled) && (spec2 == null || !spec2.Disabled);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B24 File Offset: 0x00000D24
		public ImmutableArray<ComponentSpec> CopySpecs(IEnumerable<ComponentSpec> specsToCopy, ComponentSpec specToIgnore, ComponentSpec specToAdd)
		{
			List<ComponentSpec> list = new List<ComponentSpec>();
			if (specsToCopy != null)
			{
				foreach (ComponentSpec componentSpec in specsToCopy)
				{
					if (componentSpec != specToIgnore)
					{
						componentSpec.DisableBlueprintCopying = true;
						List<ComponentSpec> list2 = list;
						ComponentSpec componentSpec2 = componentSpec.<Clone>$();
						componentSpec2.Blueprint = this;
						list2.Add(componentSpec2);
						componentSpec.DisableBlueprintCopying = false;
					}
				}
			}
			if (specToAdd != null)
			{
				list.Add(specToAdd);
			}
			return list.ToImmutableArray<ComponentSpec>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public static ImmutableArray<Blueprint> CopyChildren(Blueprint originalBlueprint)
		{
			List<Blueprint> list = new List<Blueprint>();
			if (originalBlueprint != null)
			{
				foreach (Blueprint originalBlueprint2 in originalBlueprint.Children)
				{
					list.Add(new Blueprint(originalBlueprint2, null, null));
				}
			}
			return list.ToImmutableArray<Blueprint>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void ValidateChildrenNames()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (Blueprint blueprint in this.Children)
			{
				if (!hashSet.Add(blueprint.Name))
				{
					throw new InvalidOperationException("Duplicate child name found: " + blueprint.Name + " in Blueprint " + this.Name);
				}
			}
		}
	}
}
