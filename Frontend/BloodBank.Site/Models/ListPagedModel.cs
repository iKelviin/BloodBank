namespace BloodBank.Site.Models;

public class ListPagedModel<T>
{
    public ListPagedModel(List<T> items,int currentPage, int totalPages)
    {
        Items = items;
        CurrentPage = currentPage;
        TotalPages = totalPages;
    }

    public List<T> Items { get; } = new List<T>();
    public int CurrentPage { get; }
    public int TotalPages { get; }
    public bool Next => CurrentPage < TotalPages;
    public bool Previous => CurrentPage > 1;
}