using System;

namespace Timberborn.Persistence
{
	// Token: 0x0200000A RID: 10
	public interface IValueSerializer<T>
	{
		// Token: 0x0600007C RID: 124
		void Serialize(T value, IValueSaver valueSaver);

		// Token: 0x0600007D RID: 125
		Obsoletable<T> Deserialize(IValueLoader valueLoader);
	}
}
