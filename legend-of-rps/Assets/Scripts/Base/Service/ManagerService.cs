public class ManagerService<U, T> : Service<T> where T : class, new()
{
    protected U manager;
    public void setManager(U manager)
    {
        this.manager = manager;
    }

    public U getManger()
    {
        return manager;
    }
}