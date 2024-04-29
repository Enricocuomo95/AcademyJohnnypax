namespace aspWeb.Services
{
    public interface Iservice<T>
    {
        public List<T> getAll();
    }
}
