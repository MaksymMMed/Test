using Backend.Entity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public static class Seeder
    {
        private static string Invoice = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <title>PDF</title>
            <style>
                body {{ font-family: Arial, sans-serif; font-size: 14px; color: #333; }}
                h1 {{ color: #2E86C1; }}
                p {{ margin: 5px 0; }}
                .highlight {{ background-color: #F9E79F; padding: 3px 5px; }}
            </style>
        </head>
        <body>
            <h1>Sender: **SenderName**</h1>
            <p>Receiver: **ReceiverName**</p>
            <p>Sum of money: **Sum**</p>
        </body>
        </html>";

        private static string RegistrationEmail = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <title>Registration Confirmation</title>
            <style>
                body {{ font-family: Arial, sans-serif; font-size: 14px; color: #333; }}
                h1 {{ color: #28A745; }}
                p {{ margin: 5px 0; }}
                .highlight {{ background-color: #DFF0D8; padding: 5px 10px; border-radius: 4px; }}
                a {{ color: #FFFFFF; background-color: #28A745; padding: 5px 10px; text-decoration: none; border-radius: 4px; }}
            </style>
        </head>
        <body>
            <h1>Welcome, **UserName**!</h1>
            <p>Thank you for registering at **SiteName**.</p>
        </body>
        </html>";

        public static async Task CreateDb(AppDbContext dbContext)
        {
            await dbContext.Database.MigrateAsync();
            await dbContext.Templates.AddAsync(new Template { Id = Guid.NewGuid(), Name = "Invoice", Content = Invoice, Placeholders = new List<string>() {"**Sum**","**SenderName**","**ReceiverName**"}});
            await dbContext.Templates.AddAsync(new Template { Id = Guid.NewGuid(), Name = "Template 2", Content = RegistrationEmail, Placeholders = new List<string>() { "**UserName**", "**SiteName**" } });
            await dbContext.SaveChangesAsync();
        }
    }
}
