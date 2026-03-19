using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BeaverContaminationSystem;
using Timberborn.Beavers;
using Timberborn.Characters;
using Timberborn.DwellingSystem;
using Timberborn.GameDistricts;
using Timberborn.WorkSystem;

namespace Timberborn.GameDistrictsMigration
{
	// Token: 0x02000016 RID: 22
	public class MigrationService
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool RefusesWork(BaseComponent component)
		{
			return component.GetComponent<WorkRefuser>().RefusesWork;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F09 File Offset: 0x00001109
		public bool IsEmployed(BaseComponent component)
		{
			return component.GetComponent<Worker>().Employed;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F16 File Offset: 0x00001116
		public bool IsNotContaminated(BaseComponent component)
		{
			return !this.IsContaminated(component);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F22 File Offset: 0x00001122
		public bool IsContaminated(BaseComponent component)
		{
			return component.GetComponent<Contaminable>().IsContaminated;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F2F File Offset: 0x0000112F
		public bool HasHome(Beaver beaver)
		{
			return beaver.GetComponent<Dweller>().HasHome;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F3C File Offset: 0x0000113C
		public int GetDayOfBirth(BaseComponent component)
		{
			return component.GetComponent<Character>().DayOfBirth;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F4C File Offset: 0x0000114C
		public void Migrate(IEnumerable<BaseComponent> charactersToMove, DistrictCenter targetDistrict, int numberOfCharactersToMove)
		{
			this._charactersToMove.AddRange(charactersToMove.Take(numberOfCharactersToMove));
			if (this._charactersToMove.Count < numberOfCharactersToMove)
			{
				this._charactersToMove.Clear();
				throw new InvalidOperationException("Couldn't move enough beavers to the target district.");
			}
			foreach (BaseComponent baseComponent in this._charactersToMove)
			{
				baseComponent.GetComponent<Citizen>().AssignDistrict(targetDistrict);
			}
			this._charactersToMove.Clear();
		}

		// Token: 0x04000033 RID: 51
		public readonly List<BaseComponent> _charactersToMove = new List<BaseComponent>();
	}
}
