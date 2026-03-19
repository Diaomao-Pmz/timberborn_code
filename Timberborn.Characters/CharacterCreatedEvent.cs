using System;

namespace Timberborn.Characters
{
	// Token: 0x02000008 RID: 8
	public class CharacterCreatedEvent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000232F File Offset: 0x0000052F
		public Character Character { get; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002337 File Offset: 0x00000537
		public CharacterCreatedEvent(Character character)
		{
			this.Character = character;
		}
	}
}
