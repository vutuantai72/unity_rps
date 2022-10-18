public class Service<T> where T : class, new()
{
    private static T _객체 = null;

    public static T @object
    {
        get
        {
            if (_객체 == null)
            {
                _객체 = new T();
            }
            return _객체;

        }
    }
}