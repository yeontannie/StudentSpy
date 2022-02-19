namespace StudentSpy.DataManager.Services
{
    public interface IEmailService
    {
        public void SendEmail(string email, string subject, string message);
    }
}
