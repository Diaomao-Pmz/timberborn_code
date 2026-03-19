using System;
using System.Text.RegularExpressions;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.TemplateSystem;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000010 RID: 16
	public class NumberedEntityNamer : BaseComponent, IAwakableComponent, IEntityNamer, IRegisteredComponent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002862 File Offset: 0x00000A62
		public NumberedEntityNamer(EntityComponentRegistry entityComponentRegistry, NumberedEntityNamerService numberedEntityNamerService, ILoc loc)
		{
			this._entityComponentRegistry = entityComponentRegistry;
			this._numberedEntityNamerService = numberedEntityNamerService;
			this._loc = loc;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000287F File Offset: 0x00000A7F
		public int EntityNamerPriority
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002884 File Offset: 0x00000A84
		public void Awake()
		{
			this._namedEntity = base.GetComponent<NamedEntity>();
			this._labeledEntity = base.GetComponent<LabeledEntity>();
			NumberedEntityNamerSpec component = base.GetComponent<NumberedEntityNamerSpec>();
			this._format = (string.IsNullOrEmpty((component != null) ? component.FormatLocKey : null) ? NumberedEntityNamer.DefaultFormatLocKey : component.FormatLocKey);
			this._numberingGroup = (string.IsNullOrEmpty((component != null) ? component.NumberingGroup : null) ? base.GetComponent<TemplateSpec>().TemplateName : component.NumberingGroup);
			this._isPersistent = (component != null && component.IsPersistent);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002914 File Offset: 0x00000B14
		public string GenerateEntityName()
		{
			if (!this._isPersistent)
			{
				return this.GenerateNameInferred();
			}
			return this.GenerateNamePersistent();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000292B File Offset: 0x00000B2B
		public string GenerateNamePersistent()
		{
			return this.Format(this._numberedEntityNamerService.GenerateNumber(this._numberingGroup));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002944 File Offset: 0x00000B44
		public string GenerateNameInferred()
		{
			return this.Format(this.FindMaxExistingNumber() + 1);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002954 File Offset: 0x00000B54
		public int FindMaxExistingNumber()
		{
			int num = 0;
			foreach (NumberedEntityNamer numberedEntityNamer in this._entityComponentRegistry.GetAll<NumberedEntityNamer>())
			{
				string entityName = numberedEntityNamer._namedEntity.EntityName;
				int num2;
				if (numberedEntityNamer != this && !string.IsNullOrEmpty(entityName) && string.Equals(numberedEntityNamer._numberingGroup, this._numberingGroup) && NumberedEntityNamer.TryMatchNumber(entityName, out num2) && num2 > num && this.Format(num2).Equals(entityName))
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029F0 File Offset: 0x00000BF0
		public string Format(int number)
		{
			ILoc loc = this._loc;
			string format = this._format;
			LabeledEntity labeledEntity = this._labeledEntity;
			return loc.T<int, string>(format, number, ((labeledEntity != null) ? labeledEntity.DisplayName : null) ?? "");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A20 File Offset: 0x00000C20
		public static bool TryMatchNumber(string name, out int number)
		{
			Match match = NumberedEntityNamer.NumberRegex.Match(name);
			if (match.Success)
			{
				number = int.Parse(match.Value);
				return true;
			}
			number = 0;
			return false;
		}

		// Token: 0x0400001B RID: 27
		public static readonly string DefaultFormatLocKey = "Core.NameWithNumber";

		// Token: 0x0400001C RID: 28
		public static readonly Regex NumberRegex = new Regex("(?<![0-9])-?[0-9]{1,4}(?![0-9])", RegexOptions.Compiled);

		// Token: 0x0400001D RID: 29
		public readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x0400001E RID: 30
		public readonly NumberedEntityNamerService _numberedEntityNamerService;

		// Token: 0x0400001F RID: 31
		public readonly ILoc _loc;

		// Token: 0x04000020 RID: 32
		public NamedEntity _namedEntity;

		// Token: 0x04000021 RID: 33
		public LabeledEntity _labeledEntity;

		// Token: 0x04000022 RID: 34
		public string _format;

		// Token: 0x04000023 RID: 35
		public string _numberingGroup;

		// Token: 0x04000024 RID: 36
		public bool _isPersistent;
	}
}
