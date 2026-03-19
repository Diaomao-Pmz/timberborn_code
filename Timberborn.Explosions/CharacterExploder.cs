using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.Navigation;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.Explosions
{
	// Token: 0x02000007 RID: 7
	public class CharacterExploder
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public CharacterExploder(CharacterPopulation characterPopulation, ITerrainService terrainService)
		{
			this._characterPopulation = characterPopulation;
			this._terrainService = terrainService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public void ExplodeCharactersAt(Vector3Int position, BaseComponent source)
		{
			for (int i = 0; i < this._characterPopulation.NumberOfCharacters; i++)
			{
				Character character = this._characterPopulation.Characters[i];
				Vector3Int characterWorldCoordinates = CharacterExploder.GetCharacterWorldCoordinates(character);
				if (position == characterWorldCoordinates)
				{
					this.KillCharacter(character, characterWorldCoordinates, source);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216C File Offset: 0x0000036C
		public void ExplodeCharactersAt(ReadOnlyHashSet<Vector3Int> positions, BaseComponent source)
		{
			for (int i = this._characterPopulation.NumberOfCharacters - 1; i >= 0; i--)
			{
				Character character = this._characterPopulation.Characters[i];
				Vector3Int characterWorldCoordinates = CharacterExploder.GetCharacterWorldCoordinates(character);
				if (positions.Contains(characterWorldCoordinates))
				{
					this.KillCharacter(character, characterWorldCoordinates, source);
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
		public static Vector3Int GetCharacterWorldCoordinates(Character character)
		{
			return NavigationCoordinateSystem.WorldToGridInt(character.Transform.position);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000003D4
		public void KillCharacter(Character character, Vector3Int coordinates, BaseComponent source)
		{
			ExplosionVulnerable component = character.GetComponent<ExplosionVulnerable>();
			if (component != null)
			{
				component.DieFromExplosion(source);
			}
			Vector3 position = character.Transform.position;
			int terrainHeightBelow = this._terrainService.GetTerrainHeightBelow(coordinates);
			character.Transform.position = new Vector3(position.x, (float)terrainHeightBelow, position.z);
		}

		// Token: 0x04000008 RID: 8
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x04000009 RID: 9
		public readonly ITerrainService _terrainService;
	}
}
