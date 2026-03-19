using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000020 RID: 32
	public class BlockObjectValidationService
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000045E0 File Offset: 0x000027E0
		public BlockObjectValidationService(IEnumerable<IBlockObjectValidator> blockObjectValidators)
		{
			this._blockObjectValidators = blockObjectValidators.ToImmutableArray<IBlockObjectValidator>();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000045F4 File Offset: 0x000027F4
		public bool IsValid(BlockObject blockObject)
		{
			ImmutableArray<IBlockObjectValidator>.Enumerator enumerator = this._blockObjectValidators.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text;
				if (!enumerator.Current.IsValid(blockObject, out text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000462C File Offset: 0x0000282C
		public bool AreValid(IReadOnlyList<BaseComponent> previews, out string errorMessage)
		{
			foreach (BaseComponent baseComponent in previews)
			{
				BlockObject component = baseComponent.GetComponent<BlockObject>();
				ImmutableArray<IBlockObjectValidator>.Enumerator enumerator2 = this._blockObjectValidators.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (!enumerator2.Current.IsValid(component, out errorMessage))
					{
						return false;
					}
				}
			}
			errorMessage = string.Empty;
			return true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000046A8 File Offset: 0x000028A8
		public bool AreValid(IReadOnlyList<BaseComponent> previews)
		{
			string text;
			return this.AreValid(previews, out text);
		}

		// Token: 0x0400008C RID: 140
		public readonly ImmutableArray<IBlockObjectValidator> _blockObjectValidators;
	}
}
