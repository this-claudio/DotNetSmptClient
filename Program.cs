
using EmailClient;

Console.WriteLine("Hello, World!");


var emailsender = new MailClient("smtp.gmail.com", "Nome Remetente","remetente@gmail.com", "senha 16 digitos sem espaco");
emailsender.SendEmail("destinoa@gmail.com", "Tituto do Email", "Mensagem do email");

Console.ReadKey();