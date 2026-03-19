using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BlueprintSystem;
using Timberborn.Common;

namespace Timberborn.GameFactionSystem
{
	// Token: 0x02000004 RID: 4
	public class FactionBlueprintModifierProvider : IBlueprintModifierProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public void Initialize(IEnumerable<BlueprintModifierSpec> modifiers)
		{
			Asserts.IsFalse<FactionBlueprintModifierProvider>(this, this.Initialized, "Initialized");
			this._blueprintModifierSpecs = modifiers.ToImmutableArray<BlueprintModifierSpec>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020DF File Offset: 0x000002DF
		public string ModifierName
		{
			get
			{
				return "Faction modifier";
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E6 File Offset: 0x000002E6
		public IEnumerable<string> GetModifiers(string blueprintPath)
		{
			if (this.Initialized)
			{
				foreach (BlueprintModifierSpec blueprintModifierSpec in this._blueprintModifierSpecs)
				{
					if (blueprintModifierSpec.Original.Asset.Path == blueprintPath)
					{
						yield return blueprintModifierSpec.Modifier.Asset.Content;
					}
				}
				ImmutableArray<BlueprintModifierSpec>.Enumerator enumerator = default(ImmutableArray<BlueprintModifierSpec>.Enumerator);
			}
			yield break;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020FD File Offset: 0x000002FD
		public bool Initialized
		{
			get
			{
				return !this._blueprintModifierSpecs.IsDefault;
			}
		}

		// Token: 0x04000006 RID: 6
		public ImmutableArray<BlueprintModifierSpec> _blueprintModifierSpecs;
	}
}
