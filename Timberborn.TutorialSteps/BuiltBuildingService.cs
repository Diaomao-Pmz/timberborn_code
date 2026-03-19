using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000012 RID: 18
	public class BuiltBuildingService : ILoadableSingleton
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002B11 File Offset: 0x00000D11
		public BuiltBuildingService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B36 File Offset: 0x00000D36
		public void Load()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B44 File Offset: 0x00000D44
		[OnEvent]
		public void OnEnteredUnfinishedState(EnteredUnfinishedStateEvent enteredUnfinishedStateEvent)
		{
			Building component = enteredUnfinishedStateEvent.BlockObject.GetComponent<Building>();
			if (component != null)
			{
				this.GetMutableUnfinishedBuildings(component).Add(component);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B70 File Offset: 0x00000D70
		[OnEvent]
		public void OnExitedUnfinishedState(ExitedUnfinishedStateEvent exitedUnfinishedState)
		{
			Building component = exitedUnfinishedState.BlockObject.GetComponent<Building>();
			if (component != null)
			{
				this.GetMutableUnfinishedBuildings(component).Remove(component);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B9C File Offset: 0x00000D9C
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			Building component = enteredFinishedStateEvent.BlockObject.GetComponent<Building>();
			if (component != null)
			{
				this.GetMutableFinishedBuildings(component).Add(component);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BC8 File Offset: 0x00000DC8
		[OnEvent]
		public void OnExitedFinishedState(ExitedFinishedStateEvent exitedFinishedState)
		{
			Building component = exitedFinishedState.BlockObject.GetComponent<Building>();
			if (component != null)
			{
				this.GetMutableFinishedBuildings(component).Remove(component);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public int NumberOfAllBuildings(IReadOnlyList<string> templateNames)
		{
			int num = 0;
			for (int i = 0; i < templateNames.Count; i++)
			{
				num += this.NumberOfAllBuildings(templateNames[i]);
			}
			return num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C28 File Offset: 0x00000E28
		public int NumberOfFinishedBuildings(IReadOnlyList<string> templateNames)
		{
			int num = 0;
			for (int i = 0; i < templateNames.Count; i++)
			{
				num += this.NumberOfFinishedBuildings(templateNames[i]);
			}
			return num;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C59 File Offset: 0x00000E59
		public IReadOnlyList<Building> GetFinishedBuildings(string templateName)
		{
			return this.GetMutableFinishedBuildings(templateName);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C62 File Offset: 0x00000E62
		public IReadOnlyList<Building> GetUnfinishedBuildings(string templateName)
		{
			return this.GetMutableUnfinishedBuildings(templateName);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C6B File Offset: 0x00000E6B
		public int NumberOfAllBuildings(string templateName)
		{
			return this.NumberOfUnfinishedBuildings(templateName) + this.NumberOfFinishedBuildings(templateName);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C7C File Offset: 0x00000E7C
		public int NumberOfFinishedBuildings(string templateName)
		{
			List<Building> list;
			if (!this._finishedBuildings.TryGetValue(templateName, out list))
			{
				return 0;
			}
			return list.Count;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public int NumberOfUnfinishedBuildings(string templateName)
		{
			List<Building> list;
			if (!this._unfinishedBuildings.TryGetValue(templateName, out list))
			{
				return 0;
			}
			return list.Count;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002CCC File Offset: 0x00000ECC
		public List<Building> GetMutableFinishedBuildings(Building building)
		{
			string templateName = building.GetComponent<TemplateSpec>().TemplateName;
			return this.GetMutableFinishedBuildings(templateName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002CEC File Offset: 0x00000EEC
		public List<Building> GetMutableFinishedBuildings(string templateName)
		{
			return BuiltBuildingService.GetOrCreateBuildings(this._finishedBuildings, templateName);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002CFC File Offset: 0x00000EFC
		public List<Building> GetMutableUnfinishedBuildings(Building building)
		{
			string templateName = building.GetComponent<TemplateSpec>().TemplateName;
			return this.GetMutableUnfinishedBuildings(templateName);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D1C File Offset: 0x00000F1C
		public List<Building> GetMutableUnfinishedBuildings(string templateName)
		{
			return BuiltBuildingService.GetOrCreateBuildings(this._unfinishedBuildings, templateName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static List<Building> GetOrCreateBuildings(Dictionary<string, List<Building>> allBuildings, string templateName)
		{
			List<Building> list;
			if (!allBuildings.TryGetValue(templateName, out list))
			{
				list = new List<Building>();
				allBuildings[templateName] = list;
			}
			return list;
		}

		// Token: 0x0400002E RID: 46
		public readonly EventBus _eventBus;

		// Token: 0x0400002F RID: 47
		public readonly Dictionary<string, List<Building>> _unfinishedBuildings = new Dictionary<string, List<Building>>();

		// Token: 0x04000030 RID: 48
		public readonly Dictionary<string, List<Building>> _finishedBuildings = new Dictionary<string, List<Building>>();
	}
}
