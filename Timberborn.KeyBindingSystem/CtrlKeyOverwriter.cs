using System;
using Timberborn.PlatformUtilities;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000007 RID: 7
	public class CtrlKeyOverwriter
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public InputBindingSpec OverwriteIfOnMacOS(InputBindingSpec inputBindingSpec)
		{
			if (!ApplicationPlatform.IsMacOS())
			{
				return inputBindingSpec;
			}
			return CtrlKeyOverwriter.OverwriteCtrl(inputBindingSpec);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002111 File Offset: 0x00000311
		public static InputBindingSpec OverwriteCtrl(InputBindingSpec inputBindingSpec)
		{
			InputBindingSpec inputBindingSpec2 = (InputBindingSpec)inputBindingSpec.<Clone>$();
			inputBindingSpec2.Path = CtrlKeyOverwriter.OverwritePath(inputBindingSpec.Path);
			inputBindingSpec2.InputModifiers = CtrlKeyOverwriter.OverwriteModifiers(inputBindingSpec.InputModifiers);
			return inputBindingSpec2;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		public static string OverwritePath(string path)
		{
			return path.Replace(CtrlKeyOverwriter.LeftCtrlKey, CtrlKeyOverwriter.LeftCmdKey).Replace(CtrlKeyOverwriter.RightCtrlKey, CtrlKeyOverwriter.RightCmdKey);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002161 File Offset: 0x00000361
		public static InputModifiers OverwriteModifiers(InputModifiers modifiers)
		{
			if (modifiers.HasFlag(InputModifiers.Ctrl))
			{
				modifiers &= ~InputModifiers.Ctrl;
				modifiers |= InputModifiers.Cmd;
			}
			return modifiers;
		}

		// Token: 0x04000008 RID: 8
		public static readonly string LeftCtrlKey = "leftCtrl";

		// Token: 0x04000009 RID: 9
		public static readonly string RightCtrlKey = "rightCtrl";

		// Token: 0x0400000A RID: 10
		public static readonly string LeftCmdKey = "leftMeta";

		// Token: 0x0400000B RID: 11
		public static readonly string RightCmdKey = "rightMeta";
	}
}
