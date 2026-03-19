using System;
using Timberborn.Localization;

namespace Timberborn.CoreUI
{
	// Token: 0x02000017 RID: 23
	public interface ILocalizableElement
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000064 RID: 100
		bool IsSet { get; }

		// Token: 0x06000065 RID: 101
		void Localize(ILoc loc);
	}
}
