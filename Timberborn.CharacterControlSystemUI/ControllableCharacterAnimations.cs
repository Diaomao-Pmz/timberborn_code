using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CharacterControlSystem;

namespace Timberborn.CharacterControlSystemUI
{
	// Token: 0x02000009 RID: 9
	public class ControllableCharacterAnimations
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002750 File Offset: 0x00000950
		public IReadOnlyList<string> GatAnimations(ControllableCharacter controllableCharacter, string templateName)
		{
			List<string> list;
			if (!this._animations.TryGetValue(templateName, out list))
			{
				list = (from animationName in controllableCharacter.GetAnimationNames()
				orderby animationName
				select animationName).ToList<string>();
				list.Remove(ControllableCharacterAnimations.DefaultAnimation);
				list.Insert(0, ControllableCharacterAnimations.DefaultAnimation);
				this._animations[templateName] = list;
			}
			return list;
		}

		// Token: 0x04000021 RID: 33
		public static readonly string DefaultAnimation = "CharacterControlAnimation";

		// Token: 0x04000022 RID: 34
		public readonly Dictionary<string, List<string>> _animations = new Dictionary<string, List<string>>();
	}
}
