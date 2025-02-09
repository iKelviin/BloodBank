namespace BloodBank.Site.Notifications;

public class ThemeNotification
{
    public Action? OnThemeChange { get; set; }

    public void NotificationOnChange()
    {
        OnThemeChange?.Invoke();
    }
}