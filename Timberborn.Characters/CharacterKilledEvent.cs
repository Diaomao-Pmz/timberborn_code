using System;

namespace Timberborn.Characters
{
	// Token: 0x02000009 RID: 9
	public class CharacterKilledEvent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002346 File Offset: 0x00000546
		public Character Character { get; }

		// Token: 0x0600001A RID: 26 RVA: 0x0000234E File Offset: 0x0000054E
		public CharacterKilledEvent(Character character)
		{
			this.Character = character;
		}
	}
}
