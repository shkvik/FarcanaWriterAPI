
public static class Storage
{
    private static IEnumerable<ArticalModel> ArticalsStorage = new List<ArticalModel>();

    public static void Update(IEnumerable<ArticalModel> articals)
    {
        ArticalsStorage = articals.ToList();
    }
        
    public static bool IsEmpty()
    {
        return ArticalsStorage.Any();
    }

    public static IEnumerable<ArticalModel> Get()
    {
        return ArticalsStorage;
    }
}
