using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserAndCommentsManager.Data;

namespace UserAndCommentsManager.Extentions;

public static class BuilderExtentions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MinimalContextDb>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        return builder;
    }
}
