using System;
using Steamworks;
using Timberborn.Localization;

namespace Timberborn.SteamStoreSystem
{
	// Token: 0x02000005 RID: 5
	public class SteamLanguage
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public SteamLanguage(SteamManager steamManager)
		{
			this._steamManager = steamManager;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public string GetLanguageCode()
		{
			if (!this._steamManager.Initialized)
			{
				return LocalizationCodes.Default;
			}
			string currentGameLanguage = SteamApps.GetCurrentGameLanguage();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(currentGameLanguage);
			if (num <= 2499415067U)
			{
				if (num <= 599131013U)
				{
					if (num != 380651494U)
					{
						if (num != 505713757U)
						{
							if (num == 599131013U)
							{
								if (currentGameLanguage == "french")
								{
									return LocalizationCodes.French;
								}
							}
						}
						else if (currentGameLanguage == "brazilian")
						{
							return LocalizationCodes.BrazilianPortuguese;
						}
					}
					else if (currentGameLanguage == "russian")
					{
						return LocalizationCodes.Russian;
					}
				}
				else if (num <= 1901528810U)
				{
					if (num != 683056061U)
					{
						if (num == 1901528810U)
						{
							if (currentGameLanguage == "japanese")
							{
								return LocalizationCodes.Japanese;
							}
						}
					}
					else if (currentGameLanguage == "ukrainian")
					{
						return LocalizationCodes.Ukrainian;
					}
				}
				else if (num != 2471602315U)
				{
					if (num == 2499415067U)
					{
						if (currentGameLanguage == "english")
						{
							return LocalizationCodes.English;
						}
					}
				}
				else if (currentGameLanguage == "italian")
				{
					return LocalizationCodes.Italian;
				}
			}
			else if (num <= 3264533134U)
			{
				if (num <= 3180870988U)
				{
					if (num != 2805355685U)
					{
						if (num == 3180870988U)
						{
							if (currentGameLanguage == "polish")
							{
								return LocalizationCodes.Polish;
							}
						}
					}
					else if (currentGameLanguage == "schinese")
					{
						return LocalizationCodes.SimplifiedChinese;
					}
				}
				else if (num != 3210859552U)
				{
					if (num == 3264533134U)
					{
						if (currentGameLanguage == "tchinese")
						{
							return LocalizationCodes.TraditionalChinese;
						}
					}
				}
				else if (currentGameLanguage == "koreana")
				{
					return LocalizationCodes.Korean;
				}
			}
			else if (num <= 3719199419U)
			{
				if (num != 3405445907U)
				{
					if (num == 3719199419U)
					{
						if (currentGameLanguage == "spanish")
						{
							return LocalizationCodes.Spanish;
						}
					}
				}
				else if (currentGameLanguage == "german")
				{
					return LocalizationCodes.German;
				}
			}
			else if (num != 3739448251U)
			{
				if (num == 3759690811U)
				{
					if (currentGameLanguage == "thai")
					{
						return LocalizationCodes.Thai;
					}
				}
			}
			else if (currentGameLanguage == "turkish")
			{
				return LocalizationCodes.Turkish;
			}
			return LocalizationCodes.Default;
		}

		// Token: 0x04000007 RID: 7
		public readonly SteamManager _steamManager;
	}
}
