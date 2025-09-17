namespace UserAndCommentsManager.Extentions;

public static class AppExtentions
{
    public static WebApplication UseServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}