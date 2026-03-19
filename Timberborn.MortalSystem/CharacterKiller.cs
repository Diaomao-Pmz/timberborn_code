using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Characters;
using Timberborn.Common;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.MortalSystem
{
	// Token: 0x02000007 RID: 7
	public class CharacterKiller : ILoadableSingleton, IInputProcessor, IDevModule
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public CharacterKiller(EventBus eventBus, InputService inputService, DevModeManager devModeManager, CharacterPopulation characterPopulation, IRandomNumberGenerator randomNumberGenerator)
		{
			this._eventBus = eventBus;
			this._inputService = inputService;
			this._devModeManager = devModeManager;
			this._characterPopulation = characterPopulation;
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212D File Offset: 0x0000032D
		public void Load()
		{
			this._eventBus.Register(this);
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Kill selected character", CharacterKiller.DeleteObjectKey, delegate
			{
				this.KillSelectedCharacter(false);
			})).AddMethod(DevMethod.Create(string.Format("Kill {0:F0}% of characters", CharacterKiller.PartOfPopulation * 100f), new Action(this.KillPartOfPopulation))).AddMethod(DevMethod.Create("Kill all characters instantly", new Action(this.KillAllPopulation))).AddMethod(DevMethod.Create("Kill all characters except selected", new Action(this.KillAllExceptSelected))).Build();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E8 File Offset: 0x000003E8
		[OnEvent]
		public void OnSelectableObjectSelected(SelectableObjectSelectedEvent selectableObjectSelectedEvent)
		{
			Mortal component = selectableObjectSelectedEvent.SelectableObject.GetComponent<Mortal>();
			if (component)
			{
				this._selectedMortal = component;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002210 File Offset: 0x00000410
		[OnEvent]
		public void OnSelectableObjectUnselectedEvent(SelectableObjectUnselectedEvent selectableObjectUnselectedEvent)
		{
			this._selectedMortal = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		public bool ProcessInput()
		{
			if (this._devModeManager.Enabled && this._selectedMortal && this._inputService.IsKeyDown(CharacterKiller.DeleteObjectKey))
			{
				this.KillSelectedCharacter(this._inputService.IsKeyHeld(CharacterKiller.SkipDeleteConfirmationKey));
				return true;
			}
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226E File Offset: 0x0000046E
		public void KillSelectedCharacter(bool dieInstantly)
		{
			if (this._selectedMortal && !this._selectedMortal.ShouldDie)
			{
				CharacterKiller.KillCharacter(this._selectedMortal, dieInstantly);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002298 File Offset: 0x00000498
		public void KillPartOfPopulation()
		{
			List<Mortal> killableCharacters = this.GetKillableCharacters();
			if (!killableCharacters.IsEmpty<Mortal>())
			{
				int num = Mathf.CeilToInt(CharacterKiller.PartOfPopulation * (float)killableCharacters.Count);
				for (int i = 0; i < num; i++)
				{
					Mortal listElement = this._randomNumberGenerator.GetListElement<Mortal>(killableCharacters);
					CharacterKiller.KillCharacter(listElement, false);
					killableCharacters.Remove(listElement);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022F0 File Offset: 0x000004F0
		public void KillAllPopulation()
		{
			foreach (Mortal mortal in this.GetKillableCharacters())
			{
				CharacterKiller.KillCharacter(mortal, true);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002344 File Offset: 0x00000544
		public void KillAllExceptSelected()
		{
			foreach (Mortal mortal in this.GetKillableCharacters())
			{
				if (mortal != this._selectedMortal)
				{
					CharacterKiller.KillCharacter(mortal, true);
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023A0 File Offset: 0x000005A0
		public List<Mortal> GetKillableCharacters()
		{
			return (from character in this._characterPopulation.Characters
			select character.GetComponent<Mortal>() into mortal
			where !mortal.ShouldDie
			select mortal).ToList<Mortal>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000240C File Offset: 0x0000060C
		public static void KillCharacter(Mortal mortal, bool dieInstantly)
		{
			string firstName = mortal.GetComponent<Character>().FirstName;
			if (dieInstantly)
			{
				mortal.DieInstantly(firstName + " was killed instantly by a heartless dev.");
				return;
			}
			mortal.DiePubliclyAsSoonAsPossible(firstName + " was forced to die by an evil dev.");
		}

		// Token: 0x04000008 RID: 8
		public static readonly string DeleteObjectKey = "DeleteObject";

		// Token: 0x04000009 RID: 9
		public static readonly string SkipDeleteConfirmationKey = "SkipDeleteConfirmation";

		// Token: 0x0400000A RID: 10
		public static readonly float PartOfPopulation = 0.3f;

		// Token: 0x0400000B RID: 11
		public readonly EventBus _eventBus;

		// Token: 0x0400000C RID: 12
		public readonly InputService _inputService;

		// Token: 0x0400000D RID: 13
		public readonly DevModeManager _devModeManager;

		// Token: 0x0400000E RID: 14
		public readonly CharacterPopulation _characterPopulation;

		// Token: 0x0400000F RID: 15
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000010 RID: 16
		public Mortal _selectedMortal;
	}
}
