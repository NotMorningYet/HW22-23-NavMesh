public class NavigatorSwitcher
{
    private Navigator _navigator;    

    public Navigator SetNavigator(NavigatorTypes navigatorType)
    {
        switch (navigatorType)
        {
            case NavigatorTypes.Simple:
                _navigator = new SimpleNavigator();
                break;
            case NavigatorTypes.NavMesh:
                _navigator = new NavMeshNavigator();
                break;
            default:
                _navigator = new SimpleNavigator();
                break;
        }

        return _navigator;
    }
}
