# LoadingScreen
Independent loading screen. Use Loading.LoadScene() for loading and showing loading screen automatically

## Methods:

### LoadingScreen.Show()
Create instance of LoadingScreenVisual if needed and show it on the screen. Create game object from the [LOADING SCREEN] prefab.

### LoadingScreen.Hide()
Hide loading screen visual. If it has an animation, this method starts that animation.

### LoadingScreen.HideInstantly()
Hide loading screen without animations permanently.

## Events
- LoadingScreen.OnLoadingScreenShownEvent;
- LoadingScreen.OnLoadingScreenHideStartEvent;
- LoadingScreen.OnLoadingScreenHiddenCompletelyEvent;
