using System;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000008 RID: 8
	public class CustomInputBinding
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021AC File Offset: 0x000003AC
		public string Path { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public InputModifiers InputModifiers { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		public string DefaultName { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x000021C4 File Offset: 0x000003C4
		public CustomInputBinding(string path, InputModifiers inputModifiers, string defaultName)
		{
			this.Path = path;
			this.InputModifiers = inputModifiers;
			this.DefaultName = defaultName;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E1 File Offset: 0x000003E1
		public bool IsSame(InputBindingSpec inputBindingSpec)
		{
			return this.Path == inputBindingSpec.Path && this.InputModifiers == inputBindingSpec.InputModifiers;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002206 File Offset: 0x00000406
		public InputBindingSpec ToInputBindingSpec()
		{
			return new InputBindingSpec
			{
				Path = this.Path,
				InputModifiers = this.InputModifiers
			};
		}

		// Token: 0x0400000C RID: 12
		public static readonly CustomInputBinding UndefinedBinding = new CustomInputBinding(string.Empty, InputModifiers.None, null);
	}
}
