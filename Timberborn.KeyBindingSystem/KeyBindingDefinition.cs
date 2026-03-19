using System;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000017 RID: 23
	public class KeyBindingDefinition
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000363D File Offset: 0x0000183D
		public KeyBindingSpec KeyBindingSpec { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003645 File Offset: 0x00001845
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000364D File Offset: 0x0000184D
		public InputBinding PrimaryInputBinding { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003656 File Offset: 0x00001856
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000365E File Offset: 0x0000185E
		public InputBinding SecondaryInputBinding { get; private set; }

		// Token: 0x0600009C RID: 156 RVA: 0x00003667 File Offset: 0x00001867
		public KeyBindingDefinition(KeyBindingSpec keyBindingSpec)
		{
			this.KeyBindingSpec = keyBindingSpec;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003676 File Offset: 0x00001876
		public static KeyBindingDefinition Create(KeyBindingSpec keyBindingSpec, CustomInputBinding customPrimaryInputBinding, CustomInputBinding customSecondaryInputBinding)
		{
			KeyBindingDefinition keyBindingDefinition = new KeyBindingDefinition(keyBindingSpec);
			keyBindingDefinition.RebindPrimaryInputBinding(customPrimaryInputBinding);
			keyBindingDefinition.RebindSecondaryInputBinding(customSecondaryInputBinding);
			return keyBindingDefinition;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000368C File Offset: 0x0000188C
		public void RebindPrimaryInputBinding(CustomInputBinding customInputBinding)
		{
			this.PrimaryInputBinding = this.CreateInputBinding(customInputBinding, this.KeyBindingSpec.GetSpec<PrimaryInputBindingSpec>());
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000036A6 File Offset: 0x000018A6
		public void RebindSecondaryInputBinding(CustomInputBinding customInputBinding)
		{
			this.SecondaryInputBinding = this.CreateInputBinding(customInputBinding, this.KeyBindingSpec.GetSpec<SecondaryInputBindingSpec>());
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000036C0 File Offset: 0x000018C0
		public void ResetToDefault()
		{
			this.RebindPrimaryInputBinding(null);
			this.RebindSecondaryInputBinding(null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036D0 File Offset: 0x000018D0
		public InputBinding CreateInputBinding(CustomInputBinding customInputBinding, InputBindingSpec defaultInputBindingSpec)
		{
			return InputBinding.Create((customInputBinding != null) ? customInputBinding.ToInputBindingSpec() : defaultInputBindingSpec, (customInputBinding != null) ? customInputBinding.DefaultName : null, this.KeyBindingSpec.AllowOtherModifiers);
		}
	}
}
