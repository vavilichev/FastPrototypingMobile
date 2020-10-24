﻿using System;
using System.Collections.Generic;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture {
	public interface ISceneConfig {
		string sceneName { get; }
		Dictionary<Type, IRepository> CreateAllRepositories();
		Dictionary<Type, IInteractor> CreateAllInteractors();
		Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController);
	}
}