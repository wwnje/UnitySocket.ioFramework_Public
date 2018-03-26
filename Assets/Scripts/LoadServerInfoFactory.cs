public class LoadServerInfoFactory : Zenject.IFactory<ILoadServerInfo>
{
    public ILoadServerInfo Create()
    {
        //return new LoadServerInfoLocal();
        return new LoadServerInfoOnLine();
    }
}