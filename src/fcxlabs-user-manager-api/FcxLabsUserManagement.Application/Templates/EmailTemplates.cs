namespace FcxLabsUserManagement.Application.Resources;

public static class EmailTemplates
{
	public static string ConfirmEmailTemplate = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset=""UTF-8"">
        <title>Confirmação de E-mail</title>
    </head>
    <body>
        <table width=""100%"" cellspacing=""0"" cellpadding=""0"">
            <tr>
                <td align=""center"">
                    <table width=""600"" cellspacing=""0"" cellpadding=""0"">
                        <tr>
                            <td align=""center"" bgcolor=""#007BFF"" style=""padding: 20px;"">
                                <h1 style=""color: #ffffff;"">FcxLabs<br><br>Confirmação de E-mail</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""padding: 20px;"">
                                <p>Olá,</p>
                                <p>Por favor, clique no link abaixo para confirmar o seu e-mail:</p>
                                <p><br><a href=""{0}"" style=""background-color: #007BFF; color: #ffffff; padding: 10px 20px; text-decoration: none; border-radius: 5px;"">Confirmar E-mail</a></p>
                                <p>Se você não solicitou esta confirmação, você pode ignorar este e-mail.</p>
                                <p>Obrigado!</p>
                            </td>
                        </tr>
                        <tr>
                            <td align=""center"" bgcolor=""#f4f4f4"" style=""padding: 20px;"">
                                <p style=""color: #555555;"">Se você tiver alguma dúvida, entre em contato conosco em <a href=""mailto:{1}"">{1}</a></p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
    </html>";
}
