using System;
using Timberborn.CharacterModelSystem;
using Timberborn.Characters;
using Timberborn.Debugging;

namespace Timberborn.CharactersUI
{
	// Token: 0x02000009 RID: 9
	public class CharactersModelToggler : IDevModule
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000023F3 File Offset: 0x000005F3
		public CharactersModelToggler(CharacterPopulation characterPopulation)
		{
			this._characterPopulation = characterPopulation;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002402 File Offset: 0x00000602
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Toggle models: Characters", new Action(this.ToggleCharactersModels))).Build();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000242C File Offset: 0x0000062C
		public void ToggleCharactersModels()
		{
			this._charactersHidden = !this._charactersHidden;
			foreach (Character character in this._characterPopulation.Characters)
			{
				this.ToggleCharacterModels(character);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002498 File Offset: 0x00000698
		public void ToggleCharacterModels(Character character)
		{
			CharacterModel component = character.GetComponent<CharacterModel>();
			if (this._charactersHidden)
			{
				component.Hide();
				return;
			}
			component.Show();
		}

		// Token: 0x04000018 RID: 24
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x04000019 RID: 25
		public bool _charactersHidden;
	}
}
