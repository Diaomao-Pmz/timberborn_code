using System;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200003E RID: 62
	public interface IBlockObjectValidator
	{
		// Token: 0x060001B9 RID: 441
		bool IsValid(BlockObject blockObject, out string errorMessage);
	}
}
