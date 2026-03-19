using System;
using Timberborn.InputSystemUI;
using UnityEngine.UIElements;

namespace Timberborn.TimeSpeedButtonSystem
{
	// Token: 0x02000005 RID: 5
	public class TimeSpeedButtonFactory
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public TimeSpeedButtonFactory(BindableButtonFactory bindableButtonFactory)
		{
			this._bindableButtonFactory = bindableButtonFactory;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002124 File Offset: 0x00000324
		public TimeSpeedButton Create(Button button, int index, Action<int> clickCallback)
		{
			int timeSpeed;
			if (!int.TryParse(button.name.Replace("Speed", ""), out timeSpeed))
			{
				throw new ArgumentException("Unable to parse speed value for " + button.name);
			}
			this._bindableButtonFactory.CreateAndBind(button, string.Format(TimeSpeedButtonFactory.TimeSpeedKeyFormat, index), delegate
			{
				clickCallback(timeSpeed);
			});
			return new TimeSpeedButton(button, timeSpeed);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string TimeSpeedKeyFormat = "Speed{0}";

		// Token: 0x0400000B RID: 11
		public readonly BindableButtonFactory _bindableButtonFactory;
	}
}
