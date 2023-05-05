
using AngleSharp.Common;

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

    public static IEnumerable<ArticalModel> GetAll()
    {
        return ArticalsStorage;
    }

    public static ArticalModel? GetItemById(int index)
    {
        try
        {
            return ArticalsStorage.GetItemByIndex(index);
        }
        catch(Exception ex)
        {
            ConsoleFormat.Fail(ex.Message);
            return null;
        }
        
    }
}
